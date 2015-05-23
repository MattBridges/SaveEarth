using UnityEngine;
using System.Collections;
using UnityEditor;

public class AddNodeMenu : EditorWindow {

    [MenuItem("GameObject/Create Other/Save Earth/Create Path Node")]
    static void CreatePathNode()
    {
        var node= AssetDatabase.LoadAssetAtPath("Assets/Prefabs/SpawnNodes/PathNode.prefab", 
            typeof(GameObject));
        if(Selection.activeGameObject.name == "PathNodeGroup")
        {
            
            GameObject obj = PrefabUtility.InstantiatePrefab(node) as GameObject;
            obj.transform.position = Vector2.zero;
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
        GameObject obj = PrefabUtility.InstantiatePrefab(node) as GameObject;
        obj.transform.position = Vector2.zero;
        obj.name = "SpawnNode";
        obj.transform.SetParent(Selection.activeGameObject.transform);
        Selection.activeGameObject = obj;
    }


}
