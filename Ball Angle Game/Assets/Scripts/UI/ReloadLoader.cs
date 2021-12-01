using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReloadLoader : MonoBehaviour
{
    public GameObject reloadBar;
    public Slider slider;
    float progress;

    // Start the coroutine to fill up the reload bar
    public void LoadReload(float nextTimeToFire, float fireRate)
    {
        StartCoroutine(FillBar(nextTimeToFire, fireRate));
    }

    IEnumerator FillBar(float nextTimeToFire, float fireRate)
    {
        // While the progress of the bar is below 100 percent, keep filling up the bar according to
        // the percentage between the last time fired, the next time it can be fired, and how much time
        // has elapsed in the meantime.
        progress = 0f;
        while (progress <= 1.00f)
        {
            progress = 1f - ((nextTimeToFire - Time.time) / fireRate);
            slider.value = progress + .01f;

            yield return null;
        }
        yield break;
    }
}
