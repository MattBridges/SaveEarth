using UnityEngine;
using System.Collections;

public class Carrier : AIController {

	public float distanceFromBase;
	public bool canWake;
	public float strafeSpeed;
	public bool strafeDir;
	private Vector2 direction;
	public GameObject currentLevel;
	public Sprite[] shipSprites;
	private float distance;
	private OrbitalBase oB;
	private float healRate;
	
//	private SpriteRenderer sprite;
		
	// Use this for initialization
	public override void Start () {
		base.Start ();
		
		currentLevel = GameManager.Instance.currentMission;
		updateTarget(currentLevel);
		
//		sprite = this.gameObject.GetComponent<SpriteRenderer>();
//		shipSprites = Resources.LoadAll<Sprite>("Ships/RedShips/RD3");
	}
	
	public override void AIFollow()
	{
		if (target)
		{
			if (Vector3.Distance (transform.position, target.transform.position) > distanceFromBase)
				rb.AddForce((target.transform.position - transform.position).normalized * speed, ForceMode2D.Force);
			
			if (Vector3.Distance(transform.position, target.transform.position) <= distanceFromBase) 
			{
				strafeDir = Random.Range(0, 100) > 50 ? true : false;
				currentState = AIstate.AI_Strafe;
				rb.velocity = Vector3.zero;
				rb.AddForce(transform.forward * 100);
				rb.AddForce(transform.right * 100);
			}
		}
	}
		
	public override void AIIdle()
	{
		base.AIIdle();
	}
	
	public override void AIStrafe()
	{
		if (target)
		{
			direction = target.transform.position - transform.position;
			direction.Normalize();
			
			distance = Vector3.Distance(target.transform.position, transform.position);
					
			rb.AddForce(direction * (target.GetComponent<Rigidbody2D>().mass * rb.mass * 7.0f) / (distance*distance));
						
			if (Vector3.Distance (transform.position, target.transform.position) > distanceFromBase + 4) 
				currentState = AIstate.AI_Follow;
		} 
	}
	
	private void updateTarget(GameObject level)
	{
		GameObject temp = null;

		foreach (OrbitalBase go in GameObject.FindObjectsOfType<OrbitalBase>()) 
		{
			if ((this.gameObject.tag == "Ally" && go.tag == "Enemy")
			||	(this.gameObject.tag == "Enemy" && go.tag == "Ally"))
				continue;
				
			if (temp == null)
				temp = go.gameObject;
			else if (Vector3.Distance (transform.position, go.transform.position) < Vector3.Distance (transform.position, temp.transform.position))
				temp = go.gameObject;
			else
				continue;
		}
			
		target = temp;
	}
	
	public override void FixedUpdate()
	{
		base.FixedUpdate();
		
		if (!target || !target.gameObject.activeSelf)
			updateTarget(currentLevel);
		else 
			UpdateRotation(0);

//		if (hasMothershipShield)
//			sprite.sprite = shipSprites[1];
//		else
//			sprite.sprite = shipSprites[0];
		
		if (oB == null)
		{
			if (target)
				oB = target.GetComponent<OrbitalBase>();
		}
		else
		{			
			if (oB.gameObject != target.gameObject)
				oB = null;
			else
			{
				if (oB.health < maxHealth)
				{
					if ((Time.time - healRate) > 3.0f)
					{
						oB.health += 20;
						healRate = Time.time;
					}	
					
					if (oB.health > oB.maxHealth)
						oB.health = oB.maxHealth;
				}
			}
		}			
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "PlayerBullet" && this.gameObject.tag == "Enemy"
		 || other.gameObject.tag == "EnemyBullet" && this.gameObject.tag == "Ally") 
			TakeDamage(5);		
	}
	
	void OnDisable()
	{
		health = maxHealth;
	}
}
