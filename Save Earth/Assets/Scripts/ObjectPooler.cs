using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{

    public bool dynamicPooling = true;

    public GameObject ReturnObject(List<GameObject> List, GameObject Obj, GameObject Collector)
    {
        for (int i = 0; i < List.Count; i++)
        {
            if (!List[i].activeInHierarchy)
                return List[i];
        }
        if (dynamicPooling)
        {
            GameObject obj = (GameObject)Instantiate(Obj);
            obj.SetActive(false);
            obj.gameObject.transform.parent = Collector.transform;
            List.Add(obj);
            return obj;
        }
        return null;

    }
    public List<GameObject> PoolObjects(GameObject PoolObject, int PoolAmount, GameObject Collector)
    {
        List<GameObject> objects = new List<GameObject>();
        for (int i = 0; i < PoolAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(PoolObject);
            obj.SetActive(false);
            obj.gameObject.transform.parent = Collector.transform;
            objects.Add(obj);
        }
        return objects;
    }


}