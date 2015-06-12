using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour {
    public float speed;
    public enum Weapons { BlueWeapon, RedWeapon, Cannon };
    public Weapons currentWeapon;
    private Vector3 rotDir;

 

	[HideInInspector]
    public float health;


    #region Movement Methods
    public void MoveLeft(Rigidbody2D rb)
    {
        rb.AddForce(-Vector2.right * speed);
    }
    public void MoveRight(Rigidbody2D rb)
    {
        rb.AddForce(Vector2.right * speed);
    }
    public void MoveUp(Rigidbody2D rb)
    {
        rb.AddForce( Vector2.up * speed);
    }
    public void MoveDown(Rigidbody2D rb)
    {
        rb.AddForce(-Vector2.up * speed);       
    }
    public void JoystickMove(Rigidbody2D rb, Vector3 dir)
    {
        rb.AddForce(dir * speed);
      if(rb.velocity.magnitude > speed)
      {
          rb.velocity *= .99f;
      }
      
    }
    public void Rotate(Vector3 stickPos, Transform Ship)
    {
        Vector3 newDir = new Vector3(Ship.eulerAngles.x, Ship.eulerAngles.y, Mathf.Atan2(stickPos.x, stickPos.y) * -1 * Mathf.Rad2Deg + 90);
        if (newDir.z == 90 && stickPos.y == 0)
            rotDir = Ship.transform.eulerAngles;
        else
            rotDir = newDir;

        Ship.eulerAngles = rotDir;

    }
    #endregion
    #region Weapon Methods
    public void FireCannon(Weapons currentWeapon, Transform shotPos, Vector3 shotDirection, string Tag, GameObject theShip)
    {
		if(currentWeapon == Weapons.BlueWeapon)
        {
            WeaponManager.Instance.FireBlueCannon(shotPos, shotDirection, Tag, theShip);
        }
        if (currentWeapon == Weapons.RedWeapon)
        {
            WeaponManager.Instance.FireRedCannon(shotPos, shotDirection, Tag, theShip);
        }
        if (currentWeapon == Weapons.Cannon)
        {
            WeaponManager.Instance.FireCannon(shotPos, shotDirection, Tag, theShip);
        }
    }
	

#endregion


}
