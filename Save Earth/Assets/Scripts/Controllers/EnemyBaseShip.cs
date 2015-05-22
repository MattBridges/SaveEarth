using UnityEngine;
using System.Collections;

public class EnemyBaseShip : AIController {

	public bool canWake;
	
//	public Sprite[] shipSprites;
//	private SpriteRenderer sprite;
	
	// Use this for initialization
	public override void Start () 
	{
		base.Start ();
		attack = false;
		health = 500;
//		weaponShotPosition = transform.FindChild ("DragonflyCannon").gameObject.transform;
//		target = pShip;
		
//		sprite = this.gameObject.GetComponent<SpriteRenderer>();
//		shipSprites = Resources.LoadAll<Sprite>("Ships/RedShips/RD2");
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
		
//		if (hasMothershipShield)
//			sprite.sprite = shipSprites[1];
//		else
//			sprite.sprite = shipSprites[0];
	}
	
	void OnDisable()
	{
		health = maxHealth;
	}
}
