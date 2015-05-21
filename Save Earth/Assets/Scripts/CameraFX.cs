using UnityEngine;
using System.Collections;

public class CameraFX : MonoBehaviour {
    #region Singlton Block
    private static CameraFX _instance;
    public static CameraFX Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CameraFX>();
            }
            return _instance;
        }
    }
    #endregion

    #region Variables
    public float shakeAmt;
    public float shakeTime;
    #endregion

    #region Methods
    public void ScreenShake()
    {
        iTween.ShakePosition(this.gameObject, iTween.Hash("x", shakeAmt, "y", shakeAmt, "time", shakeTime));
    }
    public void StopScreenShake()
    {
        iTween.Stop();
    }
    #endregion

}
