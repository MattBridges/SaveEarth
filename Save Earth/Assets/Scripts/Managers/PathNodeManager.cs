using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathNodeManager : MonoBehaviour {
    
    #region Singlton Block
    private static PathNodeManager _instance;
    public static PathNodeManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PathNodeManager>();
            }
            return _instance;
        }
    }
    #endregion

    #region Global Variables
    public Dictionary<int, List<NodeInfo>> nodes;
    public NodeInfo currentNode;

    #endregion 

    #region Group Methods
    void InitPathNodeDict()
    {
        if (nodes != null)
            return;
        else
            nodes = new Dictionary<int, List<NodeInfo>>();
    }

    public void AddNodeGroup(int GroupNumber, List<NodeInfo> NodeList)
    {
        InitPathNodeDict();

        if (nodes.ContainsKey(GroupNumber) == true)
        {
            nodes[GroupNumber].Clear();
            Debug.Log(nodes[GroupNumber].Count);
        }
        
        nodes[GroupNumber] = NodeList;
        Debug.Log(nodes[GroupNumber].Count);   
            
    }

    public List<NodeInfo> GetNodeGroup(int GroupNumber)
    {
        if (nodes.ContainsKey(GroupNumber) == true)
            return nodes[GroupNumber];
        else
        {
            Debug.LogError("No PathNode Group " + GroupNumber + " Found");
            return null;
        }
    }
    #endregion

    #region Get Methods
    public Transform GetClosestNode(int groupNumber, GameObject Ship)
    {
        List<NodeInfo> NodeGroup = GetNodeGroup(groupNumber);
        
        NodeInfo closestNode = null;
        float dist = 0;
        float bestDist = 0;
        foreach(NodeInfo node in NodeGroup)
        {
            dist = Vector2.Distance(Ship.transform.position, node.transform.position);
            if (dist < bestDist)
            {
                closestNode = node;
                bestDist = dist;
            }
        }
        currentNode = closestNode;
        return closestNode.transform;
    }

    public Transform GetNextNode(int groupNumber, bool inReverse = false)
    {
       List<NodeInfo> NodeGroup = GetNodeGroup(groupNumber); 
       NodeInfo nextNode = null;
        if(currentNode==null)
        {
            foreach(NodeInfo node in NodeGroup)
            {
                if(node.nodeNumber == 1)
                {
                    currentNode = node;
                    nextNode = currentNode;
                }
            }
        }
        else
        {
            int number = nextNode.nodeNumber;
            if(!inReverse)
                number++;
            if (inReverse)
                number--;
            if (number > NodeGroup.Count)
                number = NodeGroup.Count;
            if (number < 1)
                number = 1;

            foreach (NodeInfo node in NodeGroup)
            {
                if(node.nodeNumber == number)
                {
                    nextNode = node;
                }
            }
        }
        return nextNode.transform;
    }

    public Transform GetRandomNode(int groupNumber)
    {
        List<NodeInfo> NodeGroup = GetNodeGroup(groupNumber);
        return NodeGroup[Random.Range(0, NodeGroup.Count)].transform;
    }

    #endregion


}
