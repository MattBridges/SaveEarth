using UnityEngine;
using System.Collections;

public class AphroditeController : AIController {

	public bool canWake;
	public int preciousResources;
	
	// Use this for initialization
	public override void Start () 
	{
		base.Start ();
		attack = false;		
	}
	
	public override void AIFollow()
	{
		base.AIFollow();
		
	}
	
	public override void AIRetreat()
	{
		base.AIRetreat();
	}
	
	public override void AIAttack()
	{
		
	}
	
	public override void AIStationary()
	{

	}
	
	public override void AIPatrol()
	{
		base.AIPatrol();
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "EnemyBullet") {
			TakeDamage(5);
		}		
	}
	
	public void checkResources()
	{
		if (preciousResources >= 10)
			currentState = AIstate.AI_Patrol;
	}	
	
	public override void FixedUpdate ()
	{
		base.FixedUpdate();
	}
	
	void OnDisable()
	{
		health = maxHealth;
	}

}
