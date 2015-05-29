using UnityEngine;
using System.Collections;

public class Gatherer : AIController {

	[HideInInspector]
	public GameObject matHeld;
	private Vector3 spawnPoint;
	private GameObject currentLevel;
	private float dist;
	private float force;

	// Use this for initialization
	public override void Start () 
	{
		base.Start();
		maxHealth = 250;
		health = maxHealth;
		spawnPoint = transform.position;
		weaponShotPosition = transform.FindChild ("GathererCannon").gameObject.transform;
		currentLevel = GameManager.Instance.currentMission;
		updateTarget(currentLevel);
	}
	
	private void updateTarget(GameObject level)
	{
		GameObject temp = null;
		OrbitalRefinery oR = null;
		
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Station")) 
		{
			oR = go.GetComponent<OrbitalRefinery>();
			
			if (oR == null)
				continue;
				
			if (temp == null)
				temp = go;
			else if (Vector3.Distance (transform.position, go.transform.position) < Vector3.Distance (transform.position, temp.transform.position))
				temp = go;
			else
				continue;
		}
		
		target = temp;

		if (target)
			attack = true;
	}
	
	public override void AIFollow ()
	{
		if (target)
		{
			if (Vector3.Distance (transform.position, target.transform.position) > 4.0f)
				rb.AddForce((target.transform.position - transform.position).normalized * speed, ForceMode2D.Force);
			else if (Vector3.Distance(target.transform.position, transform.position) < 4.0 && target.tag != "Collectible")
				rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 2.0f);
			else
				rb.AddForce((target.transform.position - transform.position).normalized * speed, ForceMode2D.Force);
		}
		else
			currentState = AIstate.AI_Idle;
	}
	
	public override void AIRetreat()
	{
		rb.AddForce((spawnPoint - transform.position).normalized * speed);
		Vector3 dir = spawnPoint - transform.position;
		float angle = (Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg) - 90;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		
		if (Vector3.Distance(spawnPoint, transform.position) < 1)
		{
			dropItem(matHeld);
		}
	}
	
	private void dropItem(GameObject held)
	{
		matHeld = null;
		Destroy(held);
		updateTarget(currentLevel);
		currentState = AIstate.AI_Follow;
		attack = true;
	}
	
	public void GetRawMaterial(GameObject material)
	{
		target = material;
		attack = false;
	}
	
	private void CollectMe(GameObject obj)
	{
		matHeld = obj;
		currentState = AIstate.AI_Retreat;
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "PlayerBullet")
			TakeDamage(5);
		if (other.tag == "Collectible")
		{
			other.gameObject.GetComponent<Collectible>().isHeld = this.gameObject;
			other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			CollectMe(other.gameObject);
		}
	}
}
