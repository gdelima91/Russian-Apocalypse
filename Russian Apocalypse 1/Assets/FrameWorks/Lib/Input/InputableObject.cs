using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



// Split Data and Logical..........
// Logical contains the interface to Load data.
// Logical contains Action Delegate-----
// The Data will make a configration for the Logical 
// tell the Action delegate how to run the Functions.
// If it is too hard or cost too much.....
// Then The data----contains some Config functions

public abstract class InputableObject : MonoBehaviour {

    public void LoadInputableObject(LEIECompositer manager) {manager.LoadDependence_Config(this); }
    public abstract System.Type Type { get; }
    public abstract SLOTID[] SlotIDs { get; }
    public virtual bool DoubleHand { get { return false; } }

    public void ShutDown() { }
    public void SetUpLayer(int layer) { }
    public void DisableCollision() { }
    public void EnableCollision() { }

    #region Key
    //-----------------------------------------------------------
    //-----------------------------------------------------------
    Action<InputableObject> KEY_A_ON;
    Action<InputableObject> KEY_A_DOWN;
    Action<InputableObject> KEY_A_UP;
    public void BIND_KEY_A_ON(Action<InputableObject> a) { KEY_A_ON -= a; KEY_A_ON += a; }
    public void BIND_KEY_A_DOWN(Action<InputableObject> a) { KEY_A_DOWN -= a; KEY_A_DOWN += a; }
    public void BIND_KEY_A_UP(Action<InputableObject> a) { KEY_A_UP -= a; KEY_A_UP += a; }

    Action<InputableObject> KEY_B_ON;
    Action<InputableObject> KEY_B_DOWN;
    Action<InputableObject> KEY_B_UP;
    public void BIND_KEY_B_ON(Action<InputableObject> a) { KEY_B_ON -= a; KEY_B_ON += a; }
    public void BIND_KEY_B_DOWN(Action<InputableObject> a) { KEY_B_DOWN -= a; KEY_B_DOWN += a; }
    public void BIND_KEY_B_UP(Action<InputableObject> a) { KEY_B_UP -= a; KEY_B_UP += a; }

    Action<InputableObject> LEFTMOUSE_ON;
    Action<InputableObject> LEFTMOUSE_DOWN;
    Action<InputableObject> LEFTMOUSE_UP;
    public void BIND_LEFTMOUSE_ON(Action<InputableObject> a) { LEFTMOUSE_ON -= a; LEFTMOUSE_ON += a; }
    public void BIND_LEFTMOUSE_DOWN(Action<InputableObject> a) { LEFTMOUSE_DOWN -= a; LEFTMOUSE_DOWN += a; }
    public void BIND_LEFTMOUSE_UP(Action<InputableObject> a) { LEFTMOUSE_UP -= a; LEFTMOUSE_UP += a; }
    #endregion

    public void Key_A_Hold(){ if (KEY_A_ON != null) KEY_A_ON(this); }
    public void Key_A_Down() { if (KEY_A_DOWN != null) KEY_A_DOWN(this); }
    public void Key_A_Up() {if (KEY_A_UP != null) KEY_A_UP(this);}

    public void Key_B_Hold() { if (KEY_B_ON != null) KEY_B_ON(this);}
    public void Key_B_Down() { if (KEY_B_DOWN != null) KEY_B_DOWN(this); }
    public void Key_B_Up() {  if (KEY_B_UP != null) KEY_B_UP(this); }

    public void LeftMouse_Hold() {if (LEFTMOUSE_ON != null)LEFTMOUSE_ON(this);}
    public void LeftMouse_Down() {if (LEFTMOUSE_DOWN != null) LEFTMOUSE_DOWN(this);}
    public void LeftMouse_Up() {if (LEFTMOUSE_UP != null) LEFTMOUSE_UP(this);}

    public void RightMouse_On() { }
    public void RightMouse_Down() { }
    public void RightMouse_Up() { }

    public void MiddleMouse_On() { }
    public void MiddleMouse_Down() { }
    public void MiddleMouse_Up() { }

    /// <summary>
    /// We Play the Game and Animation In Eidtor Model. And Set the Item to the Proper Local Position and Rotation.
    /// Then Copy the Transform Value
    /// Then Exit the Editor Play Model, and Paste the Transform value. and Then Call the Function 
    /// To record IAO Offset Info for the LEEntity.
    /// </summary>
    [ContextMenu("Record IAO Offset Info")]
    public void EditorTime_Manually_Initialization()
    {
        LEIECompositer manager = transform.root.GetComponent<LEIECompositer>();
        if (manager == null)
        {
            Debug.LogError("The ROOT dont have a compoent of LEInputableObjectManager");
        }
        else
        {
            manager.Record_IAO_Offset_Info(Type, transform.localPosition, transform.localRotation);
        }
    }

    [Flags]
    public enum SLOTID
    {
        Non,               
        RightHand,               
        LeftHand,          
        RightFeet,         
        LeftFeet,         
        Head,
        RightWrist,
        LeftWrist,
    }
}



