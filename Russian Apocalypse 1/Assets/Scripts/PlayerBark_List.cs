using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBark_List : MonoBehaviour {

    public List<AudioClip[]> clips = new List<AudioClip[]>();

    public enum FolderName : int {
        OnDeath                 = 1,
        OnEnemyKill             = 2,
        OnGenericPowerUp        = 3,
        OnGettingDualWieldGun   = 4,
        OnGettingGenericWeapon  = 5,
        OnGettingHit            = 6,
        OnGettingMachineGun     = 7,
        OnSurrounded            = 8,
        OnVodkaPowerUp          = 9,
        ReactingToLevel         = 10,
        Waiting                 = 11,
        Misc                    = 11
    }

    void Start() {
        clips.Add(Resources.LoadAll<AudioClip>("Sounds/Quotes/OnDeath"));
        clips.Add(Resources.LoadAll<AudioClip>("Sounds/Quotes/OnEnemyKill"));
        clips.Add(Resources.LoadAll<AudioClip>("Sounds/Quotes/OnGenericPowerUp"));
        clips.Add(Resources.LoadAll<AudioClip>("Sounds/Quotes/OnGettingDualWieldGun"));
        clips.Add(Resources.LoadAll<AudioClip>("Sounds/Quotes/OnGettingGenericWeapon"));
        clips.Add(Resources.LoadAll<AudioClip>("Sounds/Quotes/OnGettingHit"));
        clips.Add(Resources.LoadAll<AudioClip>("Sounds/Quotes/OnGettingMachineGun"));
        clips.Add(Resources.LoadAll<AudioClip>("Sounds/Quotes/OnSurrounded"));
        clips.Add(Resources.LoadAll<AudioClip>("Sounds/Quotes/OnVodkaPowerUp"));
        clips.Add(Resources.LoadAll<AudioClip>("Sounds/Quotes/ReactingToLevel"));
        clips.Add(Resources.LoadAll<AudioClip>("Sounds/Quotes/Waiting"));
        clips.Add(Resources.LoadAll<AudioClip>("Sounds/Quotes/Misc"));
    }
}
