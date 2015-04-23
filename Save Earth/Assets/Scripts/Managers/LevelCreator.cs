using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelCreator : MonoBehaviour {
    public GameObject[] levels;
    private ObjectPooler op;
    private PoolingManager pm;
    private GameObject[] nodes;

	// Use this for initialization
	void Start () {
        op = GameObject.Find("Pooler").GetComponent<ObjectPooler>();
        pm = GameObject.Find("PoolManager").GetComponent<PoolingManager>();
        

        

        //SpawnEnemies();
      
	}
    public void LoadLevel(string LevelName)
    {
        foreach(GameObject level in levels)
        {
            level.SetActive(false);
   
        }
        foreach(GameObject level in levels)
        {
            if (level.name == LevelName)
            {
                level.SetActive(true);
                SpawnEnemies();
            }
        }
        
    }

    public void SpawnEnemies()
    {
        nodes = GameObject.FindGameObjectsWithTag("SpawnNode");

        foreach (GameObject node in nodes)
        {
            string[] ns = node.name.Split(' ');
            string nodeName = ns[0];

            if (nodeName == "DragonFlySpawn")
                SpawnDragonfly(node);
            if (nodeName == "RaptorSpawn")
                SpawnRaptor(node);
            if (nodeName == "MotherShipSpawn")
                SpawnMotherShip(node);
        }
    }
    public void SpawnDragonfly(GameObject Position)
    {
        GameObject ship = op.ReturnObject(pm.dragonFlies, pm.dfShip, pm.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnRaptor(GameObject Position)
    {
        GameObject ship = op.ReturnObject(pm.raptors, pm.rpShip, pm.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnMotherShip(GameObject Position)
    {
        GameObject ship = op.ReturnObject(pm.motherShips, pm.mShip, pm.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
}
