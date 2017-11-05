using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Node(false,"StateMachine/New State",new System.Type[] { typeof(StateMachineCanvas)})]
public class PatrolStateNode : BaseStateNode {

    private Vector2 scroll;

    private const int StartValue = 276;
    protected const int SizeValue = 24;

    [UnityEngine.SerializeField]
    List<Transition> _allTransition;

    public override Node Create(Vector2 pos)
    {
        PatrolStateNode node = CreateInstance<PatrolStateNode>();

        node.rect.position = pos;
        node.name = "Base State Node";

        //Previous Node Connections
        node.CreateInput("Enter Node", "StateEnter", NodeSide.Left, 30);

        node.CreateOutput("Exit Node", "StateOut", NodeSide.Right, 30);

        node.stateName = "defualt State";

        node._allTransition = new List<Transition>();

        return node;
    }

    protected internal override void NodeGUI()
    {
#if UNITY_EDITOR
        EditorGUILayout.BeginVertical("Box", GUILayout.ExpandHeight(true));
        {
            EditorGUILayout.BeginVertical("Box");
            {
                GUILayout.BeginHorizontal();
                stateName = EditorGUILayout.TextField("", stateName);
                GUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();


        GUILayout.BeginVertical("Box");
        {
            GUILayout.ExpandWidth(false);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Options", NodeEditorGUI.nodeLabelBoldCentered);
            if (GUILayout.Button("+", GUILayout.Width(20)))
            {
                AddNewOption();
                IssueEditorCallBacks();
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            DrawOptions();

            GUILayout.ExpandWidth(false); 
        }
        GUILayout.EndVertical();
#endif
    }

    private void DrawOptions()
    {
#if UNITY_EDITOR
        EditorGUILayout.BeginVertical();

        for (int i = 0; i < _allTransition.Count; i++)
        {
            Transition t = _allTransition[i];
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(t.NodeOutputIndex + ".", GUILayout.MaxWidth(15));
            t.condition = EditorGUILayout.TextArea(t.condition, GUILayout.MaxWidth(80));
            OutputKnob(_allTransition[i].NodeOutputIndex);
            if (GUILayout.Button("-", GUILayout.Width(20)))
            {
                _allTransition.RemoveAt(i);
                Outputs[t.NodeOutputIndex].Delete();
                rect = new Rect(rect.x, rect.y, rect.width, rect.height - SizeValue);
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.Space(4);
        }

        EditorGUILayout.EndVertical();
#endif
    }

    protected void AddNewOption()
    {
        Transition t = new Transition();
        CreateOutput("Exit Node", "StateOut", NodeSide.Right, StartValue + _allTransition.Count * SizeValue);
        t.NodeOutputIndex = Outputs.Count - 1;
        rect = new Rect(rect.x, rect.y, rect.width, rect.height + SizeValue);
        _allTransition.Add(t);
    }

    protected void RemoveLastOption()
    {
        if (_allTransition.Count > 1)
        {
            Transition t = _allTransition.Last();
            _allTransition.Remove(t);
            Outputs[t.NodeOutputIndex].Delete();
            rect = new Rect(rect.x, rect.y, rect.width, rect.height - SizeValue);
        }
    }

    protected void IssueEditorCallBacks()
    {
        Transition t = _allTransition.Last();
        NodeEditorCallbacks.IssueOnAddNodeKnob(Outputs[t.NodeOutputIndex]);
    }

    [System.Serializable]
    public class Transition
    {
        public string condition = "null";
        public string additionalCall = "null";
        public int NodeOutputIndex;
    }


}
