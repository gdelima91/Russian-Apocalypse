using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    public float spinSpeed = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        SpinMesh();
	}

    void SpinMesh () {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}
