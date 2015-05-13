using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {
    public GameObject menuImage;
    private LevelCreator levelCreator;
    private GameManager gm;
    private MainMenuManager mManager;
    public PlayerShip player;

	// Use this for initialization
	void Awake () {
        levelCreator = GameObject.FindObjectOfType<LevelCreator>();

        
        


	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleMenu();
	}
    public void ToggleMenu()
    {
        if (menuImage.activeSelf)
            menuImage.SetActive(false);
        else
            menuImage.SetActive(true);
        
    }
    public void LoadLevel(string level)
    {
        
        string[] str = level.Split(' ');
        int num = int.Parse(str[1]);
        GameManager.Instance.currentZone = str[0];
        GameManager.Instance.currentMissionNum = num;
        GameManager.Instance.playerLives = 3;
        UIManager.Instance.UpdatePlayerLivesText();
        ToggleMenu();
        GameManager.Instance.TogglePause();        
        PlayerShip.Instance.SpawnPlayer();
        levelCreator.LoadRandomMission(str[0], num);
    }
}
