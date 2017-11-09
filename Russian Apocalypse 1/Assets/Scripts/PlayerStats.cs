using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public float maxHealth;
    public float healthRegen;

    public bool canHeal = true;
    public float currentHealth = 50;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Heal ();
	}

    void Heal () {
        if (canHeal) {
            if (currentHealth < maxHealth) {
                currentHealth = currentHealth + healthRegen * Time.deltaTime;
            }

            if (currentHealth > maxHealth) {
                currentHealth = maxHealth;
            }
        }
    }

    float GetCurrentHealth() {
        return currentHealth;
    }
}
