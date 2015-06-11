using UnityEngine;
using System.Collections;

public class ShipPointer : MonoBehaviour {
    public GameObject target;
    


	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
      
              if (target != null)
              {
                  Vector2 dir = this.target.transform.position - PlayerShip.Instance.transform.position;
                  var ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
                  Vector3 newDir = new Vector3(0, 0, ang);
                  this.transform.position = LerpByDistance(PlayerShip.Instance.transform.position, this.target.transform.position, 2.5f);
                  transform.rotation = Quaternion.Euler(newDir);
                  if (target.GetComponent<SpriteRenderer>().isVisible || !target.activeInHierarchy)
                      this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                  else
                      this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
           
              }

          
              
       
	}

    public Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
    {
        Vector3 P = x * Vector3.Normalize(B - A) + A;
        return P;
    }


}
