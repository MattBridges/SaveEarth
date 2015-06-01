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
  
    
    
    public int bulletAmt,
        dragonFlyAmt,
        motherShipAmt,
        mineAmt,
        satelliteAmt,
        allyRaptorAmt,
        orbitalRefAmt,
        gathererAmt,
        allyOrbitalAmt,
        orbitalAmt,
        allyCarrierAmt,
        carrierAmt,
        baseShipAmt,
        raptorAmt,
        IceChunksAmt,
        rawMaterialAmt,
        preciousResourceAmt;

    public Dictionary<string, PoolObject> pooledObjects;
    public Dictionary<string, PoolObject> pooledDebris;
    public Dictionary<string, PoolObject> pooledDropObjects;
   
    
    public List<GameObject> pooled;

    

    #endregion
    
    #region Methods

    // Initialize Pool Manager Dictionary
    void InitPool()
    {
        //if (dict == null)
        //{
        //    dict = new Dictionary<string, PoolObject>();
        //    Debug.Log(dict.ToString() + " was created");
        //}
            
        //else
        //    return;
        if (pooledObjects == null)
            pooledObjects = new Dictionary<string, PoolObject>();
        if (pooledDebris == null)
            pooledDebris = new Dictionary<string, PoolObject>();
        if (pooledDropObjects == null)
            pooledDropObjects = new Dictionary<string, PoolObject>();
   
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
    public void AddPoolObject(Dictionary<string,PoolObject>dict,string name, int poolAmount)
    {
       // InitPool(dict);

        GameObject ship = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/"+ name + ".prefab",
            typeof(GameObject)) as GameObject;

        PoolObject obj = NewPoolObject(ship, poolAmount);
            

        dict.Add(name, obj);       
        
    }

    //Iterate through pooled objects manager dictionay and pool objects into pooled list;
    void PoolObjects(Dictionary<string,PoolObject> dict)
    {
        
        foreach(KeyValuePair<string,PoolObject> entry in dict)
        {
               ObjectPooler.Instance.PoolObjects(entry.Value.obj, entry.Value.poolAmount, GameObject.Find("ShipCollector"));           
        }
    }
    #endregion

    void Start()
    {
        //Init Pools
        InitPool();

        //Add Object types
        AddPoolObject(pooledObjects, "Dragonfly", dragonFlyAmt);
        AddPoolObject(pooledObjects, "Raptor", raptorAmt);
        AddPoolObject(pooledObjects, "MotherShip", motherShipAmt);
        AddPoolObject(pooledObjects, "EnemyMine", mineAmt);
        AddPoolObject(pooledObjects, "Satellite", satelliteAmt);
        AddPoolObject(pooledObjects, "AllyRaptor", allyRaptorAmt);
        AddPoolObject(pooledObjects, "OrbitalRefinery", orbitalRefAmt);
        AddPoolObject(pooledObjects, "Gatherer", gathererAmt);
        AddPoolObject(pooledObjects, "AllyOrbitalBase", allyOrbitalAmt);
        AddPoolObject(pooledObjects, "OrbitalBase", orbitalAmt);
        AddPoolObject(pooledObjects, "AllyCarrier", allyCarrierAmt);
        AddPoolObject(pooledObjects, "Carrier", carrierAmt);
        AddPoolObject(pooledObjects, "EnemyBaseShip", baseShipAmt);
        AddPoolObject(pooledObjects, "Bullet", bulletAmt);
        AddPoolObject(pooledDebris, "IceChunks", IceChunksAmt);
        AddPoolObject(pooledDropObjects, "RawMaterial", rawMaterialAmt);
        AddPoolObject(pooledDropObjects, "PreciousResource", preciousResourceAmt);

        //Pool Objects
        PoolObjects(pooledObjects);
        PoolObjects(pooledDebris);
        PoolObjects(pooledDropObjects);
    }
    

}
