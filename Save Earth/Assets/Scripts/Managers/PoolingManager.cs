using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public struct PoolObject
{
    public GameObject obj;
    public int poolAmount;
}
[System.Serializable]
public class Ships
{
    public int
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
       raptorAmt;
       
       
}
[System.Serializable]
public class Projectiles
{
    public int bulletAmt;
    public int canProjectileAmt;
    public int redProjectileAmt;
    public int yellowProjectileAmt;

}
[System.Serializable]
public class Collectibles
{

}
[System.Serializable]
public class Resource
{
    public int
    rawMaterialAmt,
    preciousResourceAmt;
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


    public Ships shipTotals = new Ships();
    public Projectiles projectileTotals = new Projectiles();
    public Collectibles collectibleTotals = new Collectibles();
    public Resource resourcesTotals = new Resource();



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

        //Ships
        AddPoolObject(pooledObjects, "Dragonfly", shipTotals.dragonFlyAmt);
        AddPoolObject(pooledObjects, "Raptor", shipTotals.raptorAmt);
        AddPoolObject(pooledObjects, "MotherShip", shipTotals.motherShipAmt);
        AddPoolObject(pooledObjects, "EnemyMine", shipTotals.mineAmt);
        AddPoolObject(pooledObjects, "Satellite", shipTotals.satelliteAmt);
        AddPoolObject(pooledObjects, "AllyRaptor", shipTotals.allyRaptorAmt);
        AddPoolObject(pooledObjects, "OrbitalRefinery", shipTotals.orbitalRefAmt);
        AddPoolObject(pooledObjects, "Gatherer", shipTotals.gathererAmt);
        AddPoolObject(pooledObjects, "AllyOrbitalBase", shipTotals.allyOrbitalAmt);
        AddPoolObject(pooledObjects, "OrbitalBase", shipTotals.orbitalAmt);
        AddPoolObject(pooledObjects, "AllyCarrier", shipTotals.allyCarrierAmt);
        AddPoolObject(pooledObjects, "Carrier", shipTotals.carrierAmt);
        AddPoolObject(pooledObjects, "EnemyBaseShip", shipTotals.baseShipAmt);
        
        //Projectiles
        AddPoolObject(pooledObjects, "Bullet", projectileTotals.bulletAmt);
        AddPoolObject(pooledObjects, "CannonProjectile", projectileTotals.canProjectileAmt);
        AddPoolObject(pooledObjects, "RedProjectile", projectileTotals.canProjectileAmt);
        AddPoolObject(pooledObjects, "YellowProjectile", projectileTotals.canProjectileAmt);
        
        //DropObjects
        AddPoolObject(pooledDropObjects, "RawMaterial", resourcesTotals.rawMaterialAmt);
        AddPoolObject(pooledDropObjects, "PreciousResource", resourcesTotals.preciousResourceAmt);

        //Pool Objects
        PoolObjects(pooledObjects);
        PoolObjects(pooledDebris);
        PoolObjects(pooledDropObjects);
    }
    

}
