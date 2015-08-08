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
	
	private AIController findClosestEnemy()
	{
		AIController temp = null;
	
		foreach (AIController c in GameObject.FindObjectsOfType<AIController>())
		{
			if (!c.gameObject.activeSelf)
				continue;
				
			if (temp == null)
				temp = c;
				
			if (temp != c && (Vector2.Distance(c.gameObject.transform.position, this.gameObject.transform.position) < Vector2.Distance(temp.gameObject.transform.position, this.gameObject.transform.position)))
				temp = c;
		}
		
		return temp;
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
		
		if (health < (maxHealth * 0.3f))
			currentState = AIstate.AI_Retreat;
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