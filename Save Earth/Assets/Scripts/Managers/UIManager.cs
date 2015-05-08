using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Text playerLives;
    public Text playerHealth;

    public void UpdatePlayerLivesText()
    {
        playerLives.text = "Player Lives: " + GameObject.FindObjectOfType<GameManager>().playerLives.ToString();
    }
    public void UpdatePlayerHealthText()
    {
        playerHealth.text = "Player Health: " + GameObject.FindObjectOfType<PlayerShip>().health.ToString();
    }
}
