using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderPrefLoad : MonoBehaviour
{
    public Slider soundSlider;
    public Slider musicSlider;
    public Slider sensitivitySlider;
    public void setSoundSlider()
    {
        soundSlider.value = PlayerPrefs.GetFloat("sound");
    }

    public void setMusicSlider()
    {
        musicSlider.value = PlayerPrefs.GetFloat("music");
    }

    public void setSensitvitySlider()
    {
        sensitivitySlider.value = PlayerPrefs.GetFloat("SaveSense");
    }
}
