using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    public float timer = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 8) {
            if (other.gameObject.GetComponent<PickUpManager>() != null) {
                other.gameObject.GetComponent<PickUpManager>().EquipShotgun(timer);
                Destroy(gameObject);
            }
        }
    }
}
