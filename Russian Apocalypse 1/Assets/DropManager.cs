using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour {

    public float rNGNumberToMeet;
    public GameObject shotgunDrop;
    public float rNGtoMinus;
    private float currentRNGNumber;
    

	// Use this for initialization
	void Start () {
        currentRNGNumber = rNGNumberToMeet;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RNGDrop (float minusRNG) {
        float rNG = Random.Range(0, 100);
        //currentRNGNumber -= minusRNG;
        if (rNG > currentRNGNumber) {
            Instantiate(shotgunDrop, new Vector3 (transform.position.x, 2, transform.position.z), transform.rotation);
            //currentRNGNumber = rNGNumberToMeet;
        }
    }
}
