using UnityEngine;
using System.Collections;

public class ItemDrop : MonoBehaviour {

    public enum DropType
    {
        Nothing,
        Shield,
        Weapon,
        Health,
        Resource,
        RawMaterial,
        PreciousResource
        
    }

    public DropType dropType = DropType.Nothing;

    public GameObject DropItem(DropType type)
    {
        if (type == DropType.Shield)
        {
            GameObject obj = ObjectPooler.Instance.ReturnObject("ShieldDrop");
            if(obj == null)
            {
                Debug.LogError("Tried To Drop Item That Doesnt Exist");
            }
            else
            {
                return obj;
            }
        }
        if (type == DropType.Weapon)
        {
            GameObject obj = ObjectPooler.Instance.ReturnObject("WeaponDrop");
            if (obj == null)
            {
                Debug.LogError("Tried To Drop Item That Doesnt Exist");
            }
            else
            {
                return obj;
            }
        }
        if (type == DropType.Health)
        {
            GameObject obj = ObjectPooler.Instance.ReturnObject("HealthDrop");
            if (obj == null)
            {
                Debug.LogError("Tried To Drop Item That Doesnt Exist");
            }
            else
            {
                return obj;
            }
        }        
        if (type == DropType.Resource)
        {
            GameObject obj = ObjectPooler.Instance.ReturnObject("ResourceDrop");
            if (obj == null)
            {
                Debug.LogError("Tried To Drop Item That Doesnt Exist");
            }
            else
            {
                return obj;
            }
        }
        if (type == DropType.RawMaterial)
        {
            GameObject obj = ObjectPooler.Instance.ReturnObject("RawMaterial");
            if (obj == null)
            {
                Debug.LogError("Tried To Drop Item That Doesnt Exist");
            }
            else
            {
                return obj;
            }
        }
        if (type == DropType.PreciousResource)
        {
            GameObject obj = ObjectPooler.Instance.ReturnObject("PreciousResource");
            if (obj == null)
            {
                Debug.LogError("Tried To Drop Item That Doesnt Exist");
            }
            else
            {
                return obj;
            }
        }
        return null;
    }
    
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            ShowNum();
    }
    public void ShowNum()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;
        int rnd = Random.Range(0, 1001);
        float num = rnd * .1f;
        
        Debug.Log(num);
    }
}
