using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    private Vector3 moveTemp;

    private Transform player;
    
    public float speed = 5;

  
    public float xDif, yDif;

 
    public float movementThreshold = 3;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        moveTemp = new Vector3(0, 0, -10);

    }

	void Update () {
        
        
        if (player != null)
        {
            if (player.transform.position.x > transform.position.x)
                xDif = player.transform.position.x - transform.position.x;
            else
                xDif = transform.position.x - player.transform.position.x;
            if (player.transform.position.y > transform.position.y)
                yDif = player.transform.position.y - transform.position.y;
            else
                yDif = transform.position.y - player.position.y;


            if (xDif >= movementThreshold || yDif >= movementThreshold || xDif <= -movementThreshold || yDif <= -movementThreshold)
            {
                moveTemp = new Vector3(player.transform.position.x, player.transform.position.y, -10);
            }


            transform.position = Vector3.MoveTowards(transform.position, moveTemp, speed * Time.deltaTime);
        }
    
        
	}
}
