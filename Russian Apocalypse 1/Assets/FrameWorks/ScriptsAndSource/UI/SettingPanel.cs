using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : UIPanel {
    public override GameUIPr.PanelType GetPanelType()
    {
        return GameUIPr.PanelType.SaveGame;
    }
}
