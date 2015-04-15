using UnityEngine;
using System.Collections;

public class AIController : Ship {
	
	public enum AIstate { AI_Idle, AI_Follow, AI_Retreat, AI_Stationary };
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

	// Use this for initialization
	public virtual void Start () 
	{
		
	}

	void OnEnable()
	{
		pShip = GameObject.FindGameObjectWithTag ("Player");

		if (!pShip) 
		{
			Debug.Log ("Error: No player ship found");
		}
	}

	private void UpdateRotation()
	{
		dir = pShip.transform.position - transform.position;
		angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		angle -= 90;

		if (currentState == AIstate.AI_Follow) 
		{
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
		if (attack && pShip) 
		{
			cDir = pShip.transform.position - transform.position;

			if ((Time.time - lastFired) < fireRate )
				canFire = false;
			else
				canFire = true;

			if (canFire)
			{
				lastFired = Time.time;
                FireCannon( bulletSpeed, null, null, weaponShotPosition, cDir, true, bulletColor, "EnemyBullet");
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		UpdateRotation();

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
   public void TakeDamage(int amt)
    {
        this.health -= amt;
        if (this.health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
