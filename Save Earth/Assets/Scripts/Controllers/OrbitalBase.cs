using UnityEngine;
using System.Collections;

public class OrbitalBase : StationaryStation {

	public enum BaseType { Ally, Enemy };
	public BaseType baseType;
	
	// Use this for initialization
	public override void Start () {
		base.Start ();
		
		health = 500;
		EventManager.searchOrbitals += returnStation;
	}
		
	public override void TakeDamage(int amt)
	{
		if (baseType == BaseType.Ally)
		{
			this.health -= amt;
			
			if (this.health <= 0)
			{
				this.health = 1;
			}
		}
	}
	
	void returnStation(string type)
	{
		if (type == "OrbitalBase" && this.type == stationType.OrbitalBase)
		{
				EventManager.Instance.theOrbitals.Add(this);
		}
	}
	
	
	public override void OnTriggerEnter2D(Collider2D other)
	{
		if (this.baseType == BaseType.Ally && other.tag == "EnemyBullet")
		{
			TakeDamage(5);
		}
	}
}
