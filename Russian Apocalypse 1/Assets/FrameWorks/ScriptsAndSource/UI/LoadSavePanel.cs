using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSavePanel : UIPanel {
    public override GameUIPr.PanelType GetPanelType()
    {
        return GameUIPr.PanelType.LoadSave;
    }
}
