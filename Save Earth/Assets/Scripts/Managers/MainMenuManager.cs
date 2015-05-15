using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {
    #region Singlton Block
    private static MainMenuManager _instance;
    public static MainMenuManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MainMenuManager>();
            }
            return _instance;
        }
    }
    #endregion 

    #region Variables
    public GameObject menuImage;
     #endregion

    #region Methods
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
        LevelCreator.Instance.LoadRandomMission(str[0], num);
    }
    #endregion

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleMenu();
    }
}
