using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class Weapon_Ak47 : Gun
    {
        public AudioClip gunShot;

        void Update() {
            if (Input.GetMouseButton(0)) {
                OnTriggerHold();
            }
            if (Input.GetMouseButtonUp(0)) {
                OnTriggerRelease();
            }
        }
    }
}
