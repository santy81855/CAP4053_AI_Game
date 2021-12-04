using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SensitivityController : MonoBehaviour
{
    [SerializeField] string key;
    [SerializeField] CannonController cannon;
    [SerializeField] Slider sensitivitySlider;

    private void Awake()
    {
        sensitivitySlider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(key, sensitivitySlider.value);
    }

    private void HandleSliderValueChanged(float value)
    {
        cannon.SetSensitvity(value);
    }

    void Start()
    {
        sensitivitySlider.value = PlayerPrefs.GetFloat(key, sensitivitySlider.value);
        HandleSliderValueChanged(sensitivitySlider.value);
    }

}
