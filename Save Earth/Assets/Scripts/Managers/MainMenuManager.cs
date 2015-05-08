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
        gm = GameObject.FindObjectOfType<GameManager>();
        mManager = GameObject.FindObjectOfType<MainMenuManager>();
        
        


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
        gm.playerLives = 3;
        GameObject.FindObjectOfType<UIManager>().UpdatePlayerLivesText();
        mManager.ToggleMenu();
        gm.TogglePause();        
        player.SpawnPlayer();
        levelCreator.LoadRandomMission(str[0], num);
    }
}
