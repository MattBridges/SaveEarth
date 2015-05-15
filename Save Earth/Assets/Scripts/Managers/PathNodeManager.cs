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
    #endregion 

    #region Methods
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
}
