using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
    public float speed;
    public enum Weapons { BlueWeapon, RedWeapon, Cannon };
    public Weapons currentWeapon;
    private Vector3 rotDir;
 

	[HideInInspector]
    public int health;


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
	public void FireCannon(Weapons currentWeapon, float bulletSpeed, AudioSource audioSrc, AudioClip shotSound, Transform shotPos, Vector3 shotDirection, bool enemy, Color color , string Tag, GameObject theShip)
    {
		if(currentWeapon == Weapons.BlueWeapon)
        {
            FireBlueCannon(bulletSpeed, audioSrc, shotSound, shotPos, shotDirection, enemy, color, Tag, theShip);
        }
        if (currentWeapon == Weapons.RedWeapon)
        {
            FireRedCannon(bulletSpeed, audioSrc, shotSound, shotPos, shotDirection, enemy, color, Tag, theShip);
        }
        if (currentWeapon == Weapons.Cannon)
        {
           FireCannon1(shotPos, shotDirection, Tag, theShip);
        }
    }
	void FireBlueCannon(float bulletSpeed, AudioSource audioSrc, AudioClip shotSound, Transform shotPos, Vector3 shotDirection, bool enemy, Color color, string Tag, GameObject theShip)
    {
        if (!enemy)
        {
            audioSrc.clip = shotSound;
            audioSrc.Play();
        }

        //ObjectPooler op = GameObject.Find("Pooler").GetComponent<ObjectPooler>();
        //PoolingManager gm = GameObject.Find("PoolManager").GetComponent<PoolingManager>();
        GameObject projectile = ObjectPooler.Instance.ReturnObject("Bullet");
        SpriteRenderer ren = projectile.gameObject.GetComponent<SpriteRenderer>();
        ren.color = Color.blue;
        projectile.tag = Tag;
        projectile.SetActive(true);
        projectile.transform.position = shotPos.position;
        projectile.GetComponent<Rigidbody2D>().velocity = shotDirection * bulletSpeed;
        Debug.Log(bulletSpeed + " " + theShip);
		projectile.GetComponent<BulletDestroy>().shipFired = theShip;
    }
    void FireRedCannon(float bulletSpeed, AudioSource audioSrc, AudioClip shotSound, Transform shotPos, Vector3 shotDirection, bool enemy, Color color, string Tag, GameObject theShip)
    {
        if (!enemy)
        {
            audioSrc.clip = shotSound;
            audioSrc.Play();
        }

       // ObjectPooler op = GameObject.Find("Pooler").GetComponent<ObjectPooler>();
        //PoolingManager gm = GameObject.Find("PoolManager").GetComponent<PoolingManager>();
        GameObject projectile = ObjectPooler.Instance.ReturnObject("Bullet");
        SpriteRenderer ren = projectile.gameObject.GetComponent<SpriteRenderer>();
        ren.color = Color.red;
        projectile.tag = Tag;        
        projectile.transform.position = shotPos.position;
        projectile.SetActive(true);
        projectile.GetComponent<BulletDestroy>().bulletSpeed = bulletSpeed;
        projectile.GetComponent<BulletDestroy>().dir = shotDirection;
        projectile.GetComponent<BulletDestroy>().StartMove();

        
        //projectile.GetComponent<Rigidbody2D>().velocity = shotDirection * bulletSpeed;
        Debug.Log(bulletSpeed + " " + theShip);
		projectile.GetComponent<BulletDestroy>().shipFired = theShip;
    }
    public void FireCannon1(Transform shotPos, Vector3 shotDirection, string Tag, GameObject theShip)
    {
        Debug.Log("Fired");
        GameObject projectile = ObjectPooler.Instance.ReturnObject("Bullet");
        SpriteRenderer ren = projectile.gameObject.GetComponent<SpriteRenderer>();
        ren.color = WeaponManager.Instance.canProjectile.projectileColor;
        projectile.transform.localScale = new Vector3(WeaponManager.Instance.canProjectile.projectileSize, WeaponManager.Instance.canProjectile.projectileSize, 1);
        projectile.tag = Tag;
        projectile.SetActive(true);
        projectile.transform.position = shotPos.position;
        projectile.GetComponent<Rigidbody2D>().velocity = shotDirection * WeaponManager.Instance.canProjectile.projectileSpeed;
        projectile.GetComponent<BulletDestroy>().shipFired = theShip;


    }


#endregion

  
}
