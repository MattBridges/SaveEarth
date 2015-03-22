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
    #endregion
 	void Start () {
        //Create ship and assign sprite and give speed value
        CreateShip(shipSprite, moveSpeed);
        //Object references
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        weaponShotPosition = GameObject.Find("PlayerCannon").transform;
        audioSrc = this.gameObject.GetComponent<AudioSource>();

        //Register joysticks
        RegisterJoysticks();
        //Register methods to events
        leftStick.ControllerMovedEvent += MoveShip;
        rightStick.ControllerMovedEvent += RotateShip;
        rightStick.FingerTouchedEvent += StartFire;
        rightStick.FingerLiftedEvent += StopFire;
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
}
