using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    private Vector2 moveTemp;

    private Transform player;
    
    public float speed = 5;

  
    public float xDif, yDif;

 
    public float movementThreshold = 3;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
                moveTemp = player.transform.position;
            }


            transform.position = Vector2.MoveTowards(transform.position, moveTemp, speed * Time.deltaTime);
        }
    
        
	}
}
