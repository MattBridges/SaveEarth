using UnityEngine;
using System.Collections;

public class DragonflyShip : AIController {

	public float rushDistance;
	public bool canWake;

    // Use this for initialization
    public override void Start () 
	{
		base.Start ();
		attack = false;
		weaponShotPosition = transform.FindChild ("DragonflyCannon").gameObject.transform;
    }

	public override void AIFollow()
	{
		base.AIFollow();

		if (Vector3.Distance (transform.position, pShip.transform.position) < rushDistance)
			speed = 5;

		transform.position = Vector3.Lerp (transform.position, pShip.transform.position, (speed * Time.fixedDeltaTime));
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
		if (Vector3.Distance (transform.position, pShip.transform.position) < wakeupDistance)
			currentState = AIstate.AI_Follow;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "PlayerBullet") {
			currentState = AIstate.AI_Follow;
            TakeDamage(5);
		}

		if (other.gameObject.tag == "Player") 
			this.gameObject.SetActive (false);
	}
	
    void OnDisable()
    {
       health = maxHealth;
    }
}
