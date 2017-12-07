using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegen : MonoBehaviour {

    public float healthRegen;
    private LE_Brute leBrute;

	// Use this for initialization
	void Start () {
        leBrute = GetComponent<LE_Brute>();
	}
	
	// Update is called once per frame
	void Update () {
        PlayerRegen();
	}

    void PlayerRegen () {
        bool canRegen;
        if (Input.GetKey(KeyCode.Mouse0)) {
            canRegen = false;
        }
        else {
            canRegen = true;
        }
        if (canRegen) {
            if (leBrute.currentHealth < leBrute.maxHealth) {
                leBrute.currentHealth += (healthRegen * Time.deltaTime);
            }

            else {
                leBrute.currentHealth = leBrute.maxHealth;
            }
        }
    }
}
