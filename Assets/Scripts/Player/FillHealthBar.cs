using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillHealthBar : MonoBehaviour
{
    public CharacterMovement player;
    // public Image fillImage;
    private Slider slider;
    void Awake()
    {
        slider = GetComponent<Slider>();
    }
    void Update()
    {
        slider.value = player.stamina / player.maxStamina;
    }
}
