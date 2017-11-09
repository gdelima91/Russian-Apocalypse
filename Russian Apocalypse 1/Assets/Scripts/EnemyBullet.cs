using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    Rigidbody rb;
    public float speed = 1f;
    public float damage;
    public PlayerStats playerStats;

    void Awake () {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 3);
    }
	
	// Update is called once per frame
	void Update () {
        GoForward();
	}

    void GoForward () {
        rb.velocity = (transform.right * speed) * Time.deltaTime;
    }

    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            playerStats.currentHealth -= damage;
        }

        Destroy(gameObject);
    }
}
