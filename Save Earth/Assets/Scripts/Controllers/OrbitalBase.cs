using UnityEngine;
using System.Collections;

public class OrbitalBase : StationaryStation {

	public enum BaseType { Ally, Enemy };
	public BaseType baseType;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		
		health = 500;
	}
	
	public override void TakeDamage(int amt)
	{
		this.health -= amt;
		
		if (this.health <= 0)
		{
			this.health = 1;
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (this.baseType == BaseType.Ally && other.tag == "EnemyBullet" ||
			this.baseType == BaseType.Enemy && other.tag == "PlayerBullet")
		{
			TakeDamage(5);
		}
	}
}
