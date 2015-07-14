using UnityEngine;
using System.Collections;

public class OrbitalRefinery : StationaryStation {

	public int rawMaterials;
	private int damageTaken;
	public GameObject material;
	
	[HideInInspector]
	public GameObject newMat;
	private bool tooClose;
	private Vector3 randomPos;

	// Use this for initialization
	public override void Start () {
		base.Start();
		EventManager.searchStations += returnStation;
		maxHealth = 1500;
		health = maxHealth;
	}
	
	private void dropMaterial()
	{
//		Gatherer gather;		
		GameObject newMat = ObjectPooler.Instance.ReturnObject("RawMaterial");

		randomPos = Random.insideUnitSphere * 5;		
		newMat.transform.position = (transform.position + randomPos);
		newMat.SetActive(true);
		
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
		{
//			if (gather = go.GetComponent<Gatherer>())
//			{
//				if (gather.matHeld == null)
//				{
//					gather.GetRawMaterial(newMat);
//					newMat.GetComponent<Collectible>().beingCollected = gather.gameObject;
//				}
//			}
		}
		rawMaterials -= 1;
	}
	
	void returnStation(string type)
	{
		if (type == "Refinery" && this.type == stationType.Refinery)
		{
			EventManager.Instance.theStations.Add(this.gameObject);
		}
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
