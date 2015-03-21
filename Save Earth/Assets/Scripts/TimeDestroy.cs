using UnityEngine;
using System.Collections;

public class TimeDestroy : MonoBehaviour {
    public float delay;

	// Update is called once per frame
	void Update () {
        GameObject.Destroy(this.gameObject, delay);
	}
}
