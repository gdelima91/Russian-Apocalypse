using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LEIPhysicsPart : MonoBehaviour {

    RaycastHit hit = new RaycastHit();

    /// <summary>
    /// Return true when we didn't Hit anything, Or We hit a collider that colliders' game Object is gameObject
    /// </summary>
    public bool Check_DirectlyRaycast(Vector3 pos)
    {
        Vector3 dir = transform.position - pos;
        float length = dir.magnitude;
        if (Physics.Raycast(pos, dir, out hit, length))
        {
            if (hit.collider.gameObject == gameObject)
            {
                Debug.DrawLine(pos, transform.position, Color.blue);
                //UnityEditor.EditorApplication.isPaused = true;
                return true;
            }
        }
        return false;
    }

}
