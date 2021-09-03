using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderManager : MonoBehaviour
{
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = GameManager.instance.volume;
    }

    public void OnValueChanged()
    {
        GameManager.instance.volume = slider.value;
        GameManager.instance.SaveVolume();
    }
}
