using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class InputableObject : MonoBehaviour {

    public abstract void Init(LEInputableObjectManager manager);
    public abstract System.Type IDType { get; }
    public void ShutDown() { }
    public void SetUpLayer(int layer) { }
    public void DisableCollision() { }
    public void EnableCollision() { }

    public virtual void Key_A_On(){}
    public virtual void Key_A_Down(){}
    public virtual void Key_A_Up(){}

    public virtual void Key_B_On() { }
    public virtual void Key_B_Down() { }
    public virtual void Key_B_Up() { }

    public virtual void LeftMouse_On() { }
    public virtual void LeftMouse_Down() { }
    public virtual void LeftMouse_Up() { }

    public virtual void RightMouse_On() { }
    public virtual void RightMouse_Down() { }
    public virtual void RightMouse_Up() { }

    public virtual void MiddleMouse_On() { }
    public virtual void MiddleMouse_Down() { }
    public virtual void MiddleMouse_Up() { }

    /// <summary>
    /// We Play the Game and Animation In Eidtor Model. And Set the Item to the Proper Local Position and Rotation.
    /// Then Copy the Transform Value
    /// Then Exit the Editor Play Model, and Paste the Transform value. and Then Call the Function 
    /// To record IAO Offset Info for the LEEntity.
    /// </summary>
    [ContextMenu("Record IAO Offset Info")]
    public void EditorTime_Manually_Initialization()
    {
        LEInputableObjectManager manager = transform.root.GetComponent<LEInputableObjectManager>();
        if (manager == null)
        {
            Debug.LogError("The ROOT dont have a compoent of LEInputableObjectManager");
        }
        else
        {
            manager.Record_IAO_Offset_Info(IDType, transform.localPosition, transform.localRotation);
        }
    }
}

