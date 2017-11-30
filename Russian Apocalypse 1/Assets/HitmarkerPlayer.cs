using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitmarkerPlayer : MonoBehaviour {

    public AudioClip hitmarker;

    public void PlayHitmarker()
    {
        GetComponent<AudioSource>().PlayOneShot(hitmarker);
    }
}
