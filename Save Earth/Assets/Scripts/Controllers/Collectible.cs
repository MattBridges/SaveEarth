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
		
	public SpriteRenderer sprite;
	
	// Use this for initialization
	void Awake () {
		sprite = this.gameObject.GetComponent<SpriteRenderer>();		
		EventManager.collectIt += collectMe;
		EventManager.dropIt += dropMe;
	}
	
	public virtual void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy")
		{
			sprite.enabled = false;
			isHeld = other.gameObject;
		} 

		if (type == CollectTypes.Towable)
		{
			beingTowed = true;
			towedBy = other.gameObject;
		}
	}
	
	void collectMe(GameObject collector, GameObject item)
	{
		if (item == this.gameObject)
		{
			isHeld = collector;
			sprite.enabled = false;
		}
	}

	void dropMe(GameObject collector)
	{
		if (collector == isHeld)
		{
			isHeld = null;
			this.gameObject.SetActive(false);
			sprite.enabled = true;
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
