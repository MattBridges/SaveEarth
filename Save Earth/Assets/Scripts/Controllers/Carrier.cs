using UnityEngine;
using System.Collections;

public class Carrier : AIController {

	public float distanceFromBase;
	public bool canWake;
	public float strafeSpeed;
	
	[HideInInspector]
	public bool strafeDir;
	private Vector2 direction;
	[HideInInspector]
	public GameObject currentLevel;
	[HideInInspector]
	public Sprite[] shipSprites;
	private float distance;
	private OrbitalBase oB;
	private float healRate;
	public float repairRange = 7.0f;
	[HideInInspector]
	public OrbitalBaseCannon cannon1, cannon2, cannon3, hacking;
	
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
				
			HealOrbitalBase();
		} 
	}
	
	private void updateTarget(GameObject level)
	{
		GameObject temp = null;

		foreach (OrbitalBase go in GameObject.FindObjectsOfType<OrbitalBase>()) 
		{
			if ((this.gameObject.tag == "Ally" && go.baseType == OrbitalBase.BaseType.Enemy)
			||	(this.gameObject.tag == "Enemy" && go.baseType == OrbitalBase.BaseType.Ally))
				continue;
				
			if (temp == null)
			{
				temp = go.gameObject;
			}
			else if (Vector3.Distance (transform.position, go.transform.position) < Vector3.Distance (transform.position, temp.transform.position))
			{
				temp = go.gameObject;
			}
			
			if (temp != null)
			{
				if (temp == go.gameObject && go.baseType == OrbitalBase.BaseType.Enemy)
				{
					cannon1 = go.transform.GetChild(0).gameObject.GetComponent<OrbitalBaseCannon>();
					cannon2 = go.transform.GetChild(1).gameObject.GetComponent<OrbitalBaseCannon>();
					cannon3 = go.transform.GetChild(2).gameObject.GetComponent<OrbitalBaseCannon>();
					hacking = go.transform.GetChild(3).gameObject.GetComponent<OrbitalBaseCannon>();
				}
			}
		}
			
		target = temp;
	}
	
	private void HealOrbitalBase()
	{
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
				if (oB.baseType == OrbitalBase.BaseType.Ally)
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
				
				if (oB.baseType == OrbitalBase.BaseType.Enemy && this.gameObject.tag == "Enemy")
				{
					if (Vector3.Distance(this.transform.position, cannon1.gameObject.transform.position) < repairRange)
					{
						if ((Time.time - healRate) > 3.0f)
						{
							cannon1.health += 20;
							healRate = Time.time;
						}	
						
						if (cannon1.health > cannon1.maxHealth)
							cannon1.health = cannon1.maxHealth;
					}
					else if (Vector3.Distance(this.transform.position, cannon2.gameObject.transform.position) < repairRange)
					{
						if ((Time.time - healRate) > 3.0f)
						{
							cannon2.health += 20;
							healRate = Time.time;
						}	
						
						if (cannon2.health > cannon2.maxHealth)
							cannon2.health = cannon2.maxHealth;
					}
					else if (Vector3.Distance(this.transform.position, cannon3.gameObject.transform.position) < repairRange)
					{
						if ((Time.time - healRate) > 3.0f)
						{
							cannon3.health += 20;
							healRate = Time.time;
						}	
						
						if (cannon3.health > cannon3.maxHealth)
							cannon3.health = cannon3.maxHealth;
					}
					else if (Vector3.Distance(this.transform.position, hacking.gameObject.transform.position) < repairRange)
					{
						if ((Time.time - healRate) > 3.0f)
						{
							hacking.health += 20;
							healRate = Time.time;
						}	
						
						if (hacking.health > hacking.maxHealth)
							hacking.health = hacking.maxHealth;
					}
				}
			}
		}
	}
	
	public override void FixedUpdate()
	{
		base.FixedUpdate();
		
		if (!target || !target.gameObject.activeSelf)
			updateTarget(currentLevel);
		else 
			UpdateRotation(target.transform, 0);

//		if (hasMothershipShield)
//			sprite.sprite = shipSprites[1];
//		else
//			sprite.sprite = shipSprites[0];
			
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
