using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsTest : MonoBehaviour {

    public AiUtility.FieldOfView fieldOfView;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, fieldOfView.viewDistance);
    }

    [ContextMenu("Check Collider")]
    void Get_All_Collider()
    {
        List<Collider> cs = fieldOfView.Get_All_Collider_InsideFieldOfViewRange();
        foreach (Collider c in cs)
        {
            Debug.Log(c.name);
        }
    }
}
