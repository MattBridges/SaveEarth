using UnityEngine;
using System.Collections;

public class AIController : Ship {
	
	public enum AIstate { AI_Idle, AI_Follow, AI_Retreat, AI_Stationary, AI_Defend, AI_Strafe, AI_Assist };

	public AIstate currentState;

	public bool attack = false;

	public GameObject pShip;

	private Vector3 dir;
	private float angle;
	public Transform weaponShotPosition;
	public float bulletSpeed;
	private Vector3 cDir;
	private bool canFire;
	public float fireRate;
	private float lastFired;
    public Color bulletColor = Color.magenta;
	public bool paused;
	public Rigidbody2D rb;

	public bool hasMothershipShield;
	public float mothershipShieldDelay;
	public int shieldHealth;
	public float shieldTime;

	public float wakeupDistance;

	public int maxHealth;

	public GameObject target;

	// Use this for initialization
	public virtual void Start () 
	{
		health = maxHealth;
        currentWeapon = Weapons.RedWeapon;
		rb = this.gameObject.GetComponent<Rigidbody2D>();
	}

    void OnEnable()
	{
		pShip = GameObject.FindGameObjectWithTag ("Player");

		if (!pShip) 
		{
			Debug.Log ("Error: No player ship found");
		}
        if(endCondidtionObject)
        {
            GameManager.Instance.currentEndLevel.RegisterAsEndCondition(this.gameObject);
        }
	}

	private void UpdateRotation()
	{
		dir = target.transform.position - transform.position;
		angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		angle -= 90;

		if (currentState == AIstate.AI_Follow || currentState == AIstate.AI_Strafe) 
		{
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}

	public virtual void AIFollow()
	{
		if (pShip) 
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
				UpdateRotation ();

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

    public void TakeDamage(int amt)
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
            this.gameObject.SetActive(false);
        }
    }
}
