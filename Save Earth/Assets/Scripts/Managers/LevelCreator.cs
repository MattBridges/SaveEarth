using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelCreator : MonoBehaviour {
    
    private ObjectPooler op;
    private PoolingManager pm;
    private GameObject[] nodes;

	// Use this for initialization
	void Start () {
        op = GameObject.Find("Pooler").GetComponent<ObjectPooler>();
        pm = GameObject.Find("PoolManager").GetComponent<PoolingManager>();

        nodes = GameObject.FindGameObjectsWithTag("SpawnNode");

        SpawnEnemies();
      
	}

    public void SpawnEnemies()
    {
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
