using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterVolumeSlider : MonoBehaviour {

    Slider masterSlider;

    private void Start()
    {
        masterSlider = GetComponent<Slider>();
        if (masterSlider != null)
        {
            masterSlider.onValueChanged.AddListener(delegate { ValueChange(); });
        }
        masterSlider.value = AudioManager.instance.masterVolumePercent;
    }

    public void ValueChange()
    {
        AudioManager.instance.SetVolume(masterSlider.value, AudioManager.AudioChannel.Master);
    }

}
