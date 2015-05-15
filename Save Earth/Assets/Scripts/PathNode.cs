using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathNode : MonoBehaviour {
    public int groupNumber;
    private Transform[] nodes;

	// Use this for initialization
  
	void Start () {
        FindAllChildNodes();
	}
	
    void FindAllChildNodes()
    {
        nodes = gameObject.GetComponentsInChildren<Transform>();
        PathNodeManager.Instance.AddNodeGroup(groupNumber, nodes);
    }
}
