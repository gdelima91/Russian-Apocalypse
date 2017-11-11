using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEPhysicsManager : MonoBehaviour {

    LEIPhysicsPart[] allPhysicsParts;
	// Use this for initialization
	void Start () {
        allPhysicsParts = GetComponentsInChildren<LEIPhysicsPart>();
	}


    public bool Check_DirectlyRaycast(Vector3 pos)
    {
        //If we see any PhysicsPart of the Physics Manager. We return true, other wise return false; 
        foreach (LEIPhysicsPart p in allPhysicsParts)
        {
            if (p.Check_DirectlyRaycast(pos)) { return true; }
        }
        return false;
    }

}
