using UnityEngine;
using System.Collections;

public class GizmoType : MonoBehaviour {
    public Color color;
    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position,.4f);
    }
}
