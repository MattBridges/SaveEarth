using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Text playerLives;
    public Text playerHealth;
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIManager>();
            }
            return _instance;
        }
    }
    void Start()
    {

    }

    public void UpdatePlayerLivesText()
    {
        playerLives.text = "Player Lives: " + GameManager.Instance.playerLives.ToString();
    }
    public void UpdatePlayerHealthText()
    {
        playerHealth.text = "Player Health: " + PlayerShip.Instance.health.ToString();
    }
}
