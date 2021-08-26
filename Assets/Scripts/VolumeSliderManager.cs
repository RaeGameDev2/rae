using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class VolumeSliderManager : MonoBehaviour
{
    private Slider slider;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectsOfType<GameManager>().FirstOrDefault(manager => manager.isDontDestroyOnLoad);
        slider = GetComponent<Slider>();

        slider.value = gameManager.volume;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnValueChanged()
    {
        gameManager.volume = slider.value;
        gameManager.SaveVolume();
    }
}
