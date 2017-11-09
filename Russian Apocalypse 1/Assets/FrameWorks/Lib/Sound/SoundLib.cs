﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLib : MonoBehaviour {

	public SoundGroup[] soundGroups; 
	Dictionary<string,AudioClip[]> groupDictionary = new Dictionary<string, AudioClip[]>();

	void Awake()
	{
		foreach(SoundGroup soundGroup in soundGroups)
		{
			groupDictionary.Add(soundGroup.groupName,soundGroup.clips);
		}
	}

	public AudioClip GetClipFromName_Random(string name)
	{
		if(groupDictionary.ContainsKey(name))
		{
            AudioClip[] sounds= groupDictionary[name];
			return sounds[Random.Range(0,sounds.Length)];
		}
		return null;
	}

    public AudioClip GetClipFromName(string name,int index)
    {
        if (groupDictionary.ContainsKey(name))
        {
            AudioClip[] sounds = groupDictionary[name];
            if(sounds.Length<index)
                return sounds[index];
        }
        return null;
    }


	[System.Serializable]
	public class SoundGroup
	{
		public string groupName;
		public AudioClip[] clips;
	}
}
