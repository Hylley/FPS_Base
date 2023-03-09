using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Enemy entity;
    public Slider slider;

    void Start()
    {
        slider.maxValue = entity.health;
    }

    void Update()
    {
        slider.value = entity.health;
    }
}
