using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    //this is the Dependence of LEInputObject
    public class Brute_IAOM_Dependence : LEInputableObjectManager_Dependence {

        public List<IAO_Offset_Info> iao_Offset_info = new List<IAO_Offset_Info>();

        #region Implementation Dependence

        public override void Init_Dependence(InputableObject obj)
        {
            if (obj.Type == typeof(Weapon_Ak47))
            {
                Weapon_Ak47 ak47 = obj as Weapon_Ak47;
                ak47.BIND_LEFTMOUSE_ON(Check_Left_Mouse_Click);
                ak47.BIND_LEFTMOUSE_ON(ak47.OnTriggerHold);
                ak47.BIND_LEFTMOUSE_UP(ak47.OnTriggerRelease);
            }
        }
      
        void Check_Left_Mouse_Click(InputableObject ak47)
        {
            RaycastHit hit;// = new RaycastHit();
            MouseAndCamera.GetScreenPointToRayColliderInfo(out hit);
            if (hit.collider.transform.root.GetComponent<LEIdentification>())
            {
                transform.LookAt(hit.collider.transform.position);
                ak47.transform.LookAt(hit.collider.bounds.center);
            }
        }

        #endregion

    }
}
