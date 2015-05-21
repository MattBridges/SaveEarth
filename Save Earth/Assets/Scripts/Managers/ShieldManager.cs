using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShieldManager : MonoBehaviour {

	public float shieldRange = 5.0f;
	public int shieldHealth = 100;
	public float shieldDelay = 10.0f;
	private AIController tempAI;
	private List<AIController> AIlist;

	// Use this for initialization
	void Start () {
		transform.GetComponent<CircleCollider2D>().radius = shieldRange;
		AIlist = new List<AIController>();
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy" && other.gameObject != transform.parent.gameObject)
		{
			tempAI = other.gameObject.GetComponent<AIController>();

			if (!AIlist.Contains(tempAI))
			{
				AIlist.Add(tempAI);
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Enemy" && other.gameObject != transform.parent.gameObject)
		{
			tempAI = other.gameObject.GetComponent<AIController>();
			
			if (AIlist.Contains(tempAI))
			{
				AIlist.Remove(tempAI);
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		foreach (AIController ai in AIlist)
		{
			if (!ai.hasMothershipShield)
			{
				if ((Time.time - ai.shieldTime) > ai.mothershipShieldDelay)
				{
					ai.hasMothershipShield = true;
					ai.shieldTime = Time.time;	
					ai.shieldHealth = shieldHealth;
					ai.mothershipShieldDelay = shieldDelay;
				}
			}
		}	
	}
}
