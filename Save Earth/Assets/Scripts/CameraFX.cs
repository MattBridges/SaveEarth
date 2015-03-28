using UnityEngine;
using System.Collections;

public class CameraFX : MonoBehaviour {
    public static CameraFX gameCam;
	// Use this for initialization
	void Start () {
        gameCam = this;
	}
	
	// Update is called once per frame
	void Update () {
        
            

	}
    public void ScreenShake(float amt, float time)
    {
        iTween.ShakePosition(this.gameObject, iTween.Hash("x", amt, "y", amt, "time", time));
    }
}
