using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillHealthBar : MonoBehaviour
{
     public Slider slider;
    public Gradient gradient;
    public Image fill;
    public CharacterMovement player;
    // public Image fillImage;

    void Awake()
    {

        slider = GetComponent<Slider>();
        fill.color = gradient.Evaluate(1f);
    }
    void Update()
    {
        slider.value = HealthManager.Instance.GetStaminaPercent();
        fill.color = gradient.Evaluate(slider.normalizedValue);
    
    }
}
