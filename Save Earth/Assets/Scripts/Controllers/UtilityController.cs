using UnityEngine;
using System.Collections;

public class UtilityController : AIController {

	public enum subClass { Light, Medium, Heavy };
	public subClass SubClass;
	
	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	public override void FixedUpdate () {
		
	}
}
