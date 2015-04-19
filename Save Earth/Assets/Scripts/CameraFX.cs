using UnityEngine;
using System.Collections;

public class CameraFX : MonoBehaviour {
    public float shakeAmt;
    public float shakeTime;
	// Use this for initialization
	void Start () {
   

	}

    public void ScreenShake()
    {
        iTween.ShakePosition(this.gameObject, iTween.Hash("x", shakeAmt, "y", shakeAmt, "time", shakeTime));
    }
    public void StopScreenShake()
    {
        iTween.Stop();
    }
}
