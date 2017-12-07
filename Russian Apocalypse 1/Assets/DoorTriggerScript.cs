using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerScript : MonoBehaviour {

    public DoorScript doorScript;
    public bool frontTrigger;

    

	// Use this for initialization
	void Start () {
        //GameObject parentGO = GetComponentInParent<GameObject>();
        //doorScript = parentGO.GetComponent<DoorScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        //print(other.name);
        if (other.gameObject.layer == LayerMask.NameToLayer ("Player") && other.name == "Wei_Player") {
            if (frontTrigger) {
                doorScript.OpenDoor(DoorScript.Triggers.Front);
            }
            else {
                doorScript.OpenDoor(DoorScript.Triggers.Back);
            }
        }
    }
}
