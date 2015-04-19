using UnityEngine;
using System.Collections;

public class RaptorShip: AIController  {
    
	public float distanceFromPlayer;
	public bool canWake;
	public int mothershipShieldDelay;
	private bool hasMothershipShield;
	private int shieldHealth;
	public float strafeSpeed;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		weaponShotPosition = transform.FindChild ("RaptorCannon").gameObject.transform;
	}
	
	public override void AIFollow()
	{
		base.AIFollow();

		if (Vector3.Distance (transform.position, pShip.transform.position) <= distanceFromPlayer)
			currentState = AIstate.AI_Strafe;
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
		if (Vector3.Distance (transform.position, pShip.transform.position) > distanceFromPlayer || Vector3.Distance (transform.position, pShip.transform.position) < distanceFromPlayer) 
			transform.position = ((transform.position - pShip.transform.position).normalized * distanceFromPlayer + pShip.transform.position);

		transform.RotateAround (pShip.transform.position, Vector3.forward, -(strafeSpeed/4));
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
