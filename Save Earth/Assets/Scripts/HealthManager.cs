using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthManager : MonoBehaviour {
    
    
    public GameObject parentObj;
    public float barOffset = 1.5f;

    private Image healthBar;
    private float maxWidth;
    private Vector3 rot = Vector3.zero;
    
	// Use this for initialization
	void OnEnable () {
        healthBar = this.gameObject.GetComponent<Image>();
        maxWidth = healthBar.rectTransform.rect.width;
        SetWidth(1);
  
        
	}
    void OnDisable()
    {
        SetWidth(1);
    }
	public void SetWidth(float scaleAmt)
    {
        healthBar.rectTransform.sizeDelta =  new Vector2((scaleAmt * maxWidth), .1f);
    }

	// Update is called once per frame
	void Update () {

        Vector3 barLoc = new Vector3(parentObj.transform.position.x-maxWidth/2, parentObj.transform.position.y - barOffset, parentObj.transform.position.z);
        this.transform.position = barLoc;
        this.gameObject.transform.rotation = Quaternion.Euler(rot);

   
	}
}
