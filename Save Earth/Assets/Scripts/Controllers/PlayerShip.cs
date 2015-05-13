using UnityEngine;
using System.Collections;

public class PlayerShip : Ship
{
    #region Variables
    public Sprite shipSprite;
    public float moveSpeed;
    public float bulletSpeed;
    public AudioClip shotSound;
    public PlayerShip curPlayer;
    public Color bulletColor;

    private CNAbstractController leftStick;
    private CNAbstractController rightStick;
    private CNAbstractController[] sticks;
    private Rigidbody2D rb;
    private Transform weaponShotPosition;
    private AudioSource audioSrc;
	private bool paused;
	private Vector2 oldVelocity;
    public Camera mainCam;
    private GameObject spawnPoint;
    private UIManager ui;
    private static PlayerShip _instance;
    public static PlayerShip Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayerShip>();
            }
            return _instance;
        }
    }

 
    
    

    #endregion
 	void Start () {
		speed = moveSpeed;
        this.gameObject.transform.parent = GameObject.Find("ShipCollector").transform;
        currentWeapon = Weapons.BlueWeapon;

        //Object references
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        weaponShotPosition = GameObject.Find("PlayerCannon").transform;
        audioSrc = this.gameObject.GetComponent<AudioSource>();
        curPlayer = this;
        mainCam = GameObject.FindObjectOfType<GameManager>().mainCam;
        ui = GameObject.FindObjectOfType<UIManager>();
        
  
        //Register joysticks
        RegisterJoysticks();
        //Register methods to events
        this.leftStick.ControllerMovedEvent += MoveShip;
        this.rightStick.ControllerMovedEvent += RotateShip;
        this.rightStick.FingerTouchedEvent += StartFire;
        this.rightStick.FingerLiftedEvent += StopFire;
	}
    void FixedUpdate()
    {
            if(Input.GetKeyDown(KeyCode.A))
            {
                currentWeapon = Weapons.BlueWeapon;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                currentWeapon = Weapons.RedWeapon;
            }
            
    }

	public void TogglePause()
	{
		paused = !paused;
		
		if (paused) 
		{
            if (rb != null)
            {
                oldVelocity = rb.velocity;
                rb.velocity = new Vector2(0, 0);
            }              
            else
                oldVelocity = new Vector2(0, 0);
			
		} 
		else 
		{
			rb.velocity = oldVelocity;
		}
	}

    #region EventMethods
    void MoveShip(Vector3 dir, CNAbstractController controller)
    {
		if (!paused)
			JoystickMove(rb, dir);
    }
    void RotateShip(Vector3 dir, CNAbstractController controller)
    {
		if (!paused)
	        Rotate(dir, this.gameObject.transform);
    }
    void StartFire(CNAbstractController controller)
    {
        StartCoroutine("FireWepon");
    }
    void StopFire(CNAbstractController controller)
    {
        StopCoroutine("FireWepon");
    }
    #endregion
    #region Methods
    void RegisterJoysticks()
    {
        //Find all joysticks in scene
        sticks = CNAbstractController.FindObjectsOfType<CNJoystick>();
        //Assign joysticks
        for (var i = 0; i < sticks.Length; i++)
        {
            if (sticks[i].name == "LeftStick")
            {
                leftStick = sticks[i];
            }
            if (sticks[i].name == "RightStick")
            {
                rightStick = sticks[i];
            }
        }
    }
    IEnumerator FireWepon()
    {
		while (true) {
			yield return new WaitForSeconds (.15f);           
            
			Vector3 sDir = new Vector3 (rightStick.GetAxis ("Horizontal"), rightStick.GetAxis ("Vertical"));
			Vector3 cDir = sDir.normalized;
            
			bool canFire = false;
			if (cDir.x == 0 && cDir.y == 0)
				canFire = false;
			else
				canFire = true;
			if (canFire && !paused) {
				FireCannon (currentWeapon, bulletSpeed, audioSrc, shotSound, weaponShotPosition, cDir, false, bulletColor, "PlayerBullet", this.gameObject);
			}
		}
    }
    #endregion
    public void SpawnPlayer()
    {
        GameObject spawnPoint = GameObject.Find("PlayerSpawn");
        GameObject ship = GameObject.FindGameObjectWithTag("Player");
        if(ship==null)
        {
            if (spawnPoint != null)
            {
                Instantiate(this.gameObject, spawnPoint.transform.position, Quaternion.identity);
                this.health = 100;
            }
     
        }
        else 
        {
            RespawnPlayer();            
        }
           
    }
    public void RespawnPlayer()
    {
        iTween.Stop();
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("SpawnNode");
        CameraController cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        GameObject ship = GameObject.FindGameObjectWithTag("Player");
        
        foreach(GameObject node in nodes)
        {
            if (node.name == "PlayerSpawn")
            {
                spawnPoint = node;
                Debug.Log("Found Player Spawn");
                ship.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                if (spawnPoint != null)
                {
                    ship.transform.position = spawnPoint.transform.position;
                    ship.transform.rotation = Quaternion.identity;
                    cam.ReturnCam();
                    this.health = 100;
                }
                if (spawnPoint == null)
                {
                    Debug.Log("No Player Spawn Point");
                }
                
                break;
            }                
        }

            
       
            
        
        
        
    }
  
    public void TakeDamage(int amt)
    {
        CameraFX camFX = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFX>();       
        camFX.ScreenShake();
        this.health -= amt;

        if (this.health <= 0)
        {
            GameObject.FindObjectOfType<GameManager>().LoseALife();
            this.health = 100;

            
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemyBullet")
        {
            TakeDamage(1);            
        }

		if (other.tag == "Enemy") 
		{
			TakeDamage (20);
		}
        if(other.tag == "Mine")
        {
            TakeDamage(25);
            Debug.Log("Hit Mine");
        }
        ui.UpdatePlayerHealthText();
    }
    
}
