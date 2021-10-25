using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AudioManager : MonoBehaviour
{
    #region Singleton

    public static AudioManager instance;

    private void Awake()
    {
            instance = this;
    }

    #endregion

    public AudioSource BackgroundAudio;
    public AudioSource WinAudio;
    public AudioSource LoseAudio;
    public AudioSource FireAudio;
}
