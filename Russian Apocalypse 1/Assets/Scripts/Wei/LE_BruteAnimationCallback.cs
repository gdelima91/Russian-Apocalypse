using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class LE_BruteAnimationCallback : LEAnimationCallBack {



    public override void InvokeMethod(MethodInfo m, object[] obj)
    {
        m.Invoke(this, obj);
    }
}
