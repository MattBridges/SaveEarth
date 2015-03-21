using UnityEngine;
using System.Collections;

public class PlayerShip : Ship
{
    #region Variables
    public Sprite shipSprite;
    public float moveSpeed;
    public GameObject weaponShot;
    public float bulletSpeed;
    private CNAbstractController leftStick;
    private CNAbstractController rightStick;
    private CNAbstractController[] sticks;
    private Rigidbody2D rb;
    private Transform weaponShotPosition;
    #endregion

    // Use this for initialization
	void Start () {
        //Create ship and assign sprite and give speed value
        CreateShip(shipSprite, moveSpeed);
        //Object references
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        weaponShotPosition = GameObject.Find("PlayerCannon").transform;

        //Register joysticks
        RegisterJoysticks();
        //Register methods to events
        leftStick.ControllerMovedEvent += MoveShip;
        rightStick.ControllerMovedEvent += RotateShip;
        rightStick.FingerTouchedEvent += StartFire;
        rightStick.FingerLiftedEvent += StopFire;
	}
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
            //audio.PlayOneShot(shotSound);
            Vector3 dir = new Vector3(rightStick.GetAxis("Horizontal"), rightStick.GetAxis("Vertical"));

            GameObject projectile = (GameObject)Instantiate(weaponShot, weaponShotPosition.position, Quaternion.identity);
            projectile.transform.parent = GameObject.Find("BulletCollector").transform;
            projectile.GetComponent<Rigidbody>().velocity = dir * bulletSpeed;
        }
    }

}
