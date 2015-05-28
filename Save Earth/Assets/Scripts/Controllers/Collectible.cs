using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {
	
	public enum CollectTypes { RawMaterial, Towable };
	public CollectTypes type;
	
	[HideInInspector]
	public GameObject beingCollected;

	[HideInInspector]
	public GameObject isHeld;

	[HideInInspector]
	public bool beingTowed;
	
	[HideInInspector]
	public GameObject towedBy;
		
	// Use this for initialization
	void Start () {
		
	}
	
	public virtual void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy")
		{
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
			isHeld = other.gameObject;
		} 

		if (type == CollectTypes.Towable)
		{
			beingTowed = true;
			towedBy = other.gameObject;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{		
		if (isHeld)
		{
			transform.position = isHeld.transform.position;
		}
		else if (beingTowed)
		{
			if (Vector3.Distance(towedBy.transform.position, transform.position) > 2)
				transform.position = Vector3.Slerp(transform.position, towedBy.transform.position, (Time.fixedDeltaTime * 2));
		}
		else
			transform.Rotate(new Vector3(0, 0, 0.25f));
	}
}
