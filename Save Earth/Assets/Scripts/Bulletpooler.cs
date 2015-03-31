using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bulletpooler : MonoBehaviour {
    public GameObject bullet;
    public static Bulletpooler curBulletPool;
    public int pooledAmount = 10;
    public bool dynamicPooling=true;
    public List<GameObject> bullets;



	// Use this for initialization
	void Start () {
        curBulletPool = this;
        bullets = new List<GameObject>();
        for(int i=0; i<pooledAmount;i++)
        {
            GameObject obj = (GameObject)Instantiate(bullet);
            obj.SetActive(false);
            obj.gameObject.transform.parent = GameObject.Find("BulletCollector").transform;
            bullets.Add(obj);
        }
	}
    public GameObject ReturnBullet()
    {
        for(int i =0; i<bullets.Count;i++)
        {
            if (!bullets[i].activeInHierarchy)
                return bullets[i];
        }
        if(dynamicPooling)
        {
            GameObject obj = (GameObject)Instantiate(bullet);
            obj.SetActive(false);
            obj.gameObject.transform.parent = GameObject.Find("BulletCollector").transform;
            bullets.Add(obj);
            return obj;
        }
        return null;

    }
	

}
