using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEPhysicsManager : SubManager {

    LEPhysicsPros[] allPhysicsParts;
	// Use this for initialization
	void Start () {
        allPhysicsParts = GetComponentsInChildren<LEPhysicsPros>();
	}

    public override void UpdateManager(MKInputData inputmanager)
    {
        //update manager......
    }

    public override void CompositeData(MKInputData inputmanager)
    {
        //Compositing Data
    }


    public bool Check_DirectlyRaycast(Vector3 pos)
    {
        //If we see any PhysicsPart of the Physics Manager. We return true, other wise return false; 
        foreach (LEPhysicsPros p in allPhysicsParts)
        {
            if (p.Check_DirectlyRaycast(pos)) { return true; }
        }
        return false;
    }

}
