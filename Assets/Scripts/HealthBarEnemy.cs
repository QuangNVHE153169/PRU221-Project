using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    public Image fillImage;
    private Slider slider;
    public Enemies enemies;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void Update()
    {
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }
        if (slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }
        float fillValue = (float)enemies.currentHealth / enemies.maxHealth;

        if (fillValue <= slider.maxValue / 3)
        {
            fillImage.color = Color.red;
        }
        else if (fillValue > slider.maxValue / 3)
        {
            fillImage.color = Color.green;
        }
        slider.value = fillValue;
    }
}
