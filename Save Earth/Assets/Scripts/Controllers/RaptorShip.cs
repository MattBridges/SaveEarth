using UnityEngine;
using System.Collections;

public class RaptorShip: FighterController  {
    
	public float distanceFromPlayer;
	public bool canWake;
	public float strafeSpeed;
	
	private Vector2 impulse;
	private Vector2 direction;
	private SpriteRenderer sprite;
	
	[HideInInspector]
	public bool strafeDir;
		
//	[HideInInspector]
	public GameObject currentLevel;
	
	[HideInInspector]
	public float swapTarget;
	
	[HideInInspector]
	public Sprite[] shipSprites;
		
	// Ally specific variables

	[HideInInspector]
	public GameObject protectedSatellite;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		
		SubClass = subClass.Medium;
		
		weaponShotPosition = transform.FindChild("RaptorCannon").gameObject.transform;

		currentLevel = GameManager.Instance.currentMission;
		updateTarget(currentLevel);
		
		sprite = this.gameObject.GetComponent<SpriteRenderer>();
		shipSprites = Resources.LoadAll<Sprite>("Ships/RedShips/RD3");

		if (this.gameObject.tag == "Ally") 
		{
			updateProtected ();
		}
	}
	
	public override void AIFollow()
	{
		if (Vector3.Distance (transform.position, target.transform.position) > distanceFromPlayer)
		{
				//transform.position = Vector3.Lerp (transform.position, pShip.transform.position, (speed * Time.fixedDeltaTime));
				rb.AddForce((target.transform.position - transform.position).normalized * speed, ForceMode2D.Force);
		}

		if (Vector3.Distance(transform.position, target.transform.position) <= distanceFromPlayer) 
		{
			strafeDir = Random.Range(0, 100) > 50 ? true : false;
			currentState = AIstate.AI_Strafe;
			rb.velocity = Vector3.zero;
		}
	}
	
	public override void AIRetreat()
	{
		base.AIRetreat();
	}
	
	/*public override void AIAttack()
	{
		base.AIAttack();
	}*/

	public override void AIIdle()
	{
		if (target) 
		{
			if (Vector3.Distance (transform.position, target.transform.position) < wakeupDistance) 
			{
				attack = true;
				currentState = AIstate.AI_Follow;
			}
		}
	}

	public override void AIPatrol()
	{
		base.AIPatrol();
	}

	public override void AIStrafe()
	{
		direction = target.transform.position - transform.position;
		direction = direction / direction.magnitude;

		if (Vector3.Distance (transform.position, target.transform.position) > distanceFromPlayer)
			direction.x = 1;
		else
			direction.x = 0;

		if (Vector3.Distance (transform.position, target.transform.position) < distanceFromPlayer) 
		{
			direction.x = -1;
		} 
		else 
			direction.x = 0;
			
		direction.y = strafeDir ? 1 : -1;

		rb.AddRelativeForce (direction * strafeSpeed, ForceMode2D.Force);

		if (Vector3.Distance (transform.position, target.transform.position) > distanceFromPlayer + 4) 
		{
			currentState = AIstate.AI_Follow;
		}
	}

	private void updateTarget(GameObject level)
	{
		GameObject temp = null;
		
		if (level != null)
		{
			if (level.name == "Level1_1") 
			{
				foreach (GameObject go in GameObject.FindGameObjectsWithTag("Satellite")) 
				{
					if (temp == null)
						temp = go;
					else if (Vector3.Distance (transform.position, go.transform.position) < Vector3.Distance (transform.position, temp.transform.position))
						temp = go;
					else
						continue;
				}
	
				if (!temp)
					target = pShip;
				else
					target = temp;
	
				attack = true;
			} 
			else
				target = pShip;
		}
	}

	private void updateProtected()
	{
		GameObject temp = null;
		
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Satellite")) 
		{
			if (temp == null)
				temp = go;
			else if (Vector3.Distance (transform.position, go.transform.position) < Vector3.Distance (transform.position, temp.transform.position))
				temp = go;
			else
				continue;
		}
			
		protectedSatellite = temp;
		protectedSatellite.GetComponent<SatelliteController>().protector = this.gameObject;
	}

	public override void FixedUpdate()
	{
		base.FixedUpdate();

		if (!target || !target.gameObject.activeSelf)
			updateTarget(currentLevel);

		if (swapTarget != 0) 
		{
			if ((Time.time - swapTarget) > 5)
			{
				updateTarget(currentLevel);
				swapTarget = 0;
			}
		}
		
		if (hasMothershipShield)
			sprite.sprite = shipSprites[1];
		else
			sprite.sprite = shipSprites[0];
		
	}

	public void CallAssist(GameObject newTarget)
	{
		target = newTarget;
		attack = true;
		currentState = AIstate.AI_Follow;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "PlayerBullet" && this.gameObject.tag == "Enemy") 
		{
			TakeDamage(5);
            if (currentState == AIstate.AI_Idle)
                currentState = AIstate.AI_Follow;

			if (currentLevel.name == "Level1_1")
			{
				swapTarget = Time.time;
				target = other.GetComponent<BulletDestroy>().shipFired;
			}
		}

		if (other.gameObject.tag == "EnemyBullet" && this.gameObject.tag == "Ally" || other.gameObject.tag == "AllyBullet" && this.gameObject.tag == "Enemy") 
		{
			TakeDamage(2);
			if (currentState == AIstate.AI_Idle)
				currentState = AIstate.AI_Follow;
			
			target = other.GetComponent<BulletDestroy>().shipFired;
		}
	}

	void OnDisable()
	{
		health = maxHealth;
	}
}
