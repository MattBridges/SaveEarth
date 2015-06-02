using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{
    #region Singlton Block

    private static ObjectPooler _instance;
    public static ObjectPooler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ObjectPooler>();
            }
            return _instance;
        }
    }
    #endregion

    #region Variables
    public bool dynamicPooling = true;
    #endregion

    #region Methods
    public GameObject ReturnObject(string obj)
    {
      
        foreach(GameObject objt in PoolingManager.Instance.pooled)
        {
            if(objt!=null)
            {
                string[] ar = objt.name.Split('(');
                if(ar[0]=="PlayerShip" && obj == "PlayerShip")
                {
                    
                    return objt;
                }
                if (ar[0] == obj && !objt.activeInHierarchy)
                {
                    return objt;
                }
            }
          
        }
        
        if (dynamicPooling)
        {
            GameObject objt = (GameObject)Instantiate(PoolingManager.Instance.pooledObjects[obj].obj);
            objt.SetActive(false);
            
            if(obj == "Bullet")
                objt.gameObject.transform.parent = GameObject.Find("BulletCollector").transform;
            else
                objt.gameObject.transform.parent = GameObject.Find("ShipCollector").transform;
           
            PoolingManager.Instance.pooled.Add(objt);
            return objt;
        }
        return null;

    }
    public void PoolObjects(GameObject PoolObject, int PoolAmount, GameObject Collector)
    {
        for (int i = 0; i < PoolAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(PoolObject);
            obj.SetActive(false);

            if (PoolObject.name == "Bullet")
                obj.gameObject.transform.parent = GameObject.Find("BulletCollector").transform;
            else
                obj.gameObject.transform.parent = GameObject.Find("ShipCollector").transform;
        
            PoolingManager.Instance.pooled.Add(obj);
        }
    }
    #endregion


}