using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour {

    public enum EndCondition { KillAll, DestroyObject };
    public EndCondition curEndCondition;
    private GameObject[] enemyShips;
    public GameObject destroyObject;

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
        if(destroyObject == null)
        {
            Debug.Log("No End Condition Object Set");
        }
        if(destroyObject!=null && !destroyObject.activeInHierarchy)
        {
            GameObject.FindObjectOfType<LevelCreator>().LoadNextMission();
        }
    }
}
