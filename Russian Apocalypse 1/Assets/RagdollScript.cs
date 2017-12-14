using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * 7500);
        Destroy(gameObject, 10);
	}
	
	// Update is called once per frame
	void Update () {
        print(gameObject.GetComponent<Rigidbody>().velocity);
	}
}
