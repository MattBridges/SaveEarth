using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathNode : MonoBehaviour {
    public int groupNumber;
    private NodeInfo[] nodes;
    private List<NodeInfo> ni;
  
	void OnEnable () {
        ni = new List<NodeInfo>();
        FindAllChildNodes();
	}
	
    void FindAllChildNodes()
    {
        nodes = gameObject.GetComponentsInChildren<NodeInfo>();
        foreach (NodeInfo node in nodes)
        {
            ni.Add(node);
        }
        PathNodeManager.Instance.AddNodeGroup(groupNumber, ni);
    }
}
