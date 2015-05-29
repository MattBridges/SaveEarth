﻿using UnityEngine;
using System.Collections;

public class StationaryStation : SpaceStation {


	public override void Start () {
		base.Start ();
		currentState = AIstate.AI_Stationary;
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
		base.AIAttack();
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{

	}
	
	public override void OnDisable()
	{
		base.OnDisable ();        
	}

}
