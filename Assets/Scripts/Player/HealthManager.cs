using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }
    public float stamina { get; private set; }

    public event Action OnDeath;
    public float maxStamina = 100f;
    public float staminaDrain = 3.5f;
    public float staminaRegen = 5f;
    public float criticalStaminaLevel = 35f;
    private bool warnedLowStamina = false;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina;
    }

    // -----------------------------
    // Stamina
    // -----------------------------
    private void RegenerateStamina()
    {
        if (stamina < maxStamina)
        {
            stamina += staminaRegen * Time.deltaTime;
            stamina = Mathf.Min(stamina, maxStamina);
        }

        if (stamina <= criticalStaminaLevel && !warnedLowStamina)
        {
            Debug.Log("Stamina is low — rest or eat!");
            warnedLowStamina = true;
        }
        else if (stamina > criticalStaminaLevel)
        {
            warnedLowStamina = false;
        }
    }

    public bool AddStamina(float amount)
    {
        if (stamina >= amount)
        {
            stamina += amount;
            stamina = (stamina > maxStamina) ? maxStamina : stamina;
            return true;
        }
        return false;
    }

    public bool DecreaseStamina(float amount)
    {
        if (stamina >= amount && stamina > 0)
        {
            stamina -= amount;

            WarnLowStamins();

            if (stamina <= 0)
            {
                stamina = 0;
                Die();
            }

            return true;
        }
        return false;
    }

    public void MultiplyStamina(float amount)
    {
        if (stamina >= amount)
        {
            stamina *= amount;
            if (stamina <= 0)
            {
                stamina = 0;
                Die();
            }
        }
    }

    private void Die()
    {

        OnDeath?.Invoke();
    }

    public float GetStaminaPercent()
    {
        return stamina / maxStamina;
    }

    private void WarnLowStamins()
    {
        if (stamina <= criticalStaminaLevel && !warnedLowStamina)
        {
            Debug.Log("Stamina is low — rest or eat!");
            warnedLowStamina = true;
        }
        else if (stamina > criticalStaminaLevel)
        {
            warnedLowStamina = false;
        }
    }
}
