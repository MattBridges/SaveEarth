using UnityEngine;
using System.Collections;

public class AIController : Ship {
	
	public enum AIstate { AI_Idle, AI_Follow, AI_Retreat, AI_Stationary, AI_Defend, AI_Strafe, AI_Assist, AI_Patrol };

	public AIstate currentState;
	
	public float bulletSpeed;
	public float fireRate;
	public float wakeupDistance;
	public int maxHealth;
	
	private Vector3 dir;
	private float angle;
	private float lastFired;
	private Vector3 cDir;
	private bool canFire;
	
	[HideInInspector]
	public Transform nextNode;
	
	
	public int pathNodeGroup;
	
	[HideInInspector]
    public Color bulletColor = Color.magenta;
    
    [HideInInspector]
	public bool paused;
	
	[HideInInspector]
	public Rigidbody2D rb;

	[HideInInspector]
	public bool hasMothershipShield;
	
	[HideInInspector]
	public float mothershipShieldDelay;
	
	[HideInInspector]
	public int shieldHealth;
	
	[HideInInspector]
	public float shieldTime;

	[HideInInspector]
	public GameObject target;

	[HideInInspector]
	public bool attack = false;
	
	[HideInInspector]
	public GameObject pShip;
	
	[HideInInspector]
	public Transform weaponShotPosition;

	// Use this for initialization
	public virtual void Start () 
	{		
		health = maxHealth;
        currentWeapon = Weapons.RedWeapon;
		rb = this.gameObject.GetComponent<Rigidbody2D>();
	}

	void ResetTarget()
	{
		target = null;
	}

    void OnEnable()
	{
		pShip = PlayerShip.Instance.gameObject;

		EventManager.rT += ResetTarget;
        EventManager.loadLvl += AddEndConditionObj;
		if (!pShip) 
		{
			Debug.Log ("Error: No player ship found");
		}
	}

	void OnDisable()
	{
		EventManager.rT -= ResetTarget;
        EventManager.loadLvl -= AddEndConditionObj;
        endCondidtionObject = false;
	}
    
    void AddEndConditionObj()
    {
        if (this.endCondidtionObject)
            GameManager.Instance.currentEndLevel.AddDestroyObject(this.gameObject);
    }
	public void UpdateRotation(Transform faceDir, float tAngle)
	{
		dir = faceDir.position - transform.position;
		angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		angle -= tAngle;

		if (currentState == AIstate.AI_Follow || currentState == AIstate.AI_Strafe || currentState == AIstate.AI_Patrol) 
		{
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}

	public virtual void AIFollow()
	{
		if (target) 
		{
			if (Vector3.Distance (transform.position, target.transform.position) > 3.5)
			{
				//transform.position = Vector3.Lerp (transform.position, pShip.transform.position, (speed * Time.fixedDeltaTime));
				rb.AddForce((target.transform.position - transform.position).normalized * speed, ForceMode2D.Force);
			}
		}
	}

	public virtual void AIRetreat()
	{

	}
	
	public virtual void AIAssist()
	{
	
	}
	
	public virtual void AIAttack()
	{
		if ((attack && target) && target.activeSelf) 
		{
			cDir = target.transform.position - transform.position;

			if ((Time.time - lastFired) < fireRate)
				canFire = false;
			else
				canFire = true;

			if (canFire) {
				lastFired = Time.time;
				FireCannon (currentWeapon, bulletSpeed, null, null, weaponShotPosition, cDir, true, bulletColor, this.gameObject.tag == "Ally" ? "AllyBullet" : "EnemyBullet", this.gameObject);
			}
		} 
	}

	public virtual void AIDefend()
	{

	}

	public virtual void AIStationary()
	{

	}

	public virtual void AIStrafe()
	{

	}

	public virtual void AIIdle()
	{

	}

	public virtual void AIPatrol()
	{
        if (PathNodeManager.Instance.GetNodeGroup(pathNodeGroup) != null)
        {
            if (nextNode == null)
            {
                nextNode = PathNodeManager.Instance.GetNextNodePingPong(pathNodeGroup);
            }

            if (Vector3.Distance(transform.position, nextNode.position) > 1.0f)
                rb.AddForce((nextNode.position - transform.position).normalized * speed, ForceMode2D.Force);
            else
                nextNode = PathNodeManager.Instance.GetNextNodePingPong(pathNodeGroup);

            UpdateRotation(nextNode, 90);
        }
        else
        {
            Debug.LogError("Assignd Group is null for " + this.gameObject.name);
            //Put default back checked behavior here if group is null
        }
            
        
	}

	private void TogglePause()
	{
		paused = !paused;
	}

	// Update is called once per frame
	public virtual void FixedUpdate () 
	{
		if (!paused) 
		{
			if (target)
			{
				UpdateRotation(target.transform, 90);

				if (!target.activeSelf)
				{
					currentState = AIstate.AI_Idle;
					attack = false;
				}
			}
			
			switch (currentState) 
			{
				case AIstate.AI_Idle:
					AIIdle ();
					break;
				case AIstate.AI_Follow:
					AIFollow ();
					AIAttack ();
					break;
				case AIstate.AI_Retreat:
					AIRetreat ();
					AIAttack ();
					break;
				case AIstate.AI_Stationary:
					AIStationary ();
					AIAttack ();
					break;
				case AIstate.AI_Defend:
					AIDefend ();
					AIAttack ();
					break;
				case AIstate.AI_Strafe:
					AIStrafe ();
					AIAttack ();
					break;
				case AIstate.AI_Assist:
					AIAssist();
					break;
				case AIstate.AI_Patrol:
					AIPatrol();
					break;
				default:
					break;
			}
			
			if (hasMothershipShield)
			{
				if ((Time.time - shieldTime) > mothershipShieldDelay)
				{
					hasMothershipShield = false;	
				}
			}
		}
	}

    public virtual void TakeDamage(int amt)
    {
    	if (hasMothershipShield)
    	{
    		this.shieldHealth -= amt;
    		
    		if (shieldHealth <= 0)
    		{
    			this.health -= shieldHealth;
    			hasMothershipShield = false;
    		}
    	}
    	else
	        this.health -= amt;
	        
        if (this.health <= 0)
        {
            if (GameManager.Instance.currentEndLevel.destroyObjects.Contains(this.gameObject))
            {
                GameManager.Instance.currentEndLevel.destroyObjects.Remove(this.gameObject);
                GameManager.Instance.currentEndLevel.CheckWinCondition(); 
            }
                

               
            this.gameObject.SetActive(false);
        }
    }
}
