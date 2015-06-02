using UnityEngine;
using System.Collections;
[System.Serializable]
public class Cannon
{
    public float projectileSpeed;
    public AudioClip cannonSound;
    public Color projectileColor;
    public int damageAmount;
    public float projectileSize;
    
}
public class WeaponManager : MonoBehaviour {
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

    public Cannon canProjectile = new Cannon();
    
    
    public void FireCannon(Transform shotPos, Vector3 shotDirection, string Tag, GameObject theShip)
    {
        Debug.Log("Fired");
        GameObject projectile = ObjectPooler.Instance.ReturnObject("Bullet");
        SpriteRenderer ren = projectile.gameObject.GetComponent<SpriteRenderer>();
        ren.color = canProjectile.projectileColor;
        projectile.transform.localScale = new Vector3(canProjectile.projectileSize, canProjectile.projectileSize, 1);
        projectile.tag = Tag;
        projectile.SetActive(true);
        projectile.transform.position = shotPos.position;
        projectile.GetComponent<Rigidbody2D>().velocity = shotDirection * canProjectile.projectileSpeed;
        projectile.GetComponent<BulletDestroy>().shipFired = theShip;
        

    }
}
