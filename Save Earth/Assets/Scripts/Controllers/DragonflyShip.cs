using UnityEngine;
using System.Collections;

public class DragonflyShip : AIController {

	public float moveSpeed;
	public int newHealth;

    // Use this for initialization
    public override void Start () 
	{
		base.Start ();
		currentState = AIstate.AI_Follow;
		speed = moveSpeed;
		health = newHealth;
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
		if (other.gameObject.tag == "Bullet") {
			health -= 1;
		}
	}

    // Update is called once per frame
    void Update () {

    }
}
