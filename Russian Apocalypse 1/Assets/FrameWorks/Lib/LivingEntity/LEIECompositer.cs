using UnityEngine;
using System.Collections.Generic;
using System.Linq;


//LE Internal & External Compositer
//
public class LEIECompositer : MonoBehaviour{

    //We should Also move this to Dependence
    public List<IAO_Offset_Info> iao_Offset_info = new List<IAO_Offset_Info>();

    [SerializeField] LEInputableObjectManager_Dependence dependence;
    List<InputableObject> allObjs = new List<InputableObject>();

    Dictionary<int, InputableObject> inputableObjectPair = new Dictionary<int, InputableObject>();
    LEMainBase mainBase;
    LEExternalBase externalBase;

    private void Start()
    {
        dependence = GetComponent<LEInputableObjectManager_Dependence>();
        mainBase = GetComponent<LEMainBase>();
        externalBase = GetComponent<LEExternalBase>();
    }

    public void Composite_InputData(MKInputData data)
    {
        if (data.HoldMask.HasValue((int)InputEnum.Key_A)) { GetKey_A_Hold(); }
        if (data.DownMask.HasValue((int)InputEnum.Key_A)) { GetKey_A_Down(); }
        if (data.UpMask.HasValue((int)InputEnum.Key_A)) { GetKey_A_Up(); }

        if (data.HoldMask.HasValue((int)InputEnum.Key_B)) { GetKey_B_Hold(); }
        if (data.DownMask.HasValue((int)InputEnum.Key_B)){ GetKey_B_Down();}
        if (data.UpMask.HasValue((int)InputEnum.Key_B)) { GetKey_B_Up(); }

        if (data.HoldMask.HasValue((int)InputEnum.LeftMouse)) { Get_LeftMouse_Hold(); }
    }

    void GetKey_A_Hold() { foreach (InputableObject o in allObjs) o.Key_A_Hold(); }
    void GetKey_A_Down() { foreach(InputableObject o in allObjs) o.Key_A_Down(); }
    void GetKey_A_Up() { foreach(InputableObject o in allObjs) o.Key_A_Up(); }

    void GetKey_B_Hold() { foreach (InputableObject o in allObjs) o.Key_B_Hold(); }
    void GetKey_B_Down() { foreach (InputableObject o in allObjs) o.Key_B_Down(); }
    void GetKey_B_Up() { foreach (InputableObject o in allObjs) o.Key_B_Up(); }

    void Get_LeftMouse_Hold() { foreach (InputableObject o in allObjs) o.LeftMouse_Hold();}

    public void LoadDependence_Data()
    {

    }

    public void LoadDependence_Config(InputableObject obj){ if (dependence != null) dependence.Init_Dependence(obj); else { Debug.Log("Inputable Object Manager Need Dependence to excute"); } }

    public void LoadInputableObject(InputableObject obj)
    {

    }

    //InputableObject Call those Functions and Fields----
    public void ChangeAnimationMotionType(LEAnimatorManager.AnimationMotionType type)
    {
        
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

    //this should Move to Dependence component
    void SetUpOffset(InputableObject obj)
    {
        if (iao_Offset_info.Count > 0)
        {
            IAO_Offset_Info info = iao_Offset_info.First(i => i.IAOType == obj.Type.ToString());
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