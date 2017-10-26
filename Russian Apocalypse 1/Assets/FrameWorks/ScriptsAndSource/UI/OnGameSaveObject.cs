using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameSaveObject : MonoBehaviour {

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameUIPr.Instance.OpenSavePanel();
        }
    }

}
