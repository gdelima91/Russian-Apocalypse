using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineBase : MonoBehaviour
{
    //Those Value will be assigned. When SateMachine Base First Time be accessed from the LEMainBase.....
    [HideInInspector] public LEMainBase leBase;
    [HideInInspector] public LETransiationManager transitionManager;
    [HideInInspector] public LEAnimatorManager animatorManager;


    public bool Ckeck_FindPlayer()
    {
        return false;
    }

    public Vector3 Get_RandomPos(float from,float to)
    {
        Vector3 pos = Vector3.zero;
        pos = pos.RandomPos(from, to);
        return pos;
    }

    public Vector3 Get_RandomPos_BaseOnCurrentPos(float from, float to)
    {
        Vector3 offset = Vector3.zero;
        offset = offset.RandomPos(from, to);
        return transform.position + offset;
    }

    public Vector2 Get_RandomPosXZ_BasedOnCurrentPos(float from, float to)
    {
        Vector3 offset = Vector3.zero;
        offset =  offset.RandomPosXZ(from, to);
        return transform.position + offset;
    }

    public void Set_Nav_Destination(Vector3 pos)
    {
        transitionManager.Set_Nav_Destination(pos);
    }

    public bool Check_PositionWalkAble(Vector3 pos)
    {
       return transitionManager.SamplePosition(pos);
    }

    public bool ArriveDestination_NotPathPending()
    {
        return transitionManager.ArriveDestination_NotPathPending();
    }

    public void Anima_Set_Float(string name,float value)
    {
        animatorManager.SetFloat(name, value);
    }


}
