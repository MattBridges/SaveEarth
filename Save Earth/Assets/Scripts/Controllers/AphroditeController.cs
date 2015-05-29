using UnityEngine;
using System.Collections;

public class AphroditeController : AIController {

	public bool canWake;
	
	[HideInInspector]
	public int preciousResources;

	public int evacuees;
	
	// Use this for initialization
	public override void Start () 
	{
		base.Start ();
		attack = false;	
		Dropzone.evac += Evacuate;
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
	
	void Evacuate(HerculesController herc)
	{
		herc.crewMembers--;
		evacuees++;
		
		if (evacuees >= 20)
		{
			rb.isKinematic = false;
			currentState = AIstate.AI_Patrol;
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
