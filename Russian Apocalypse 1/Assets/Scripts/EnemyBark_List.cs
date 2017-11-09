using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBark_List : MonoBehaviour {

    public List<AudioClip[]> clips = new List<AudioClip[]>();

    public enum FolderName : int {
        onHit   = 1,
        onDeath = 2
    }

    // Use this for initialization
    void Start () {
        clips.Add(Resources.LoadAll<AudioClip>("Sounds/Quotes/onHit"));
        clips.Add(Resources.LoadAll<AudioClip>("Sounds/Quotes/onDeath"));
    }
}
