using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class EndLevel : MonoBehaviour {

    public enum EndCondition { KillAll, DestroyObject, Nothing };
    public EndCondition curEndCondition;
    //private GameObject[] enemyShips;
    public List<GameObject> destroyObjects;

    void OnEnable()
    {
        GameManager.Instance.currentEndLevel = this;
        destroyObjects.Clear();
    }


    public void AddDestroyObject(GameObject obj)
    {
        if (!destroyObjects.Contains(obj))
            destroyObjects.Add(obj);
    }

    public void CheckWinCondition()
    {
        
        if(curEndCondition == EndCondition.KillAll)
        {
            GameObject[]  enemyShips = GameObject.FindGameObjectsWithTag("Enemy");
            Debug.Log(enemyShips.Length);
            foreach(GameObject ship in enemyShips)
            {
                Debug.Log(ship.name);
                Selection.activeObject = ship.gameObject;
            }
           
            if (enemyShips.Length <= 0)
            {
                Debug.Log("Level Complete");
                GameObject.FindObjectOfType<LevelCreator>().LoadNextMission();
            }
        }

        if (curEndCondition == EndCondition.DestroyObject)
        {
            if(destroyObjects.Count==0)
            {
                Debug.Log("Level Complete");
                GameObject.FindObjectOfType<LevelCreator>().LoadNextMission();
            }
                
        }
    }
}
