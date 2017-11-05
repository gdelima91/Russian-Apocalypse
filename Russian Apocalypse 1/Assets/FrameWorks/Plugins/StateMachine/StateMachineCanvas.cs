using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;

[NodeCanvasType("StateMachine Canvas")]
public class StateMachineCanvas : NodeCanvas {

    public override string canvasName{get{return "StateMachine";}}
    public string Name = "StateMachine";

    //private Dictionary<int, BaseStateNode> _lsStates = new Dictionary<int, BaseStateNode>();

}
