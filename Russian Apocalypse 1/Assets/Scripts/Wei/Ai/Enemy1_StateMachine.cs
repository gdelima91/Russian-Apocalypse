using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy1_StateMachine : StateMachineGeneric<LE_Enemy1> {

    LE_Enemy1 leEnemy;

    protected override void Init_Target_InitState()
    {
        leEnemy = GetComponent<LE_Enemy1>();
        Configure(leEnemy, Enemy1_Partrol.Instance);
    }
}
