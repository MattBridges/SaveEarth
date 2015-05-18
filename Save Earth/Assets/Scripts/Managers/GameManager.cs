using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
   

    #region Singlton Block
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }
#endregion  

    #region Variables

    private List<GameObject> objects = new List<GameObject>();
    public GameObject player;
    public Camera mainCam;
    public GameObject currentMission;
    public string currentZone;
    public int currentMissionNum;
    public int playerLives;
    public EndLevel currentEndLevel;
    #endregion

    #region Methods
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
            PlayerShip.Instance.RespawnPlayer();
        }
    }
    public void StartLevel()
    {
        PlayerShip.Instance.SpawnPlayer();
        Debug.Log("Started Level");
    }
    #endregion

    void Start()
    {
        player.GetComponent<PlayerShip>().SpawnPlayer();
        TogglePause();
    }

	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			TogglePause ();
		}
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartLevel();
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            TestMethod(true);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            TestMethod();
        }
	}
    void TestMethod(bool testBool = false)
    {
        if(testBool)
        {
            Debug.Log("TestBool is true");
        }
        if(!testBool)
        {
            Debug.Log("TestBool is default/false");
        }
    }
}
