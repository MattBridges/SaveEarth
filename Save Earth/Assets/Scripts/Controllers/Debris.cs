using UnityEngine;
using System.Collections;

public class Debris : MonoBehaviour {

	private Rigidbody2D rb;
	private SpriteRenderer sprite;
	private float debrisLife;
	private float tempTime;
	private int health;
	public int maxHealth;

	// Use this for initialization
	void Awake () 
	{
		rb = this.gameObject.GetComponent<Rigidbody2D>();
		sprite = this.gameObject.GetComponent<SpriteRenderer>();
		health = maxHealth;
	}
	
	void OnEnable()
	{
		tempTime = Time.time;
	}
	
	public void RandomizeDebris(float lowRange, float hiRange, float lifetime, float speed)
	{
		debrisLife = lifetime;
		
		Vector3 loc = new Vector3(-50, Random.Range(lowRange, hiRange), 0);
		transform.position = loc;
		sprite.sprite = DebrisManager.Instance.debrisSprites[Random.Range(0, DebrisManager.Instance.debrisSprites.Length)];

		float scale = Random.Range(0.25f, 2.0f);
		int rot = Random.Range (0, 360);
		
		transform.localScale = new Vector3(scale, scale, scale);
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, rot));
		this.gameObject.SetActive(true);
		Destroy(this.gameObject.GetComponent<PolygonCollider2D>());		
		PolygonCollider2D pC = this.gameObject.AddComponent<PolygonCollider2D>();
		pC.isTrigger = true;	
		rb.AddForce(new Vector3(speed, 0, 0), ForceMode2D.Impulse);
		rb.AddTorque(Random.Range(-2.5f, 2.5f), ForceMode2D.Impulse);
	}
	
	void OnDisable()
	{
		health = maxHealth;
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "PlayerBullet")
		{
			health -= 5;
			
			if (health <= 0)
				this.gameObject.SetActive(false);
		}	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if ((Time.time - tempTime) > debrisLife)
		{
			this.gameObject.SetActive (false);
			tempTime = 0f;
		}
	}
}
