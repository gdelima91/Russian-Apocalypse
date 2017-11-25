using UnityEngine;
using System.Collections.Generic;
using System.Linq;

//  This Script is touch to A LE Entity.  
//  For different Weapon or in different game Play model. Input may raise different 
//Action.
//  For example: If the inputActionClient is a Gun, Then GetKey_A(), the LEEntity
//Will shoot the gun, and use the shot animation....When the inputActionClient is a 
//sword, the LEEntity will play sword animation
//
public class LEInputableObjectManager : MonoBehaviour {

    public List<IAO_Offset_Info> iao_Offset_info = new List<IAO_Offset_Info>();

    protected Transform rightHandTF;
    private LEAnimatorManager animationManager;

    public InputableObject initInputObject;

    InputableObject inputableObject;
    public int actionItemPhycislayer = 0;

    private void Start()
    {
        if (initInputObject != null) { ResetClient(initInputObject); }
    }

    protected virtual void OnEnable()
    {
        RightHandHolder righthand = GetComponentInChildren<RightHandHolder>();
        if (righthand != null)
        {
            rightHandTF = righthand.transform;
        }
        animationManager = GetComponent<LEAnimatorManager>();
    }

    private void LateUpdate()
    {
        
    }

    public void GetKey_A() {if (inputableObject != null) inputableObject.Key_A_On(); }
    public void GetKey_A_Down() { if (inputableObject != null) inputableObject.Key_A_Down(); }
    public void GetKey_A_Up() { if (inputableObject != null) inputableObject.Key_A_Up(); }

    public void GetKey_B() { if (inputableObject != null) inputableObject.Key_B_On(); }
    public void GetKey_B_Down() { if (inputableObject != null) inputableObject.Key_B_Down(); }
    public void GetKey_B_Up() { if (inputableObject != null) inputableObject.Key_B_Up(); }

    public void Get_LeftMouse() { if (inputableObject != null) inputableObject.LeftMouse_On();}

    public void ShutDown() { if (inputableObject != null) inputableObject.ShutDown();}

    //Reset InputAction Client and Init the Client.
    public void ResetClient(InputableObject client)
    {
        if(inputableObject!=null)
            inputableObject.ShutDown();
        inputableObject = client;
        inputableObject.Init(this);
        inputableObject.SetUpLayer(gameObject.layer + 1);
        SetUpOffset(client, client.IDType);
    }

    //InputableObject Call those Functions and Fields----
    public void ChangeAnimationMotionType(LEAnimatorManager.AnimationMotionType type)
    {
        animationManager.SetMotionType(type);
    }

    public Transform RightHand { get { return rightHandTF; } }

    public LEAnimatorManager AnimationManager { get { return animationManager; } }

    /// <summary>
    /// This function should be called at Editor time.......To optimize Some Of the Initalization.
    /// Especially for the Function GetComponentInChildren<T> inside the Awake,or Start
    /// </summary>
    [ContextMenu("Manual_Init_InputableObject")]
    void Manual_Init_InputableObject()
    {
        initInputObject = GetComponentInChildren<InputableObject>();
    }

    public void Record_IAO_Offset_Info(System.Type type, Vector3 pos, Quaternion rot)
    {
        if (iao_Offset_info.Count > 0)
        {
            IAO_Offset_Info info = iao_Offset_info.First(i => i.IAOType == type.ToString());
            if (info != null)
            {
                info.posOffset = pos;
                info.rotOffset = rot;
            }
            else
            {
                iao_Offset_info.Add(new IAO_Offset_Info(type, pos, rot));
            }
        }
        else {
            iao_Offset_info.Add(new IAO_Offset_Info(type, pos, rot));
        }
    }

    void SetUpOffset(InputableObject obj,System.Type type)
    {
        if (iao_Offset_info.Count > 0)
        {
            IAO_Offset_Info info = iao_Offset_info.First(i => i.IAOType == type.ToString());
            if (info != null)
            {
                obj.transform.localPosition = info.posOffset ;
                obj.transform.localRotation = info.rotOffset;
            }
        }
    }
}

//this class contains the offset information.
//Becaise Different LEEntity may have different posture to handle weapoon......
[System.Serializable]
public class IAO_Offset_Info
{
    public string IAOType;
    public Vector3 posOffset;
    public Quaternion rotOffset;
    public IAO_Offset_Info(System.Type type, Vector3 pos, Quaternion rot)
    {
        IAOType = type.ToString();
        posOffset = pos;
        rotOffset = rot;
    }
}