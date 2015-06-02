using UnityEngine;
using System.Collections;

public class OrbitalBaseCannon : Ship {

	[HideInInspector]
	public GameObject target;
	private Vector3 cDir;
	private bool canFire;
	private float lastFired;
	public float fireRate;
	public float bulletSpeed;
	[HideInInspector]
	public Transform weaponShotPosition;
	[HideInInspector]
	public Color bulletColor = Color.red;
	public int maxHealth;
	public bool hackingEquip;

	// Use this for initialization
	void Start () {
		currentWeapon = Weapons.Cannon;
		health = maxHealth;
		CannonRadar.newTarget += UpdateTarget;
		EventManager.findCannons += findCannons;
	}

	void findCannons(GameObject orbBase)
	{
		if (orbBase == this.transform.parent.gameObject)	
			EventManager.Instance.cannons.Add(this);
	}
	
	void TakeDamage(int amt)
	{
		health -= amt;
		
		if (health <= 0)
			this.gameObject.SetActive(false);
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "PlayerBullet")
			TakeDamage(5);
	}
	
	private void UpdateRotation()
	{
		Vector3 dir;
		float angle;
		
		if (target)
		{
			dir = target.transform.position - transform.position;
			angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
		else if (this.gameObject.name == "Cannon 1")
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, 90), Time.fixedDeltaTime * 1.0f);
		else if (this.gameObject.name == "Cannon 2")
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, 180), Time.fixedDeltaTime * 1.0f);
		else if (this.gameObject.name == "Cannon 3")
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, 0), Time.fixedDeltaTime * 1.0f);
	}
	
	void UpdateTarget(GameObject newTarget)
	{
		target = newTarget;
	}
	
	private void AttackTarget(GameObject theTarget)
	{
		if (target && target.activeSelf) 
		{
			cDir = target.transform.position - transform.position;
            Vector3 dDir = cDir.normalized;
			
			if ((Time.time - lastFired) < fireRate)
				canFire = false;
			else
				canFire = true;
			
			if (canFire) {
				lastFired = Time.time;
                FireCannon(currentWeapon, weaponShotPosition, dDir,  "EnemyBullet", this.gameObject);
			}
		}	
	}
	
	void FixedUpdate () 
	{
		if (!hackingEquip)
		{
			UpdateRotation();
		
			if (target)
				AttackTarget(target);
		}
	}
}
