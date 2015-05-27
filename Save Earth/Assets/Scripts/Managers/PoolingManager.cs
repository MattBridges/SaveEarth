using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
public struct PoolObject
{
    public GameObject obj;
    public int poolAmount;
}
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

    public int bulletAmt, dragonFlyAmt, motherShipAmt, mineAmt, satelliteAmt, allyRaptorAmt, orbitalRefAmt, gathererAmt, allyOrbitalAmt, orbitalAmt, allyCarrierAmt, carrierAmt, baseShipAmt, raptorAmt;
    public Dictionary<string, PoolObject> pooledObjects;
    public Dictionary<string, PoolObject> pooledDebris;
    
    public List<GameObject> pooled;

    #endregion
    
    #region Methods

    // Initialize Pool Manager Dictionary
    void InitPool()
    {
        if (pooledObjects == null)
            pooledObjects = new Dictionary<string, PoolObject>();
        else
            return;
    }

    //Create a new PoolObject for the dictionary
    public PoolObject NewPoolObject(GameObject newObject, int poolAmount )
    {
        PoolObject obj = new PoolObject();
        obj.obj = newObject;
        obj.poolAmount = poolAmount;

        return obj;
    }

    //Add PoolObject to dictionary
    public void AddPoolObject(string name, int poolAmount)
    {
        InitPool();

        GameObject ship = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/"+ name + ".prefab",
            typeof(GameObject)) as GameObject;

        pooledObjects.Add(name, NewPoolObject(ship, poolAmount));
        
    }

    //Iterate through pooled objects manager dictionay and pool objects into pooled list;
    void PoolObjects()
    {
        foreach(KeyValuePair<string,PoolObject> entry in pooledObjects)
        {
               ObjectPooler.Instance.PoolObjects(entry.Value.obj, entry.Value.poolAmount, GameObject.Find("ShipCollector"));           
        }
    }
    #endregion

    void Start()
    {
        AddPoolObject("Dragonfly", dragonFlyAmt);
        AddPoolObject("Raptor", raptorAmt);
        AddPoolObject("MotherShip", motherShipAmt);
        AddPoolObject("EnemyMine", mineAmt);
        AddPoolObject("Satellite", satelliteAmt);
        AddPoolObject("AllyRaptor", allyRaptorAmt);
        AddPoolObject("OrbitalRefinery", orbitalRefAmt);
        AddPoolObject("Gatherer", gathererAmt);
        AddPoolObject("AllyOrbitalBase", allyOrbitalAmt);
        AddPoolObject("OrbitalBase", orbitalAmt);
        AddPoolObject("AllyCarrier", allyCarrierAmt);
        AddPoolObject("Carrier", carrierAmt);
        AddPoolObject("EnemyBaseShip", baseShipAmt);
        AddPoolObject("Bullet", bulletAmt);
        PoolObjects();
    }

}
