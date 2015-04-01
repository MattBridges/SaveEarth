using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
   
    public PlayerShip player;
   

    
	// Use this for initialization
	void Start () {
         
        player.SpawnPlayer();
        
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.A))
        {
           player.SpawnPlayer();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.RespawnPlayer();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.TakeDamage();
        }

	}

}
