﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class Weapon_Ak47 : Gun
    {

        public override void Init(LEInputableObjectManager manager)
        {

        }

        public override void LeftMouse_On()
        {
            OnTriggerHold();
        }

        public override void LeftMouse_Up()
        {
            OnTriggerRelease();
        }

        public override System.Type IDType { get { return typeof(Weapon_Ak47); } }
    }
}
