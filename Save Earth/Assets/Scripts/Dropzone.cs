using UnityEngine;
using System.Collections;

public class Dropzone : MonoBehaviour {

	public delegate void Evacuate(HerculesController herc);
	public static event Evacuate evac;

	private float theTime;
	private float timer;
	public HerculesController herc;
	private bool evacuating;

	// Use this for initialization
	void Start () {
		timer = 5;
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Hercules")
		{
			herc = other.gameObject.GetComponent<HerculesController>();
			theTime = Time.time;
			evacuating = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Hercules")
		{
			herc = null;
			evacuating = false;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (herc != null)
		{
			if (evacuating)
			{
				if ((Time.time - theTime) > timer)
				{
					theTime = Time.time;
					evac(herc);
				}	
			}
		}
	}
}
