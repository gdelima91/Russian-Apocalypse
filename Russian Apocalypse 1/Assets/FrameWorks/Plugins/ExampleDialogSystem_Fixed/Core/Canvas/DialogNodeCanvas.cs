using System.Collections.Generic;
using System.Linq;
using NodeEditorFramework;
using UnityEngine;

[NodeCanvasType("Dialog Canvas")]
public class DialogNodeCanvas : NodeCanvas
{
	public override string canvasName { get { return "Dialog"; } }
	public string Name = "Dialog";

	private Dictionary<int, BaseDialogNode> _lstActiveDialogs = new Dictionary<int, BaseDialogNode>();

	public bool HasDialogWithId(int dialogIdToLoad)
	{
		DialogStartNode node = getDialogStartNode(dialogIdToLoad);
		return node != default(Node) && node != default(DialogStartNode);
	}



	public IEnumerable<int> GetAllDialogId()
	{
		//return _lstDialogStartNodes.Select(startNode => startNode.DialogID).ToList();
		foreach (Node node in this.nodes)
		{
			if(node is DialogStartNode)
			{
				yield return ((DialogStartNode)node).DialogID;
			}
		}
	}

	/// <summary>
	/// Check if the Start Node in side the Active Dialogs
	/// 	if Not, we fetch from the nodes. and Add it to the Active Dialogs
	/// If it is insde the Active Dialogs, and we need to reload the Node
	/// we fetch from the nodes again, and upper cast Check
	/// if it is a Start Node, if it is, then we remap the key and value.
	/// 	So the function basically alwasy Add or remap the Start Node to the Active Dialogs.
	/// </summary>
	public void ActivateDialog(int dialogIdToLoad, bool goBackToBeginning)
	{
		BaseDialogNode node;
		if (!_lstActiveDialogs.TryGetValue(dialogIdToLoad, out node)) //Check if the Start Node Inside the Active Dialogs
		{
			//node = _lstDialogStartNodes.First(x => x.DialogID == dialogIdToLoad);
			node = getDialogStartNode (dialogIdToLoad);
			_lstActiveDialogs.Add(dialogIdToLoad, node);
		}
		else
		{
			if (goBackToBeginning && !(node is DialogStartNode))
			{
				_lstActiveDialogs [dialogIdToLoad] = getDialogStartNode(dialogIdToLoad);
			}
		}
	}

	public BaseDialogNode GetDialog(int dialogIdToLoad)
	{
		BaseDialogNode node;
		if (!_lstActiveDialogs.TryGetValue(dialogIdToLoad, out node))
		{
			Debug.Log("Reload or remap");
			ActivateDialog(dialogIdToLoad, false);
		}
		
		return _lstActiveDialogs[dialogIdToLoad];
	}

	public void InputToDialog(int dialogIdToLoad, int inputValue)
	{
		BaseDialogNode node;
		if (_lstActiveDialogs.TryGetValue(dialogIdToLoad, out node))
		{
			node = node.Input(inputValue); //Get the Next Node.
			if(node != null)
				node = node.PassAhead(inputValue);    //the inputValue Indicate for Next or Back. DialogStartNode--can only get Next,DialogNode can get Next or Back, MultipathSelectorNode will internal caculate the nextPath, MultiOptionNode will have Next,Back,and Option
			_lstActiveDialogs[dialogIdToLoad] = node;

			/*if(node!=null)
			{
				foreach(NodeOutput output in node.Outputs)
				{
					if(output.connections.Count > 0)
					{
						Debug.Log(output.connections[0].body.name);
					}
				}
			}*/	
		}
	}

	/// <summary>
	/// Gets the dialog start node. Which casted from the base class of Node
	/// </summary>
	/// <returns>The dialog start node.</returns>
	/// <param name="dialogID">Dialog I.</param>
	public DialogStartNode getDialogStartNode(int dialogID)
	{
		return (DialogStartNode)this.nodes.FirstOrDefault (x => x is DialogStartNode && ((DialogStartNode)x).DialogID == dialogID);
	}
}
