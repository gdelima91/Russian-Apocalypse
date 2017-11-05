using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class LEAnimatorManager : MonoBehaviour {

    [HideInInspector]
    public Animator animator;

    float currentVelocity;
    bool enableMotionInput = true;
    float nextInputTime;

    LEAnimationCallBack callback;

    System.Random rand = new System.Random();
    // Use this for initialization

    private void OnEnable()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        if (animator == null)
            Debug.LogError("There is no Animator in this Object, or its children");
    }

    protected virtual void Start () {
        callback = GetComponent<LEAnimationCallBack>();
    }

    public void UpdateAnimation() { }

    public virtual void SetKeyStatue(InputIndex index,bool state) { animator.SetBool(index.ToString(), state); }

    public virtual void SetMovementForward(float forward) { animator.SetFloat("Forward", forward); }

    public void SetMovementStrafe(float strafe) { animator.SetFloat("Strafe", strafe); }

    public void SetMovementForwardSmoothDamp(float forward)
    {
        float current = animator.GetFloat("Forward");
        float newCurrent = Mathf.SmoothDamp(current, forward,ref currentVelocity, 0.15f);
        animator.SetFloat("Forward", newCurrent);
    }

    public void SetMotionType(AnimationMotionType type) {if(enableMotionInput) animator.SetInteger("MotionType", (int)type); }

    public void Force_SetMotionType(AnimationMotionType type)
    {
        animator.SetInteger("MotionType", (int)type);
    }

    public void SetMotionIndex(int motionIndex) {if(enableMotionInput)animator.SetInteger("MotionIndex", motionIndex); }

    public void Force_SetMotionIndex(int motionIndex) { animator.SetInteger("MotionIndex", motionIndex); }

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

    public void Force_SetBool(string name, bool value)
    {
        animator.SetBool(name, value);
    }

    public void SetTrigger(string name) {
        if (enableMotionInput)
            animator.SetTrigger(name);
    }

    public void Force_SetTrigger(string name) { animator.SetTrigger(name); }

    public void SetFloat(string name, float value) { if (enableMotionInput) animator.SetFloat(name, value);}

    public void Force_SetFloat(string name, float value) { animator.SetFloat(name, value); }

    public Animator GetAnimator()
    {
        return animator;
    }

    public void InvokeMethod(MethodInfo m, object[] obj) { if (callback != null) { callback.InvokeMethod(m, obj); } else { Debug.Log("Animation CallBack Component is Null"); } }

    public enum AnimationMotionType
    {
        IWR_0,
        MELEE_1,
        HoldGun_2,
        STUFF_3
    }

}