using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelCreator : MonoBehaviour {
    
    private GameObject[] dfNodes;
    private GameObject[] rapNodes;
    private GameObject[] motNodes;
    private ObjectPooler op;
    private PoolingManager gm;
  //  private GameObject[] nodes;
	// Use this for initialization
	void Start () {
        op = GameObject.Find("Pooler").GetComponent<ObjectPooler>();
        gm = GameObject.Find("PoolManager").GetComponent<PoolingManager>();
       
        dfNodes = GameObject.FindGameObjectsWithTag("DragonFlySpawn");
        rapNodes = GameObject.FindGameObjectsWithTag("RaptorSpawn");
        motNodes = GameObject.FindGameObjectsWithTag("MotherShipSpawn");
        
        DistributeShip(dfNodes);
        DistributeShip(rapNodes);
        DistributeShip(motNodes);

	}

    public void DistributeShip(GameObject[] node)
    {
        foreach(GameObject n in node)
        {
            if (n.tag == "DragonFlySpawn")
                SpawnDragonfly(n);
            if (n.tag == "RaptorSpawn")
                SpawnRaptor(n);
            if (n.tag == "MotherShipSpawn")
                SpawnMotherShip(n);
        }
    }
    public void SpawnDragonfly(GameObject Position)
    {
        GameObject ship = op.ReturnObject(gm.dragonFlies, gm.dfShip, gm.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnRaptor(GameObject Position)
    {
        GameObject ship = op.ReturnObject(gm.raptors, gm.rpShip, gm.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
    public void SpawnMotherShip(GameObject Position)
    {
        GameObject ship = op.ReturnObject(gm.motherShips, gm.mShip, gm.shipCollector);
        ship.SetActive(true);
        ship.transform.position = Position.transform.position;
    }
}
