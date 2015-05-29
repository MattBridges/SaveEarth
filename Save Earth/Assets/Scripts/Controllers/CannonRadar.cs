using UnityEngine;
using System.Collections;

public class CannonRadar : MonoBehaviour {

	public delegate void UpdateTarget(GameObject target);
	public static event UpdateTarget newTarget;
	
	private GameObject currentTarget;

	// Use this for initialization
	void Start () {
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			currentTarget = other.gameObject;
			newTarget(currentTarget);
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player" && currentTarget == other.gameObject)
		{
			currentTarget = null;
			newTarget(null);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
