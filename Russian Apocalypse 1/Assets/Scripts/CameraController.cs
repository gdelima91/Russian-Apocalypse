using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float lerpSpeed;
    public Vector3 cameraOffset;
    public PlayerInput playerGO;
    public float lerpLeash = 0.3f;




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        LerpToDestination();

	}

    void LerpToDestination () {
        Vector3 midPoint = new Vector3((playerGO.pointToLook.x - player.transform.position.x) * lerpLeash + player.transform.position.x, player.transform.position.y, (playerGO.pointToLook.z - player.transform.position.z) * lerpLeash + player.transform.position.z);

        Vector3 lerpDestination = new Vector3(midPoint.x + cameraOffset.x, transform.position.y + cameraOffset.y, midPoint.z + cameraOffset.z);
        transform.position = Vector3.Lerp(transform.position, lerpDestination, Time.deltaTime * lerpSpeed);
    }
}
