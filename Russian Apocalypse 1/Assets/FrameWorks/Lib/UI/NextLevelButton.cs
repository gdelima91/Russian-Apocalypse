using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour {

    Button nextLevelButton;
    // Use this for initialization
    void Start()
    {
        nextLevelButton = GetComponent<Button>();
        if (nextLevelButton != null)
        {
            nextLevelButton.onClick.AddListener(delegate { OnClick(); });
        }
    }

    public void OnClick()
    {
       GameUIPr.Instance.NextLevel();
    }
}
