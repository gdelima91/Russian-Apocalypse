using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Partrol : AiStateInterface<LE_Enemy1>  {

    public static readonly Enemy1_Partrol Instance = new Enemy1_Partrol();

    public override void Enter(LE_Enemy1 entity)
    {
        entity.StateMachine.Anima_Set_Float("Speed Z", 1.0f);

    }

    public override void Execute(LE_Enemy1 entity)
    {
        StateMachineBase stateMachine = entity.StateMachine;
        if (stateMachine.ArriveDestination_NotPathPending())
        {
           
            Vector3 pos = stateMachine.Get_RandomPosXZ_BasedOnCurrentPos(-20, 20);
            bool walkable = stateMachine.Check_PositionWalkAble(pos);    
            if (walkable) { stateMachine.Set_Nav_Destination(pos); }

        }
    }

    public override void Exit(LE_Enemy1 entity)
    {
      
    }
}
