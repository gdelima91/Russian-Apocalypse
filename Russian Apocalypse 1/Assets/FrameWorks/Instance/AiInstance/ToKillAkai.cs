using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
using V.AI;

[BehaviorTask(typeof(LEMainBase))]
public class MoveToTarget : Behavior {

    public override Status Update()
    {
        return Status.BhRunning;
    }
}
