using UnityEngine;
using System.Collections;

public class PlayerShip : Ship
{
    #region Variables
    public Sprite shipSprite;
    public float moveSpeed;
    public float bulletSpeed;
    public AudioClip shotSound;
    public static PlayerShip curPlayer;
    public Color bulletColor;

    private CNAbstractController leftStick;
    private CNAbstractController rightStick;
    private CNAbstractController[] sticks;
    private Rigidbody2D rb;
    private Transform weaponShotPosition;
    private AudioSource audioSrc;
 

 
    
    

    #endregion
 	void Start () {
		speed = moveSpeed;
        this.gameObject.transform.parent = GameObject.Find("ShipCollector").transform;

        //Object references
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        weaponShotPosition = GameObject.Find("PlayerCannon").transform;
        audioSrc = this.gameObject.GetComponent<AudioSource>();
        curPlayer = this;
        
  
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
   
    }
    #region EventMethods
    void MoveShip(Vector3 dir, CNAbstractController controller)
    {
        JoystickMove(rb, dir);
    }
    void RotateShip(Vector3 dir, CNAbstractController controller)
    {
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
        while (true)
        {
            yield return new WaitForSeconds(.15f);           
            
            Vector3 sDir = new Vector3(rightStick.GetAxis("Horizontal"), rightStick.GetAxis("Vertical"));
            Vector3 cDir = sDir.normalized;
            
            bool canFire = false;
            if (cDir.x == 0 && cDir.y == 0)
                canFire = false;
            else
                canFire = true;
            if(canFire)
            {
                FireCannon( bulletSpeed, audioSrc, shotSound, weaponShotPosition, cDir, false, bulletColor, "PlayerBullet");
            }
       }
    }
    #endregion
    public void SpawnPlayer()
    {
        GameObject ship = GameObject.FindGameObjectWithTag("Player");
        if(ship!=null)
        {
            Debug.Log("Ship Exists");
        }
        else 
        {
            Instantiate(this.gameObject, Vector2.zero, Quaternion.identity);
            this.health = 100;
        }
           
    }
    public void RespawnPlayer()
    {
        iTween.Stop();
        curPlayer.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        curPlayer.transform.position = new Vector2(0,0);
        CameraController cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        cam.ReturnCam();
        this.health = 100;
        
    }
    public void TakeDamage(int amt)
    {
        CameraFX camFX = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFX>();       
        camFX.ScreenShake();
        this.health -= amt;

        if (this.health <= 0)
        {
            RespawnPlayer();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemyBullet")
        {
            TakeDamage(5);            
        }

    }
    
}
