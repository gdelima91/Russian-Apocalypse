using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PlayerBark_List))]

public class PlayerBarkManager : MonoBehaviour {

#region Variables

    AudioSource audioSource;
    PlayerBark_List barkList;
    AudioClip barkClip;
    Vector2 oldBark;
    bool canPlay = true;

#endregion

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        barkList = GetComponent<PlayerBark_List>();
	}

    // For test purposes
    void PlayerInput() {
        if (Input.GetKey(KeyCode.Alpha1)) {
            PlayClip(PlayerBark_List.FolderName.OnDeath);
        }

        if (Input.GetKey(KeyCode.Alpha2)) {
            PlayClip(PlayerBark_List.FolderName.OnEnemyKill);
        }

        if (Input.GetKey(KeyCode.Alpha3)) {
            PlayClip(PlayerBark_List.FolderName.OnGenericPowerUp);
        }

        if (Input.GetKey(KeyCode.Alpha4)) {
            PlayClip(PlayerBark_List.FolderName.OnGettingDualWieldGun);
        }

        if (Input.GetKey(KeyCode.Alpha5)) {
            PlayClip(PlayerBark_List.FolderName.OnGettingGenericWeapon);
        }

        if (Input.GetKey(KeyCode.Alpha6)) {
            PlayClip(PlayerBark_List.FolderName.OnGettingHit);
        }

    }

    void PlayClip (PlayerBark_List.FolderName folderInt) {
        if (canPlay)
        {
            canPlay = false;
            int a = (int)folderInt - 1;
            int b = Random.Range(0, barkList.clips[a].Length);

            while (oldBark.x == a && oldBark.y == b)
            {
                b = Random.Range(0, barkList.clips[a].Length);
            }

            barkClip = barkList.clips[a][b];

            audioSource.PlayOneShot(barkClip);

            oldBark.x = a;
            oldBark.y = b;

            StartCoroutine("PlayWait");
        }
    }

    IEnumerator PlayWait () {
        yield return new WaitForSeconds(barkClip.length);
        canPlay = true;
    }
}
