using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour {

    private gunManager gunManager;
    private float defaultShotgunTimer;
    private float shotgunTimer = 0;

	// Use this for initialization
	void Start () {
        gunManager = GetComponentInChildren<gunManager>();
	}
	
	// Update is called once per frame
	void Update () {
        Timer();
	}

    public void EquipShotgun(float timer) {
        gunManager.Equp(1);
        gunManager.monos[0].gameObject.SetActive(false);
        shotgunTimer = timer;
    }

    void Timer () {
        if (shotgunTimer > 0) {
            shotgunTimer -= Time.deltaTime;
        }
        else {
            gunManager.Equp(0);
            gunManager.monos[1].gameObject.SetActive(false);
        }
    }

    void RefreshTimer () {
        shotgunTimer = defaultShotgunTimer;
    }
}
