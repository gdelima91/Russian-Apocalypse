using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(EnemyBark_List))]

public class EnemyBarkManager : MonoBehaviour {

    AudioSource audioSource;
    EnemyBark_List barkList;
    AudioClip barkClip;
    Vector2 oldBark;
    bool canPlay = true;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        barkList = GetComponent<EnemyBark_List>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void PlayClip(PlayerBark_List.FolderName folderInt) {
        if (canPlay) {

            canPlay = false;
            int a = (int)folderInt - 1;
            int b = Random.Range(0, barkList.clips[a].Length);

            while (oldBark.x == a && oldBark.y == b) {
                b = Random.Range(0, barkList.clips[a].Length);
            }

            barkClip = barkList.clips[a][b];

            audioSource.PlayOneShot(barkClip);

            oldBark.x = a;
            oldBark.y = b;

            StartCoroutine("PlayWait");
        }
    }

    IEnumerator PlayWait() {
        yield return new WaitForSeconds(barkClip.length);
        canPlay = true;
    }
}
