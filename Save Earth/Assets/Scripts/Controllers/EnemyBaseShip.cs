using UnityEngine;
using System.Collections;

public class EnemyBaseShip : AIController {

	public bool canWake;
		
	// Use this for initialization
	public override void Start () 
	{
		base.Start ();
		attack = false;
		health = 500;
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
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "PlayerBullet") {
			TakeDamage(5);
		}		
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
