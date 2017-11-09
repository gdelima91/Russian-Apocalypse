using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public abstract class LEAnimationCallBack : MonoBehaviour {
    LEMainBase leMainBase;
    private void Start()
    {
        leMainBase = GetComponent<LEMainBase>();
    }
    //This function need to be public and static
    public static void Non() { Debug.LogError("This Function Should Not be Called"); }

    public static void NonFloat(float number) { Debug.LogError("This Function Should Not be Called"); }

    public static void NonBool(bool value) { Debug.LogError("This Function Should Not be Called"); }

    public static void NonVec3(Vector3 vec3) { Debug.LogError("This Function Should Not be Called"); }

    protected void Dispatch_Animation_Message(AnimationMessageType messageType, object messageValue) { leMainBase.Dispatch_Animation_Message(messageType, messageValue); }

    public abstract void InvokeMethod(MethodInfo m, object[] obj);
}

public enum AnimationMessageType
{
    SetBasicMoveMent_ActiveStatu,
    SetAnimation_AttackStatue,
    LookAtTarget,
}