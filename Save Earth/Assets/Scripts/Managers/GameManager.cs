using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public GameObject playerRef;
    public PlayerShip player;
	// Use this for initialization
	void Start () {
       // playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShip>();
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

	}

}
