using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {
	
	public enum AIstate { AI_Idle, AI_Follow, AI_Retreat, AI_Stationary };
	public AIstate currentState;
	public bool attack = false;
	public GameObject pShip;
	
	// Use this for initialization
	public virtual void Start () 
	{
		Debug.Log ("Called base");
	}

	void OnEnable()
	{
		pShip = GameObject.FindGameObjectWithTag ("Player");

		if (!pShip) 
		{
			Debug.Log ("Error: No player ship found");
		}
	}

	public virtual void AIFollow()
	{
		if (pShip) 
		{
			if (Vector3.Distance (transform.position, pShip.transform.position) > 2)
				transform.position = Vector3.Lerp (transform.position, pShip.transform.position, Time.fixedDeltaTime);
		}
	}

	public virtual void AIRetreat()
	{

	}
	
	public virtual void AIAttack()
	{

	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		switch (currentState) 
		{
			case AIstate.AI_Idle:
				break;
			case AIstate.AI_Follow:
				AIFollow();
				AIAttack();
				break;
			case AIstate.AI_Retreat:
				AIRetreat();
				AIAttack();
				break;
			case AIstate.AI_Stationary:
				AIAttack();
				break;
			default:
				break;
		}
	}
}
