using UnityEngine;
using System.Collections;

public class DragonflyShip : CorvetteController {

	public float rushDistance;
	public bool canWake;
	
	[HideInInspector]
	public Sprite[] shipSprites;
	
	private SpriteRenderer sprite;

    // Use this for initialization
    public override void Start () 
	{
		base.Start ();
		SubClass = subClass.Heavy;
		attack = false;
		weaponShotPosition = transform.FindChild ("DragonflyCannon").gameObject.transform;
		target = pShip;
		
		sprite = this.gameObject.GetComponent<SpriteRenderer>();
		shipSprites = Resources.LoadAll<Sprite>("Ships/RedShips/RD2");
    }

	public override void AIFollow()
	{
		base.AIFollow();

		if (target)
		{
			if (Vector3.Distance(transform.position, target.transform.position) < rushDistance)
				speed = 5;
	
			//transform.position = Vector3.Lerp (transform.position, pShip.transform.position, (speed * Time.fixedDeltaTime));
			rb.AddForce((target.transform.position - transform.position) * speed);
		}
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
		if (target)
		{
			if (Vector3.Distance (transform.position, target.transform.position) < wakeupDistance)
				currentState = AIstate.AI_Follow;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "PlayerBullet") {
			currentState = AIstate.AI_Follow;
            TakeDamage(5);
		}

		if (other.gameObject.tag == "Player")
            TakeDamage(500);
	}
	
	public override void FixedUpdate ()
	{
		base.FixedUpdate();
		
		if (hasMothershipShield)
			sprite.sprite = shipSprites[1];
		else
			sprite.sprite = shipSprites[0];
	}
	
    void OnDisable()
    {
       health = maxHealth;
    }
}
