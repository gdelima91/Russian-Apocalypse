using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LEPhysicsPros : MonoBehaviour, LEEditorTimeAutoInitializer {

    RaycastHit hit = new RaycastHit();
    public LEMainBase mainBase;

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

    public void Recive_Damage(float damage)
    {
        if (mainBase == null) { mainBase = transform.root.GetComponent<LEMainBase>();if (mainBase == null) { Debug.LogError("There is no LEMainBase on the Root GameObject"); return; } }
        mainBase.Damage(damage);
    }

    /// <summary>
    /// This function Could be called at Editor Time. to Simplify the Edit process.
    /// for making Prefabes......  Improve the implimentation.......
    /// </summary>
    [ContextMenu("Eidtor Time Init")]
    public void ET_Init()
    {
        if (mainBase == null) { mainBase = transform.root.GetComponent<LEMainBase>(); if (mainBase == null) { Debug.LogError("There is no LEMainBase on the Root GameObject"); return; } }
    }

}
