using UnityEngine;
using System.Collections;

public class PlayerShip : Ship
{
    #region Variables
    public Sprite shipSprite;
    public float moveSpeed;
    public GameObject weaponShot;
    public float bulletSpeed;
    public AudioClip shotSound;

    private CNAbstractController leftStick;
    private CNAbstractController rightStick;
    private CNAbstractController[] sticks;
    private Rigidbody2D rb;
    private Transform weaponShotPosition;
    private AudioSource audioSrc;
 
    public Collider2D col;
    public static PlayerShip curPlayer;
    public int health;
    
    

    #endregion
 	void Start () {
        //Create ship and assign sprite and give speed value
        CreateShip(shipSprite, moveSpeed);
        this.gameObject.transform.parent = GameObject.Find("ShipCollector").transform;
        this.health = 100;
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
        Rotate(dir);
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
                FireCannon(weaponShot, bulletSpeed, audioSrc, shotSound, weaponShotPosition, cDir);
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
        curPlayer.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        curPlayer.transform.position = new Vector2(0,0);
        this.health = 100;
        
    }
    public void TakeDamage(int amt)
    {
        CameraFX camFX = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFX>();       
        camFX.ScreenShake();
        this.health -= amt;
        
        if(health<=0)
        {
            RespawnPlayer();
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bullet")
        {
            TakeDamage(10);            
        }

    }
    
}
