using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
   
    public PlayerShip player;
	private List<GameObject> objects = new List<GameObject>();
    public Camera mainCam;
    public GameObject currentMission;
    public int playerLives;
	   
	// Use this for initialization
	void Start () {
         
        player.SpawnPlayer();
        TogglePause();
	}

	public void TogglePause()
	{
		objects.Add(GameObject.FindGameObjectWithTag("Player"));
		objects.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
		objects.AddRange(GameObject.FindGameObjectsWithTag("EnemyBullet"));
		objects.AddRange(GameObject.FindGameObjectsWithTag("PlayerBullet"));
		objects.AddRange(GameObject.FindGameObjectsWithTag("AllyBullet"));

		foreach (GameObject o in objects) 
		{
			o.SendMessage ("TogglePause", SendMessageOptions.DontRequireReceiver);
		}

		objects.Clear();
	}
    public void LoseALife()
    {
        playerLives--;
        GameObject.FindObjectOfType<UIManager>().UpdatePlayerLivesText();
        if(playerLives<=0)
        {
            TogglePause();
            GameObject.FindObjectOfType<MainMenuManager>().ToggleMenu();
        }
        else
        {
            player.RespawnPlayer();
        }
    }
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			TogglePause ();
		}
	}

}
