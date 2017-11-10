using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Chase : AiStateInterface<LE_Enemy1> {

    public static readonly Enemy1_Chase Instance = new Enemy1_Chase();


    public override void Enter(LE_Enemy1 entity)
    {
        Enemy1_StateMachine stateMachine = entity.StateMachine as Enemy1_StateMachine;
        stateMachine.Set_Nav_Destination(stateMachine.currentTFTarget.position);
    }

    public override void Execute(LE_Enemy1 entity)
    {
        Enemy1_StateMachine stateMachine = entity.StateMachine as Enemy1_StateMachine;

        stateMachine.Look_At_Target();
        float dstToTarget = stateMachine.Get_Distance_To_Target();
        if (dstToTarget > 5)
        {
            if (stateMachine.Check_Target_NewPosition(0.25f))
            {
                stateMachine.Approach_Target(3.0f);
            }
        }
        else {
            if (stateMachine.Check_ArriveDestination_NotPathPending())
            {
                
            }
        }
        

    }

    public override void Exit(LE_Enemy1 entity)
    {
        
    }

}
