using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    #region Singlton Block
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
    #endregion 

    #region Variables
    public Text playerLives;
    public Text playerHealth;
   
    public GameObject pointer;

    #endregion

    #region Methods
    public void UpdatePlayerLivesText()
    {
        playerLives.text = "Player Lives: " + GameManager.Instance.playerLives.ToString();
    }
    public void UpdatePlayerHealthText()
    {
        playerHealth.text = "Player Health: " + PlayerShip.Instance.health.ToString();
    }


    #endregion
    

    public GameObject RegisterPointer(GameObject target)
    {
        GameObject obj = FindPointer();
        obj.GetComponent<ShipPointer>().target = target;
        return this.gameObject;

    }
    public GameObject FindPointer()
    {
        
        //foreach (GameObject pointr in pointers)
        //{
        //    if (!pointer.activeInHierarchy)
        //    {
        //        return pointer;
        //    }
        //}
  
       GameObject obj = Instantiate(pointer);
       obj.gameObject.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
       obj.SetActive(true);
       return obj;

    }
}
