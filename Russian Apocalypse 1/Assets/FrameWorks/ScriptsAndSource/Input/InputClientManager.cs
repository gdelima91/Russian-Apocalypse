using UnityEngine;

//  This Script is touch to A LE Entity.  
//  For different Weapon or in different game Play model. Input may raise different 
//Action.
//  For example: If the inputActionClient is a Gun, Then GetKey_A(), the LEEntity
//Will shoot the gun, and use the shot animation....When the inputActionClient is a 
//sword, the LEEntity will play sword animation
//
public class InputClientManager : MonoBehaviour {

    protected Transform rightHandTF;
    public LEUnitAnimatorManager animationManager;
    IInputClient inputClient;
    public int actionItemPhycislayer = 0;

    protected virtual void OnEnable()
    {
        rightHandTF = GetComponentInChildren<RightHandHolder>().transform;
        animationManager = GetComponent<LEUnitAnimatorManager>();
    }

    public void GetKey_A() {if (inputClient != null) inputClient.GetKey_A(); }
    public void GetKey_A_Down() { if (inputClient != null) inputClient.GetKey_A_Down(); }
    public void GetKey_A_Up() { if (inputClient != null) inputClient.GetKey_A_Up(); }
    public void GetKey_B_Down() { if (inputClient != null) inputClient.GetKey_B_Down(); }
    public void ShutDown() { if (inputClient != null) inputClient.ShutDown();}

    //Reset InputAction Client and Init the Client.
    public void ResetClient(IInputClient client)
    {
        if(inputClient!=null)
            inputClient.ShutDown();
        inputClient = client;
        inputClient.Init(this);
        inputClient.SetUpLayer(gameObject.layer + 1);    
    }

    public void ChangeAnimationMotionType(LEUnitAnimatorManager.AnimationMotionType type)
    {
        animationManager.SetMotionType(type);
    }

    public Transform RightHandMid1 { get { return rightHandTF; } }

    public LEUnitAnimatorManager AnimationManager { get { return animationManager; } }

    public void SetIInputClientAttackStatu(bool s)
    {
        if(inputClient!=null)
            inputClient.SetIInputActableItemStatu(s);
    }
}
