using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UpdateAudio : MonoBehaviour
{
    [SerializeField] AudioMixer _mixer;

    // Start is called before the first frame update
    void Start()
    {
        float soundVolume = PlayerPrefs.GetFloat("sound");
        _mixer.SetFloat("sound", Mathf.Log10(soundVolume) * 30f);
        float musicVolume = PlayerPrefs.GetFloat("music");
        _mixer.SetFloat("music", Mathf.Log10(musicVolume) * 30f);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
