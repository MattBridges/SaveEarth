using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathNode : MonoBehaviour {
    public int groupNumber;
    private Transform[] nodes;
    private NodeInfo[] ni;

	// Use this for initialization
  
	void OnEnable () {
        FindAllChildNodes();
	}
	
    void FindAllChildNodes()
    {
        nodes = gameObject.GetComponentsInChildren<Transform>();
        ni = gameObject.GetComponentsInChildren<NodeInfo>();
        Debug.Log(ni.Length);
        PathNodeManager.Instance.AddNodeGroup(groupNumber, nodes);
    }
}
