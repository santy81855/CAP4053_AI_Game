using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReloadLoader : MonoBehaviour
{
    public GameObject reloadBar;
    public Slider slider;
    float progress;
    public void LoadReload (float nextTimeToFire, float fireRate)
    {
        StartCoroutine(FillBar(nextTimeToFire, fireRate));    
    }

    IEnumerator FillBar (float nextTimeToFire, float fireRate)
    {
        progress = 0f;
        while (progress <= .99f)
        {
            progress = 1f - ((nextTimeToFire - Time.time) / fireRate);
            Debug.Log("PROGRESS: " + progress);
            slider.value = progress + .01f;

            yield return null;
        }
        yield break;
    }
}
