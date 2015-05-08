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

    public GameObject mine;
    public int minesSpawnAmt;

    public GameObject satellite;
    public int satelliteSpawnAmt;

    public GameObject allyRaptor;
    public int allyRaptorSpawnAmt;

    public GameObject orbitalRefinery;
    public int orbitalRefSpawnAmt;

    public GameObject gatherer;
    public int gathererSpawnAmt;

    public GameObject allyOrbitalBase;
    public int allyOrbitalBaseSpawnAmt;

    public GameObject orbitalBase;
    public int orbitalBaseSpawnAmt;

    public GameObject allyCarrier;
    public int allyCarrierSpawnAmt;

    public GameObject carrier;
    public int carrierSpawnAmt;

    public GameObject baseShip;
    public int baseShipSpawnAmt;

    

 
   
    //Lists
    public List<GameObject> bullets;
    public List<GameObject> dragonFlies;
    public List<GameObject> raptors;
    public List<GameObject> motherShips;
    public List<GameObject> mines;
    public List<GameObject> satellites;
    public List<GameObject> allyRaptors;
    public List<GameObject> orbitalRefinerys;
    public List<GameObject> gatherers;
    public List<GameObject> allyOrbitalBases;
    public List<GameObject> orbitalBases;
    public List<GameObject> allyCarriers;
    public List<GameObject> carriers;
    public List<GameObject> baseShips;
    //Collectors
    public GameObject shipCollector;
    public GameObject bulletCollector;

    private ObjectPooler op;
	// Use this for initialization
	void Start () {
       // dragonFlies = new List<GameObject>();
       // bullets = new List<GameObject>();
        op = GameObject.Find("Pooler").GetComponent<ObjectPooler>();
        bullets = op.PoolObjects(bullet, bulletSpawnAmt, bulletCollector);
        dragonFlies = op.PoolObjects(dfShip, dfSpawnAmt, shipCollector);
        raptors = op.PoolObjects(rpShip, rpSpawnAmt, shipCollector);
        motherShips = op.PoolObjects(mShip, mShipSpawnAmt, shipCollector);
        mines = op.PoolObjects(mine, minesSpawnAmt, shipCollector);
        satellites = op.PoolObjects(satellite, satelliteSpawnAmt, shipCollector);
        allyRaptors = op.PoolObjects(allyRaptor, allyRaptorSpawnAmt, shipCollector);
        orbitalRefinerys = op.PoolObjects(orbitalRefinery, orbitalRefSpawnAmt, shipCollector);
        gatherers = op.PoolObjects(gatherer, gathererSpawnAmt, shipCollector);
        allyOrbitalBases = op.PoolObjects(allyOrbitalBase, allyOrbitalBaseSpawnAmt, shipCollector);
        orbitalBases = op.PoolObjects(orbitalBase, orbitalBaseSpawnAmt, shipCollector);
        allyCarriers = op.PoolObjects(allyCarrier, allyCarrierSpawnAmt, shipCollector);
        carriers = op.PoolObjects(carrier, carrierSpawnAmt, shipCollector);
        baseShips = op.PoolObjects(baseShip, baseShipSpawnAmt, shipCollector);
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
