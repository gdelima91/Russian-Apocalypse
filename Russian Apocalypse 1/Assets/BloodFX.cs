using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFX : MonoBehaviour {

    public GameObject blood;
    private CapsuleCollider capsuleCollider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision) {
        
        if (collision.gameObject.layer == 10) {
            print("Hit");
            Instantiate(blood, transform.position, transform.rotation);
        }
    }
}
