using UnityEngine;
using System.Collections;

public class OrbitalRefinery : AIController {

	public int rawMaterials;
	private int damageTaken;
	public GameObject material;
	public GameObject newMat;
	private bool tooClose;
	private Vector3 randomPos;

	// Use this for initialization
	public override void Start () {
		base.Start();
		maxHealth = 1500;
		health = maxHealth;
	}
	
	private void dropMaterial()
	{
		Gatherer gather;
		
		tooClose = true;
		
		while (tooClose)
		{
			randomPos = Random.insideUnitSphere * 5;
			
			if (Vector3.Distance(transform.position, randomPos) <= 5)
				tooClose = true;
			else
				tooClose = false;
		}
		
		newMat = Instantiate(material, transform.position + randomPos, Quaternion.identity) as GameObject;
		
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			if (gather = go.GetComponent<Gatherer>())
			{
				if (gather.matHeld == null)
				{
					gather.GetRawMaterial(newMat);
					newMat.GetComponent<Collectible>().beingCollected = gather.gameObject;
				}
			}
		}
		rawMaterials -= 1;
	}
	
	public override void AIStationary()
	{
		transform.Rotate(new Vector3(0, 0, speed));
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{	
		if (other.tag == "EnemyBullet")
		{
			TakeDamage (10);
			damageTaken += 10;
			
			if (damageTaken == 100)
			{
				dropMaterial();
				damageTaken = 0;
			}
			
			if (health <= 0)
			{
				this.gameObject.SetActive(false);
				health = maxHealth;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
