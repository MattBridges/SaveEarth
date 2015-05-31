using UnityEngine;
using System.Collections;

public class EndObject : MonoBehaviour {
    public bool isEndObject;

    public void UpdateEndObject()
    {
        if(isEndObject)
        {
            GameManager.Instance.currentEndLevel.AddDestroyObject(this.gameObject);
        }
    }
    void OnDisable()
    {
        isEndObject = false;
    }

}
