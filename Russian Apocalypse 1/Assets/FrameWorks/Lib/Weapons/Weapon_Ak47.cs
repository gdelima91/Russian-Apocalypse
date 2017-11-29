using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class Weapon_Ak47 : Gun
    {
        public override System.Type Type { get { return typeof(Weapon_Ak47); } }

        public override SLOTID[] SlotIDs
        {
            get
            {
                return new SLOTID[] { SLOTID.RightHand};
            }
        }

    }
}
