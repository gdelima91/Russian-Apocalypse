using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class Enemy1_StateMachine : StateMachineGeneric<LE_Enemy1> {

    LE_Enemy1 leEnemy;
    public NavMeshAgent angent;
    public V.Gun gun;

    protected override void Init_Target_InitState()
    {
        angent.enabled = true;
        leEnemy = GetComponent<LE_Enemy1>();
        Configure(leEnemy, Enemy1_Partrol.Instance);
    }

    public Component DoWhatEver()
    {
        return GetComponent<Transform>();
    }

}
