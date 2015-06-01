using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof (PoolingManager))]
public class PoolManagerInspector : Editor {
    public int test1, test2, test3;
    public bool toggleShips = true;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
       /* PoolingManager myPoolingManager = (PoolingManager)target;

        EditorGUILayout.BeginToggleGroup("Ships", toggleShips);
        if(toggleShips)
        myPoolingManager.bulletAmt = EditorGUILayout.IntField("Bullets", myPoolingManager.bulletAmt);
        EditorGUILayout.EndToggleGroup();*/
        
        
        

    }
}
