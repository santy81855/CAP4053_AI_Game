using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    [SerializeField] string _musicParameter = "music";
    [SerializeField] string _soundParameter = "sound";
    [SerializeField] AudioMixer _mixer;
    [SerializeField] Slider _musicSlider;
    [SerializeField] Slider _soundSlider;
    [SerializeField] float _multiplier = 30f;

    private void Awake()
    {
        _musicSlider.onValueChanged.AddListener(HandleSliderValueChanged);
        _soundSlider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_musicParameter, _musicSlider.value);
        PlayerPrefs.SetFloat(_soundParameter, _soundSlider.value);
    }

    private void HandleSliderValueChanged(float value)
    {
        _mixer.SetFloat(_musicParameter, Mathf.Log10(value) * _multiplier);
        _mixer.SetFloat(_soundParameter, Mathf.Log10(value) * _multiplier);
    }
    // Start is called before the first frame update
    void Start()
    {
        //_musicSlider.value = PlayerPrefs.GetFloat(_musicParameter, _musicSlider.value);
        //_soundSlider.value = PlayerPrefs.GetFloat(_soundParameter, _soundSlider.value);
        //_mixer.SetFloat(_musicParameter, _musicSlider.value);
        //_mixer.SetFloat(_soundParameter, _soundSlider.value);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
