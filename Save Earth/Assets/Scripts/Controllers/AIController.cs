using UnityEngine;
using System.Collections;

public class AIController : Ship {
	
	public enum AIstate { AI_Idle, AI_Follow, AI_Retreat, AI_Stationary, AI_Patrol };
	public enum AIsubState { AI_Idle, AI_Attack, AI_Strafe, AI_Assist, AI_Defend };
	public AIstate currentState;
	public AIsubState currentSubState;
	
	public float bulletSpeed;
	public float fireRate;
	public float wakeupDistance;
	public float maxHealth;
    public HealthManager healthManager;
	
	private Vector3 dir;
	private float angle;
	private float lastFired;
	private Vector3 cDir;
	private bool canFire;
    private GameObject targetingPointer;
	
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
    public virtual void Awake()
    {
        targetingPointer = UIManager.Instance.RegisterPointer(this.gameObject);
    }
	void ResetTarget()
	{
		target = null;
	}

    public virtual void OnEnable()
	{
		if(PlayerShip.Instance!=null)
            pShip = PlayerShip.Instance.gameObject;
       
        healthManager = gameObject.GetComponentInChildren<HealthManager>();
      

		EventManager.rT += ResetTarget;
        
		if (!pShip) 
		{
			Debug.LogError ("No player ship found");
		}
        

        
	}

	public virtual void OnDisable()
	{
		EventManager.rT -= ResetTarget;
       
        
        
        
	}
    
  
	public void UpdateRotation(Transform faceDir, float tAngle)
	{
		dir = faceDir.position - transform.position;
		angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		angle -= tAngle;

		if (currentState == AIstate.AI_Follow || currentSubState == AIsubState.AI_Strafe || currentState == AIstate.AI_Patrol) 
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
            Vector3 dDir = cDir.normalized;

			if ((Time.time - lastFired) < fireRate)
				canFire = false;
			else
				canFire = true;

            if (canFire)
            {
                lastFired = Time.time;
               
                FireCannon (currentWeapon, weaponShotPosition, dDir, "EnemyBullet", this.gameObject);
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

            UpdateRotation(nextNode, 0);
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
				UpdateRotation(target.transform, 0);

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
				case AIstate.AI_Patrol:
					AIPatrol();
					break;
				default:
					break;
			}

			switch (currentSubState)
			{
				case AIsubState.AI_Idle:
					break;
				case AIsubState.AI_Defend:
					AIDefend ();
					break;
				case AIsubState.AI_Strafe:
					AIStrafe ();
					break;
				case AIsubState.AI_Assist:
					AIAssist();
					break;
				case AIsubState.AI_Attack:
					AIAttack();
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
       

        healthManager.SetWidth(health / maxHealth);
        if (this.health <= 0)
        {
            if (GameManager.Instance.currentEndLevel.destroyObjects.Contains(this.gameObject))
            {
                GameManager.Instance.currentEndLevel.destroyObjects.Remove(this.gameObject);
                
            }
            DropItem();
            this.gameObject.SetActive(false);            
            GameManager.Instance.currentEndLevel.CheckWinCondition(); 
            
        }
        
    }
    public void DropItem()
    {
        ItemDrop item = this.gameObject.GetComponent<ItemDrop>();
        if(item !=null)
        {
            GameObject obj = item.DropItem(item.dropType);
            obj.transform.position = this.gameObject.transform.position;
            obj.SetActive(true);
        }
    }
}
