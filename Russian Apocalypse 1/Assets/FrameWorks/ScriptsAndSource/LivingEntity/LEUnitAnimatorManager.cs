using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public abstract class LEUnitAnimatorManager : MonoBehaviour {

    LEUnitProcessorBase processor;

    [HideInInspector]
    public Animator animator;

    float currentVelocity;
    bool enableMotionInput = true;
    float nextInputTime;

    System.Random rand = new System.Random();
    // Use this for initialization

    private void OnEnable()
    {
        processor = GetComponent<LEUnitProcessorBase>();
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        if (animator == null)
            Debug.LogError("There is no Animator in this Object, or its children");
    }

    protected virtual void Start () {
        
    }

    public abstract void UpdateAnimation();

    public virtual void SetKeyStatue(InputIndex index,bool state) { animator.SetBool(index.ToString(), state); }

    public virtual void SetMovementForward(float forward) { animator.SetFloat("Forward", forward); }

    public void SetMovementForwardSmoothDamp(float forward)
    {
        float current = animator.GetFloat("Forward");
        float newCurrent = Mathf.SmoothDamp(current, forward,ref currentVelocity, 0.15f);
        animator.SetFloat("Forward", newCurrent);
    }

    public void SetMovementStrafe(float strafe) { animator.SetFloat("Strafe", strafe); }

    public void SetMotionType(AnimationMotionType type) {if(enableMotionInput) animator.SetInteger("MotionType", (int)type); }

    public void SetMotionTypeImmediately(AnimationMotionType type)
    {
        animator.SetInteger("MotionType", (int)type);
    }

    public void SetMotionIndex(int motionIndex) {if(enableMotionInput)animator.SetInteger("MotionIndex", motionIndex); }

    public void SetMotionIndexImmediately(int motionIndex) { animator.SetInteger("MotionIndex", motionIndex); }

    public void SetMotionIndex_Random_From_To(int from, int to)
    {
        nextInputTime += Time.deltaTime;
        if (nextInputTime > 3.0f)
        {
            int index = rand.Next(from, to);
            animator.SetInteger("MotionIndex", index);
            nextInputTime = 0.0f;
        }
    }

    public void SetBool(string name, bool value)
    {
        if (enableMotionInput)
            animator.SetBool(name, value);
    }

    public void SetBoolImmediately(string name, bool value)
    {
        animator.SetBool(name, value);
    }

    public void SetTrigger(string name) {
        if (enableMotionInput)
            animator.SetTrigger(name);
    }

    public void SetTriggerImmediately(string name) { animator.SetTrigger(name); }

    public Animator GetAnimator()
    {
        return animator;
    }

    [Visin1_1.AMBCallback()]
    public virtual void PlaySound_82()
    {
        AudioManager.instance.PlaySound2DRandom("SwordAttack");
    }

    [Visin1_1.AMBCallback()]
    public virtual void EnableBasicMoveMent() {
        if (processor == null) return;
        processor.Dispatch_Animation_Message(AnimationMessageType.SetBasicMoveMent_ActiveStatu,true);
    }

    [Visin1_1.AMBCallback()]
    public virtual void DisableBasicMovement() {
        if (processor == null) return;
        processor.Dispatch_Animation_Message(AnimationMessageType.SetBasicMoveMent_ActiveStatu, false);
    }

    [Visin1_1.AMBCallback()]
    public virtual void Attack_Statue_On()
    {
        if (processor == null) return;
        processor.Dispatch_Animation_Message(AnimationMessageType.SetAnimation_AttackStatue, true);
    }

    [Visin1_1.AMBCallback()]
    public virtual void Attack_Statue_Off()
    {
        if (processor == null) return;
        processor.Dispatch_Animation_Message(AnimationMessageType.SetAnimation_AttackStatue, false);
    }

    [Visin1_1.AMBCallback()]
    public virtual void DisableMotionInput()
    {
        enableMotionInput = true;
    }

    [Visin1_1.AMBCallback()]
    public virtual void OnableMotionInput()
    {
        enableMotionInput = false;
    }

    [Visin1_1.AMBCallback()]
    public virtual void LookAtTarget()
    {
        if (processor == null) return;
        processor.Dispatch_Animation_Message(AnimationMessageType.LookAtTarget, null);
    }

    [Visin1_1.AMBCallback()]
    public virtual void XixiHaha()
    {
        Debug.Log("Xi Xi Ha Ha");
    }

    public abstract void InvokeMethod(MethodInfo m,object[] obj);


    //This function need to be public and static
    public static void Non(){ Debug.LogError("This Function Should Not be Called"); }

    public static void NonFloat(float number) { Debug.LogError("This Function Should Not be Called"); }

    public static void NonBool(bool value) { Debug.LogError("This Function Should Not be Called"); }

    public static void NonVec3(Vector3 vec3) { Debug.LogError("This Function Should Not be Called"); }

    //this is used to blending to 
    public virtual void Dispatch_Message(AnimationMessageType messageType, object messageValue)
    {

    }

    public enum AnimationMotionType
    {
        IWR_0,
        MELEE_1,
        HoldGun_2,
        STUFF_3
    }
}

public enum AnimationMessageType
{
    SetBasicMoveMent_ActiveStatu,
    SetAnimation_AttackStatue,
    LookAtTarget,
}