using UnityEngine;
using System.Collections;

public class DragonflyShip : AIController {

	//private AIController controller;
	public float moveSpeed;
	public int health;
	//public Ship thisShip;

    // Use this for initialization
    public override void Start () {

	//	thisShip = new Ship(moveSpeed, health);	
		base.Start ();
		currentState = AIstate.AI_Follow;
    }

    // Update is called once per frame
    void Update () {

    }
}
