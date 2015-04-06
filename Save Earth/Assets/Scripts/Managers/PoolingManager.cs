using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolingManager : MonoBehaviour {
    //Objects
    public GameObject bullet;
    public int bulletSpawnAmt;

    public GameObject dfShip;
    public int dfSpawnAmt;

    public GameObject rpShip;
    public int rpSpawnAmt;

    public GameObject mShip;
    public int mShipSpawnAmt;
 
   
    //Lists
    public List<GameObject> bullets;
    public List<GameObject> dragonFlies;
    public List<GameObject> raptors;
    public List<GameObject> motherShips;
    //Collectors
    public GameObject shipCollector;
    public GameObject bulletCollector;

    private ObjectPooler op;
	// Use this for initialization
	void Start () {
        dragonFlies = new List<GameObject>();
        bullets = new List<GameObject>();
        op = GameObject.Find("Pooler").GetComponent<ObjectPooler>();
        bullets = op.PoolObjects(bullet, bulletSpawnAmt, bulletCollector);
        dragonFlies = op.PoolObjects(dfShip, dfSpawnAmt, shipCollector);
        raptors = op.PoolObjects(rpShip, rpSpawnAmt, shipCollector);
        motherShips = op.PoolObjects(mShip, mShipSpawnAmt, shipCollector);
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
