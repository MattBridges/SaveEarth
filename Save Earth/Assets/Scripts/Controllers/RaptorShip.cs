using UnityEngine;
using System.Collections;

public class RaptorShip: AIController  {
    
	public float distanceFromPlayer;
	public bool canWake;
	public int mothershipShieldDelay;
	private bool hasMothershipShield;
	private int shieldHealth;
	public float strafeSpeed;
	public bool strafeDir;
	private Vector2 impulse;
	private Vector2 direction;
	public GameObject currentLevel;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		weaponShotPosition = transform.FindChild ("RaptorCannon").gameObject.transform;

		foreach (RegisterLevel rL in GameObject.FindObjectsOfType<RegisterLevel>()) 
		{
			currentLevel = rL.gameObject;
		}
	}
	
	public override void AIFollow()
	{
		if (Vector3.Distance (transform.position, pShip.transform.position) > distanceFromPlayer)
		{
				//transform.position = Vector3.Lerp (transform.position, pShip.transform.position, (speed * Time.fixedDeltaTime));
				rb.AddForce((pShip.transform.position - transform.position).normalized * speed, ForceMode2D.Force);
		}

		if (Vector3.Distance(transform.position, pShip.transform.position) <= distanceFromPlayer) 
		{
			strafeDir = Random.Range(0, 100) > 50 ? true : false;
			currentState = AIstate.AI_Strafe;
			rb.velocity = Vector3.zero;
		}
	}
	
	public override void AIRetreat()
	{
		base.AIRetreat();
	}
	
	public override void AIAttack()
	{
		base.AIAttack();
	}

	public override void AIIdle()
	{
		if (Vector3.Distance (transform.position, pShip.transform.position) < wakeupDistance) 
		{
			attack = true;
			currentState = AIstate.AI_Follow;
		}
	}

	public override void AIStrafe()
	{
		direction = pShip.transform.position - transform.position;
		direction = direction / direction.magnitude;

		if (Vector3.Distance (transform.position, pShip.transform.position) > distanceFromPlayer)
			direction.x = 1;
		else
			direction.x = 0;

		if (Vector3.Distance (transform.position, pShip.transform.position) < distanceFromPlayer) 
		{
			direction.x = -1;
		} 
		else 
			direction.x = 0;
			
		direction.y = strafeDir ? 1 : -1;

		rb.AddRelativeForce (direction * strafeSpeed, ForceMode2D.Force);

		if (Vector3.Distance (transform.position, pShip.transform.position) > distanceFromPlayer + 4) 
		{
			currentState = AIstate.AI_Follow;
		}
	}

	public override void FixedUpdate()
	{
		base.FixedUpdate();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "PlayerBullet") {
			TakeDamage(5);
            if (currentState == AIstate.AI_Idle)
                currentState = AIstate.AI_Follow;
		}
	}

	void OnDisable()
	{
		health = maxHealth;
	}
}
