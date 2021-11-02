using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AudioManager : MonoBehaviour
{
    // Class is a singleton (there is only one instance and can be reference globally).
    #region Singleton
    private static AudioManager instance;

    public static AudioManager Instance { get { return instance; } }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    #endregion

    // Sound variables
    public AudioSource BackgroundAudio;
    public AudioSource WinAudio;
    public AudioSource LoseAudio;
    public AudioSource FireAudio;
}
