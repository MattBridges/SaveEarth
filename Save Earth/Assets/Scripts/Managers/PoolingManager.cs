using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolingManager : MonoBehaviour
{
    #region Singlton Block

    private static PoolingManager _instance;
    public static PoolingManager Instance
    {
        get
        {
            if(_instance==null)
            {
                _instance = GameObject.FindObjectOfType<PoolingManager>();
            }
            return _instance;
        }
    }
    #endregion

    #region Variables
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

    #endregion

   	void Start () {

        bullets = ObjectPooler.Instance.PoolObjects(bullet, bulletSpawnAmt, bulletCollector);
        dragonFlies = ObjectPooler.Instance.PoolObjects(dfShip, dfSpawnAmt, shipCollector);
        raptors = ObjectPooler.Instance.PoolObjects(rpShip, rpSpawnAmt, shipCollector);
        motherShips = ObjectPooler.Instance.PoolObjects(mShip, mShipSpawnAmt, shipCollector);
        mines = ObjectPooler.Instance.PoolObjects(mine, minesSpawnAmt, shipCollector);
        satellites = ObjectPooler.Instance.PoolObjects(satellite, satelliteSpawnAmt, shipCollector);
        allyRaptors = ObjectPooler.Instance.PoolObjects(allyRaptor, allyRaptorSpawnAmt, shipCollector);
        orbitalRefinerys = ObjectPooler.Instance.PoolObjects(orbitalRefinery, orbitalRefSpawnAmt, shipCollector);
        gatherers = ObjectPooler.Instance.PoolObjects(gatherer, gathererSpawnAmt, shipCollector);
        allyOrbitalBases = ObjectPooler.Instance.PoolObjects(allyOrbitalBase, allyOrbitalBaseSpawnAmt, shipCollector);
        orbitalBases = ObjectPooler.Instance.PoolObjects(orbitalBase, orbitalBaseSpawnAmt, shipCollector);
        allyCarriers = ObjectPooler.Instance.PoolObjects(allyCarrier, allyCarrierSpawnAmt, shipCollector);
        carriers = ObjectPooler.Instance.PoolObjects(carrier, carrierSpawnAmt, shipCollector);
        baseShips = ObjectPooler.Instance.PoolObjects(baseShip, baseShipSpawnAmt, shipCollector);
        
	}

}
