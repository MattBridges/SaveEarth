using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {
	
	public enum CollectTypes { RawMaterial };
	public CollectTypes type;
	
	public GameObject beingCollected;
	public GameObject isHeld;
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnEnterTrigger2D(Collider2D other)
	{
/*		if (other.tag == "Enemy")
		{
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
			isHeld = other.gameObject;
		} */
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{		
		if (isHeld)
		{
			transform.position = isHeld.transform.position;
		}
		else
			transform.Rotate(new Vector3(0, 0, 0.25f));
	}
}
