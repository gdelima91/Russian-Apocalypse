using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFX : MonoBehaviour {

    public GameObject blood;
    public float velocityMultiplier = 10;
    private CapsuleCollider capsuleCollider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision) {
        
        if (collision.gameObject.layer == 10) {
            if (collision.gameObject.GetComponent<LE_Enemy1>() != null) {
                collision.gameObject.GetComponent<LE_Enemy1>().velocity = GetComponent<Rigidbody>().velocity * velocityMultiplier;
            }
            
            Instantiate(blood, new Vector3 (transform.position.x, 0, transform.position.z), transform.rotation);


            //collision.gameObject.GetComponent<Rigidbody>().AddForce(gameObject.GetComponent<Rigidbody>().velocity * 10);
            //collision.gameObject.GetComponent<Rigidbody>().AddForce(collision.collider.GetComponent<Rigidbody>().velocity * 10);
        }
    }
}
