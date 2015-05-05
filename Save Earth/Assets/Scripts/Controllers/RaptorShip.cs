using UnityEngine;
using System.Collections;

public class RaptorShip: AIController  {
    
	public float distanceFromPlayer;
	public bool canWake;
	public int mothershipShieldDelay;
	private bool hasMothershipShield;
	private int shieldHealth;
	public float strafeSpeed;
	public Vector3 strafeDir;
	private Vector2 impulse;
	private Vector3 direction;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		weaponShotPosition = transform.FindChild ("RaptorCannon").gameObject.transform;
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
			strafeDir = Random.Range(0, 100) > 50 ? transform.right : -transform.right;
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
			rb.AddForce (direction * speed, ForceMode2D.Force);

		if (Vector3.Distance (transform.position, pShip.transform.position) < distanceFromPlayer / 2) 
		{
			impulse = -direction * strafeSpeed;
			rb.AddForce(impulse, ForceMode2D.Impulse);
		} 
		
		rb.AddRelativeForce(strafeDir * strafeSpeed, ForceMode2D.Force);

		if (Vector3.Distance (transform.position, pShip.transform.position) > distanceFromPlayer + 4)
			currentState = AIstate.AI_Follow;

		//transform.position = ((transform.position - pShip.transform.position).normalized * distanceFromPlayer + pShip.transform.position);
		//transform.RotateAround (pShip.transform.position, Vector3.forward, -(strafeSpeed/4));
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
