using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndLevel : MonoBehaviour {

    public enum EndCondition { KillAll, DestroyObject };
    public EndCondition curEndCondition;
    private GameObject[] enemyShips;
    public List<GameObject> destroyObjects;

    void OnEnable()
    {
        GameManager.Instance.currentEndLevel = this;
        destroyObjects.Clear();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (curEndCondition == EndCondition.KillAll)
            FindAllEnemyShips();
        if (curEndCondition == EndCondition.DestroyObject)
            DestroyShipCondition();
	}
    public void FindAllEnemyShips()
    {
        enemyShips = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemyShips.Length<=0)
        {
            GameObject.FindObjectOfType<LevelCreator>().LoadNextMission();
        }
    }
    public void DestroyShipCondition()
    {
        if(destroyObjects.Count == 0)
        {
            GameObject.FindObjectOfType<LevelCreator>().LoadNextMission();
        }
    }

    public void AddDestroyObject(GameObject obj)
    {
        if (!destroyObjects.Contains(obj))
            destroyObjects.Add(obj);
    }
}
