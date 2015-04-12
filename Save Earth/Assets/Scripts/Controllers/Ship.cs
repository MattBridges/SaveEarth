using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
    private float speed;
    private Vector3 rotDir;
    private int health;
   

    public Ship(float Speed, int Health)
    {
        speed = Speed;
        health = Health;
    }

    #region Movement Methods
    public void MoveLeft(Rigidbody2D rb)
    {
        rb.AddForce(-Vector2.right * 4);
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
    public void FireCannon(GameObject projectle, float bulletSpeed, AudioSource audioSrc, AudioClip shotSound, Transform shotPos, Vector3 shotDirection )
    {
        audioSrc.clip = shotSound;
        audioSrc.Play();
     
        ObjectPooler op = GameObject.Find("Pooler").GetComponent<ObjectPooler>();
        PoolingManager gm = GameObject.Find("PoolManager").GetComponent<PoolingManager>();
        GameObject projectile = op.ReturnObject(gm.bullets, gm.bullet, gm.bulletCollector);
        projectile.SetActive(true);
        projectile.transform.position = shotPos.position;
        projectile.GetComponent<Rigidbody>().velocity = shotDirection * bulletSpeed;
    }
#endregion

}
