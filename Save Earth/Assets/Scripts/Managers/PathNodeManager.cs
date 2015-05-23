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
    public Dictionary<int, Transform[]> pathNodes;
    public Dictionary<int, List<GameObject>> nodes;
    #endregion 

    #region Group Methods
    void InitPathNodeDict()
    {
        if (pathNodes != null)
            return;
        else
            pathNodes = new Dictionary<int, Transform[]>();
    }

    public void AddNodeGroup(int GroupNumber, Transform[] NodeList)
    {
        InitPathNodeDict();

        if (pathNodes.ContainsKey(GroupNumber) == true)
            Debug.LogError("PathNode Group " + GroupNumber + " Already Exists!");
        else
            pathNodes[GroupNumber] = NodeList;
    }

    public Transform[] GetNodeGroup(int GroupNumber)
    {
        if (pathNodes.ContainsKey(GroupNumber) == true)
            return pathNodes[GroupNumber];
        else
        {
            Debug.LogError("No PathNode Group " + GroupNumber + " Found");
            return null;
        }
    }
    #endregion

    public Transform GetClosestNode(Transform[] NodeGroup, GameObject Ship)
    {
        Transform closestNode = null;
        float dist = 0;
        float bestDist = 0;
        foreach(Transform node in NodeGroup)
        {
            dist = Vector2.Distance(Ship.transform.position, node.position);
            if (dist < bestDist)
            {
                closestNode = node;
                bestDist = dist;
            }
        }
        return closestNode;
    }



}
