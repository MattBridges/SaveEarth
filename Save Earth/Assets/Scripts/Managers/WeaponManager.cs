using UnityEngine;
using System.Collections;
[System.Serializable]
public class Cannon
{
    public GameObject projectile;
    public float projectileSpeed;
    public AudioClip cannonSound;
    public Color projectileColor;
    public int damageAmount;
    public float projectileSize;
    
}
[System.Serializable]
public class RedCannon
{
    public GameObject projectile;
    public float projectileSpeed;
    public AudioClip cannonSound;
    public Color projectileColor;
    public int damageAmount;
}
[System.Serializable]
public class BlueCannon
{
    public GameObject projectile;
    public float projectileSpeed;
    public AudioClip cannonSound;
    public Color projectileColor;
    public int damageAmount;
}
public class WeaponManager : MonoBehaviour
{
    #region Singleton Block
    private static WeaponManager _instance;
    public static WeaponManager Instance
    {
        get
        {
            if(_instance==null)
            {
                _instance = GameObject.FindObjectOfType<WeaponManager>();
            }
            return _instance;
        }
    }
    #endregion

    #region Global Variables
    public Cannon canProjectile = new Cannon();
    public RedCannon redProjectile = new RedCannon();
    public BlueCannon blueProjectile = new BlueCannon();
    #endregion
    public void FireCannon(Transform shotPos, Vector3 shotDirection, string Tag, GameObject theShip)
    {
        
        GameObject projectile = ObjectPooler.Instance.ReturnObject(canProjectile.projectile.name);
        projectile.transform.localScale = new Vector3(canProjectile.projectileSize, canProjectile.projectileSize, 1);
        projectile.tag = Tag;
        projectile.SetActive(true);
        projectile.transform.position = shotPos.position;
        projectile.transform.rotation = theShip.transform.rotation;
        projectile.GetComponent<Rigidbody2D>().velocity = shotDirection * canProjectile.projectileSpeed;
        projectile.GetComponent<BulletDestroy>().shipFired = theShip;
        

    }
    public void FireRedCannon(Transform shotPos, Vector3 shotDirection, string Tag, GameObject theShip)
    {
       
        GameObject projectile = ObjectPooler.Instance.ReturnObject(redProjectile.projectile.name);     
        projectile.tag = Tag;
        projectile.transform.position = shotPos.position;
        projectile.transform.rotation = theShip.transform.rotation;
        projectile.SetActive(true);
        projectile.transform.position = shotPos.position;
        projectile.GetComponent<Rigidbody2D>().velocity = shotDirection * redProjectile.projectileSpeed;
        projectile.GetComponent<BulletDestroy>().shipFired = theShip;
        
    }
    public void FireBlueCannon(Transform shotPos, Vector3 shotDirection, string Tag, GameObject theShip)
    {
        
        GameObject projectile = ObjectPooler.Instance.ReturnObject(blueProjectile.projectile.name);  
        projectile.tag = Tag;
        projectile.SetActive(true);
        projectile.transform.position = shotPos.position;
        projectile.transform.rotation = theShip.transform.rotation;
        projectile.GetComponent<Rigidbody2D>().velocity = shotDirection * blueProjectile.projectileSpeed;
        projectile.GetComponent<BulletDestroy>().shipFired = theShip;
    }
}
