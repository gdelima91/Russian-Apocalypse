﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bark_Call : MonoBehaviour {

    public PlayerBarkManager pBM;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDestroy ()
    {
        //Note:  When OnDestroy get called.
        //       pBM will also be release from the memory.....
        
        //int rng = Random.Range(0, 100);

        //if(rng > 25)
        //{
        //    pBM.PlayClip(PlayerBark_List.FolderName.OnEnemyKill);
        //}  
    }
}
