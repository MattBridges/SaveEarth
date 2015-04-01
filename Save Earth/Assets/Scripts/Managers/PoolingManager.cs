using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolingManager : MonoBehaviour {
    //Objects
    public GameObject dfShip;
    public int dfSpawnAmt;
 
    public GameObject bullet;
    public int bulletSpawnAmt;
    //Lists
    public List<GameObject> bullets;
    public List<GameObject> dragonFlies;
    //Collectors
    public GameObject shipCollector;
    public GameObject bulletCollector;

    private ObjectPooler op;
	// Use this for initialization
	void Start () {
        dragonFlies = new List<GameObject>();
        bullets = new List<GameObject>();
        op = GameObject.Find("Pooler").GetComponent<ObjectPooler>();
        dragonFlies = op.PoolObjects(dfShip, dfSpawnAmt, shipCollector);
        bullets = op.PoolObjects(bullet, bulletSpawnAmt, bulletCollector);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
