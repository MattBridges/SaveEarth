using UnityEngine;
using System.Collections;

public class MineController : MonoBehaviour {
    public int speed = 2;
    int dir;
	void OnEnable()
    {
       dir = Random.Range(0, 2);
    }
    

	// Update is called once per frame
	void Update () {
        if (this.dir == 0)
        {
            transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        }
        if (this.dir == 1)
        {
            transform.Rotate(-Vector3.forward * speed * Time.deltaTime);
        }
	}

}
