using UnityEngine;
using System.Collections;

public class BulletDestroy : MonoBehaviour {

	void OnEnable()
    {
        Invoke("Destroy", 2f);        
    }
    void Destroy()
    {
        gameObject.SetActive(false);
    }
    void OnDisable()
    {
        CancelInvoke();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" && this.gameObject.tag == "PlayerBullet")
        {
            Destroy();
        }
        if (other.tag == "Player" && this.gameObject.tag == "EnemyBullet")
        {
            Destroy();
        }
    }
}
