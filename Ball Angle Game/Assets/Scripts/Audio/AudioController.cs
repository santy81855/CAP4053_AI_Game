using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] string _volumeParameter;
    [SerializeField] AudioMixer _mixer;
    [SerializeField] Slider _volumeSlider;
    [SerializeField] float _multiplier = 30f;

    private void Awake()
    {
        _volumeSlider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_volumeParameter, _volumeSlider.value);
    }

    private void HandleSliderValueChanged(float value)
    {
        _mixer.SetFloat(_volumeParameter, Mathf.Log10(value) * _multiplier);
    }
    // Start is called before the first frame update
    void Start()
    { 
        _volumeSlider.value = PlayerPrefs.GetFloat(_volumeParameter, _volumeSlider.value);
        HandleSliderValueChanged(_volumeSlider.value);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
