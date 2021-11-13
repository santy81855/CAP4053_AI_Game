using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //public Text healthText;
    public Image healthBar;

    float health = 0, maxHealth = 100, lerpSpeed;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        HealthBarFiller();
        lerpSpeed = 3f * Time.deltaTime;
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);
        ColorChanger();
    }

    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));

    }

    public void Damage(float damagepoints)
    {
        if (health > 0)
        {
            health -= damagepoints;
        }
           
    }
}
