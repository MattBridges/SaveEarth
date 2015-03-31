using UnityEngine;
using System.Collections;

public class CameraFX : MonoBehaviour {
    public float shakeAmt;
    public float shakeTime;
	// Use this for initialization
	void Start () {
        shakeAmt = .15f;
        shakeTime = .4f;

	}
	
	// Update is called once per frame
	void Update () {
        
            

	}
    public void ScreenShake()
    {
        iTween.ShakePosition(this.gameObject, iTween.Hash("x", shakeAmt, "y", shakeAmt, "time", shakeTime));
    }
}
