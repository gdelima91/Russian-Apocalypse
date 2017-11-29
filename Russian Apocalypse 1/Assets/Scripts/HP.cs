using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour {

    public PlayerStats playerStats;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateHP();
	}

    void UpdateHP () {
        float x = playerStats.currentHealth / 100;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
}
