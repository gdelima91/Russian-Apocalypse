using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Fight : AiStateInterface<LE_Enemy1> {


    public static readonly Enemy1_Fight Instance = new Enemy1_Fight();


    public override void Enter(LE_Enemy1 entity)
    {
        Enemy1_StateMachine stateMachine = entity.StateMachine as Enemy1_StateMachine;

        stateMachine.Set_Nav_Destination(stateMachine.currentTFTarget.position);
    }

    public override void Execute(LE_Enemy1 entity)
    {
        Enemy1_StateMachine stateMachine = entity.StateMachine as Enemy1_StateMachine;
        stateMachine.Look_At_Target_XZ();
        stateMachine.CheckSet___CHECK_InRange_SET_KeepTargetInRange(5.0f,3.0f);

        /*
         Shoot ..........
         */
        stateMachine.GetComponent<LEInputableObjectManager>().Get_LeftMouse();

        
        
    }


    public override void Exit(LE_Enemy1 entity)
    {
        
    }

}
