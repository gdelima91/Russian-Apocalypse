using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using NodeEditorFramework;
using System;


#if UNITY_EDITOR
using UnityEditor;
#endif

[Node(false,"StateMachine/Base State Node",new System.Type[] { typeof(StateMachineCanvas)})]
public abstract class BaseStateNode : Node {

    public override bool AllowRecursion
    {
        get
        {
            return true;
        }
    }

    public override Vector2 MinSize { get { return new Vector2(400, 60); } }
    public override bool Resizable { get { return true; } }

    private const string Id = "BaseStateNode";
    public override string GetID{ get{ return Id;}}

    [FormerlySerializedAs("StateName")]
    public string stateName;
}

public class StateEnterType : IConnectionTypeDeclaration
{
    public string Identifier { get { return "StateEnter"; } }
    public System.Type Type { get { return typeof(void); } }
    public Color Color { get { return Color.red; } }
    public string InKnobTex { get { return "Textures/In_Knob.png"; } }
    public string OutKnobTex { get { return "Textures/Out_Knob.png"; } }
}

public class StateOutType : IConnectionTypeDeclaration
{
    public string Identifier { get { return "StateOut"; } }
    public System.Type Type { get { return typeof(void); } }
    public Color Color { get { return Color.red; } }
    public string InKnobTex { get { return "Textures/In_Knob.png"; } }
    public string OutKnobTex { get { return "Textures/Out_Knob.png"; } }
}
