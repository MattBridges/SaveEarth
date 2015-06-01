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
    
    [HideInInspector]
    public GameObject[] resources;
    
    [HideInInspector]
    public Collectible towingObject;
        
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

 
    void checkTow(PlayerShip pShip, HerculesPart part, bool towDrop)
    {
		if (towDrop)
		{
			if (towingObject == null)
				towingObject = part;
		}
		else
		{
			if (towingObject != null)
				towingObject = null;
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
        resources = new GameObject[2];
        EventManager.doTow += checkTow;
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
        GameObject spawnPoint = GameObject.Find("PlayerShip_Spawn");
        GameObject ship = GameObject.FindGameObjectWithTag("Player");
        if(ship==null)
        {
            if (spawnPoint != null)
            {
                GameObject obj = (GameObject)Instantiate(this.gameObject, spawnPoint.transform.position, Quaternion.identity) as GameObject;
                PoolingManager.Instance.pooled.Add(obj);
                this.health = 100;
				EventManager.ResetTargets();
			}
            else
            {
                GameObject obj = (GameObject)Instantiate(this.gameObject, Vector2.zero, Quaternion.identity)as GameObject;
                PoolingManager.Instance.pooled.Add(obj);
                this.health = 100;
                EventManager.ResetTargets();
            }

        }
        else 
        {
            RespawnPlayer(); 
			EventManager.ResetTargets();				
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
            string[] nme = node.name.Split('_');
    
            if (nme[0] == "PlayerShip")
            {
                spawnPoint = node;
 
                ship.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                if (spawnPoint != null)
                {
                    ship.transform.position = spawnPoint.transform.position;
                    ship.transform.rotation = Quaternion.identity;
                    ship.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    cam.ReturnCam();
                    this.health = 100;
                }
                if (spawnPoint == null)
                {
                    Debug.LogError("No Player Spawn Point");
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

	private int checkResourceSlot()
	{
		if (resources[0] == null)
			return 0;
		else if (resources[1] == null)
			return 1;
		else
			return -1;	
	}

	private void clearResources()
	{
		
		for (int i = 0; i < resources.Length; i++)
		{
			if (resources[i] != null)
			{
				GameObject go = resources[i];
			
				Dropzone.CollectResourceEvent(1);
				resources[i] = null;
				Destroy(go);
			}
		}
	}


    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemyBullet")
        {
            TakeDamage(1);            
        }

		if (other.tag == "Enemy" ) 
		{
			TakeDamage (20);
		}
        if(other.tag == "Mine")
        {
            TakeDamage(25);
            Debug.Log("Hit Mine");
        }
        if (other.tag == "Resource")
        {
        	int slot = checkResourceSlot();
        	
        	if (slot != -1)
        	{
        		resources[slot] = other.gameObject;
        		other.gameObject.SetActive(false);
        	}
        }
        
        if (other.tag == "Dropzone")
        {
        	if (other.transform.parent.tag == "Aphrodite" && checkResourceSlot() != -1)
        	{
        		clearResources();
        	}
        }
        
        ui.UpdatePlayerHealthText();
    }
    
}
