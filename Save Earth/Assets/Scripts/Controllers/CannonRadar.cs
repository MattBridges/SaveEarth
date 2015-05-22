using UnityEngine;
using System.Collections;

public class CannonRadar : MonoBehaviour {

	private OrbitalBaseCannon cannon;

	// Use this for initialization
	void Start () {
		cannon = transform.parent.GetComponent<OrbitalBaseCannon>();
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
			cannon.UpdateTarget(other.gameObject);
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player" && cannon.target == other.gameObject)
			cannon.UpdateTarget(null);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
