using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HP_Manager : MonoBehaviour {

    public Slider slider;
    private LE_Brute leBrute;

	// Use this for initialization
	void Start () {
        leBrute = GetComponent<LE_Brute>();
	}
	
	// Update is called once per frame
	void Update () {
        SetHP();
	}

    void SetHP() {
        slider.value = leBrute.currentHealth / leBrute.maxHealth;
    }
}
