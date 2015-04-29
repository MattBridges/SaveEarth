using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
    //public Transform[] earthLevels;
   // public List<GameObject> earthLevels;
    public GameObject[] earthMission1;
    public GameObject[] earthMission2;
    public GameObject[] earthMission3;


	// Use this for initialization
	void Start () {
        RandomLevel(earthMission1);
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(RandomLevel(earthMission1).name);
        }
	}
    public GameObject RandomLevel(GameObject[] Mission)
    {
        
        int randMission = Random.Range(0, Mission.Length);
        return Mission[randMission];

    }
}
