using UnityEngine;
using System.Collections;

public class SatelliteController : MonoBehaviour {
	public int speed = 10;
	private int health;
	public int maxHealth = 1000;
	public GameObject protector;

	int dir;

	void OnEnable()
	{
		dir = Random.Range(0, 2);
		health = maxHealth;
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "EnemyBullet") 
		{
			health -= 5;

			protector.SendMessage("CallAssist", other.GetComponent<BulletDestroy>().shipFired, SendMessageOptions.DontRequireReceiver);

			if (health <= 0) 
			{
				this.gameObject.SetActive (false);
				health = maxHealth;
			}

			other.gameObject.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update () {
		if (this.dir == 0)
		{
			transform.Rotate(Vector3.forward * speed * Time.deltaTime);
		}
		if (this.dir == 1)
		{
			transform.Rotate(-Vector3.forward * speed * Time.deltaTime);
		}
	}
}
