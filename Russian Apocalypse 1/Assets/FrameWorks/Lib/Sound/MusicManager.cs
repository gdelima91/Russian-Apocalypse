﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public AudioClip mainTheme;
	public AudioClip menuTheme;

	void Start()
	{
        //Debug.Log("Play Music");
		AudioManager.Instance.PlayMusic(menuTheme,3);
        Invoke("PlayMusic1",3.0f );
    }

    void PlayMusic1()
    {
        AudioManager.Instance.PlayMusic(mainTheme, 1);
    }
	void Update()
	{
			

	}
	
}
