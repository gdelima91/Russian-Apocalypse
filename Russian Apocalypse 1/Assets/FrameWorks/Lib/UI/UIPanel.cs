using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIPanel : MonoBehaviour {
   public abstract GameUIPr.PanelType GetPanelType();
}
