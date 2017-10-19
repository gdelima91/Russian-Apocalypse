﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    Image healthImage;

    private void Start()
    {
        healthImage = GetComponent<Image>();
        GameUIPr.Instance.Adapter_Healthbar_CA -= ValueChange;
        GameUIPr.Instance.Adapter_Healthbar_CA += ValueChange;
    }

    public void ValueChange(float current,float max)
    {
        if (healthImage != null)
        {
            healthImage.fillAmount = current / max;
        }
    }
}
