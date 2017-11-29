using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLevelUI : MonoBehaviour {

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameUIPr.Instance.OpenLevelPanel();
        }
    }
}
