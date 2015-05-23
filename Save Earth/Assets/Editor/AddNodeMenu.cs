using UnityEngine;
using System.Collections;
using UnityEditor;

public class AddNodeMenu : EditorWindow {

    [MenuItem("GameObject/Create Other/Save Earth/Create Path Node")]
    static void CreatePathNode()
    {
        GameObject node= AssetDatabase.LoadAssetAtPath("Assets/Prefabs/SpawnNodes/PathNode.prefab", 
            typeof(GameObject)) as GameObject;
        if(Selection.activeGameObject.name == "PathNodeGroup")
        {
            GameObject obj = GameObject.Instantiate(node, Vector3.zero, Quaternion.identity) as GameObject;
            obj.name = "PathNode";
            obj.transform.SetParent(Selection.activeGameObject.transform);
            Selection.activeGameObject = obj;
        }
        else
        {
            Debug.LogError("Path Nodes Must Be Placed On PathNodeGroup Object");
        }
  
    }
    [MenuItem("GameObject/Create Other/Save Earth/Create Spawn Node")]
    static void CreatSpawnNode()
    {
        GameObject node = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/SpawnNodes/SpawnNode.prefab",
            typeof(GameObject)) as GameObject;
        GameObject obj = GameObject.Instantiate(node, Vector3.zero, Quaternion.identity) as GameObject;
        obj.name = "SpawnNode";
        obj.transform.SetParent(Selection.activeGameObject.transform);
        Selection.activeGameObject = node;
    }


}
