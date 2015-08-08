using UnityEngine;
using System.Collections;

public class FighterController : AIController {

	public enum subClass { Light, Medium, Heavy };
	public subClass SubClass;
	
	public float detectionRange;
	
	public override void Start () 
	{
		base.Start();
	}
	
	public override void Awake() 
	{
		base.Awake();
	}
	
	private GameObject findClosestEnemy()
	{
		GameObject closest = null;
	
		foreach (GameObject c in PoolingManager.Instance.pooled)
		{
			if (!c.activeSelf)
				continue;
				
			if (closest == null)
				closest = c;
				
			if (closest != c && (Vector2.Distance(c.transform.position, this.gameObject.transform.position) < Vector2.Distance(closest.transform.position, this.gameObject.transform.position)))
				closest = c;
		}
		
		return closest;
	}
	
	public override void OnEnable() 
	{	
		base.OnEnable();
	}
	
	public override void OnDisable()
	{
		base.OnDisable();
	}
	
	public override void AIFollow()
	{
		base.AIFollow();
		
		if (health < (maxHealth * 0.3f))
			currentState = AIstate.AI_Retreat;
	}
	
	public override void AIRetreat()
	{
		base.AIRetreat();
	}
	
	public override void AIAssist()
	{
		base.AIAssist();
	}
	
	public override void AIAttack()
	{
		base.AIAttack();
	}
	
	public override void AIDefend()
	{
		base.AIDefend();
	}
	
	public override void AIStationary()
	{
		base.AIStationary();
	}
	
	public override void AIStrafe()
	{
		base.AIStrafe();
	}
	
	public override void AIIdle()
	{
		base.AIIdle();
		
		if (Vector2.Distance(PlayerShip.Instance.gameObject.transform.position, this.gameObject.transform.position) < wakeupDistance)
		{
			target = PlayerShip.Instance.gameObject;
			currentState = AIstate.AI_Follow;
		}
	}
	
	public override void AIPatrol()
	{
		base.AIPatrol();
	}
	
	public override void FixedUpdate () 
	{
		base.FixedUpdate();
	}
}