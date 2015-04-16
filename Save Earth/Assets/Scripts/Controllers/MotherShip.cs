using UnityEngine;
using System.Collections;

public class MotherShip : AIController {

	// Use this for initialization
	public override void Start () {
		base.Start ();
		currentState = AIstate.AI_Follow;
		attack = true;
		weaponShotPosition = transform.FindChild ("MothershipCannon").gameObject.transform;
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
		base.AIAttack();
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "PlayerBullet") {
			TakeDamage(5);
		}
	}

	void OnDisable()
	{
		health = maxHealth;
	}
}
