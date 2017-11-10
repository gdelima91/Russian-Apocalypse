using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Partrol : AiStateInterface<LE_Enemy1>  {

    public static readonly Enemy1_Partrol Instance = new Enemy1_Partrol();


    float timer = 0.0f;

    public override void Enter(LE_Enemy1 entity)
    {
        entity.StateMachine.Anima_Set_Float("Speed Z", 1.0f);

    }

    public override void Execute(LE_Enemy1 entity)
    {
        Enemy1_StateMachine stateMachine = entity.StateMachine as Enemy1_StateMachine;

        stateMachine.fieldOfView.DebugDrawFielOfView();
        LEIdentification id = stateMachine.fieldOfView.Get_Nearest_T_InsideFiledOfView<LEIdentification>(Is_Player, 0.5f, ref timer, stateMachine.targetLayer);

        if (id != null) {
            stateMachine.currentTFTarget = id.transform;
            stateMachine.ChangeState(Enemy1_Chase.Instance);
            return;
        }

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

    public bool Is_Player(LEIdentification id)
    {
        return id.letype == LEType.Player;
    }
}
