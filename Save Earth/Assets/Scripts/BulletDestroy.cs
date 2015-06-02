using UnityEngine;
using System.Collections;

public class BulletDestroy : MonoBehaviour {

	private Vector2 oldVelocity;
	private Rigidbody2D rb;
	private bool paused;
	public GameObject shipFired;

	void Start()
	{
		rb = this.gameObject.GetComponent<Rigidbody2D>();
	}

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
        this.transform.localScale = new Vector3(1, 1, 1);
    }

	private void TogglePause()
	{
		paused = !paused;

		if (paused) 
		{
			oldVelocity = rb.velocity;
			rb.velocity = new Vector2 (0, 0);
			CancelInvoke();
		} 
		else 
		{
			rb.velocity = oldVelocity;
			Invoke("Destroy", 2f);
		}
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
		if (other.tag == "Enemy" && this.gameObject.tag == "AllyBullet") 
		{
			Destroy ();
		}
		if (other.tag == "Ally" && this.gameObject.tag == "EnemyBullet") 
		{
			Destroy ();
		}
		if (other.tag == "Satellite" && this.gameObject.tag == "EnemyBullet") 
		{
			Destroy ();
		}
		if (other.tag == "Station" && this.gameObject.tag == "EnemyBullet")
		{
			Destroy();
		}
		if (other.tag == "Cannon" && this.gameObject.tag == "PlayerBullet")
		{
			Destroy();
		}
		if (other.tag == "Debris" && this.gameObject.tag == "PlayerBullet")
		{
			Destroy();
		}
    }
}
