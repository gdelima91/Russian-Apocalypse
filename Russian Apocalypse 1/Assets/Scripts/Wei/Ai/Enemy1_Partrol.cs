﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Partrol : AiStateInterface<LE_Enemy1>  {

    public static readonly Enemy1_Partrol Instance = new Enemy1_Partrol();


    float timer = 0.0f;

    public override void Enter(LE_Enemy1 entity)
    {

        Enemy1_StateMachine stateMachine = entity.StateMachine as Enemy1_StateMachine;
        stateMachine.Anima_Set_Float("Speed Z", 1.0f);

        if (TempManager.Instance.playerCollider != null)
            stateMachine.Set_Nav_Destination(TempManager.Instance.playerCollider.transform.position);
    }

    public override void Execute(LE_Enemy1 entity)
    {
        Enemy1_StateMachine stateMachine = entity.StateMachine as Enemy1_StateMachine;
        stateMachine.fieldOfView.DebugDrawFielOfView();

        //LEIdentification id = stateMachine.fieldOfView.Get_Nearest_T_InsideFiledOfViewRange<LEIdentification>(Can_SeePlayer, 0.5f, ref timer, stateMachine.targetLayer);
        //if (id != null) {
        //    Debug.Log(id.name);
        //    stateMachine.currentTFTarget = id.transform;
        //    stateMachine.ChangeState(Enemy1_Fight.Instance);
        //    return;
        //}

        bool findPlayer = stateMachine.fieldOfView.See_Collider_InFieldOfView(TempManager.Instance.playerCollider);
        if (findPlayer)
        {
            stateMachine.currentTFTarget = TempManager.Instance.playerCollider.transform.root;
            stateMachine.ChangeState(Enemy1_Fight.Instance);
            return;
        }

        if (stateMachine.Check_ArriveDestination_NotPathPending()) {
            Vector3 pos = stateMachine.Get_RandomPosXZ_BasedOnCurrentPos(-20, 20);
            bool walkable = stateMachine.Check_PositionWalkAble(pos);
            Debug.Log(walkable);
            if (walkable) {
                GizmosDebuger.Instance.spherePos = pos;
                stateMachine.Set_Nav_Destination(pos);
            }

        }
    }

    public override void Exit(LE_Enemy1 entity)
    {
      
    }

    public bool Can_SeePlayer(LEIdentification id,AiUtility.FieldOfView fieldOfView)
    {
        Debug.Log("????????????????????");
        bool isPlayer = id.letype == LEType.Player;
        if (isPlayer) { Debug.Log(id); }
        bool canSee = id.GetComponent<LEPhysicsManager>().Check_DirectlyRaycast(fieldOfView.eye.position);

        return (isPlayer && canSee);

    }
}
