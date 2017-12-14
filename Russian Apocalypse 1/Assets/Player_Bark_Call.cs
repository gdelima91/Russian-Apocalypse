﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bark_Call : MonoBehaviour {

    public PlayerBarkManager pBM;
    public float rNGNumberToBeHigherThan = 25;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RNGBark ()
    {

        int rng = Random.Range(0, 100);

        if (rng > rNGNumberToBeHigherThan) {
            if (pBM != null) {
                pBM.PlayClip(PlayerBark_List.FolderName.OnEnemyKill);
            }
            
        }
    }
}
