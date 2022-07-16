using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Bar healthBar;
    [SerializeField] private int maxHealth = 100;
    private int currentHealth = 0;

    [SerializeField] private Bar manaBar;
    [SerializeField] private int maxMana = 100;
    private int currentMana = 0;

    public int GetPlayerMana() { return currentMana; }
    public bool IsManaAtMax() { return currentMana == maxMana; }

    private void Awake()
    {
        if (!healthBar) Debug.LogError("Health Bar is Null!");
        if (!manaBar) Debug.LogError("Mana Bar is Null!");
    }

    private void Start()
    {
        healthBar.SetMaxValue(maxHealth);
        healthBar.SetValue(maxHealth);
        currentHealth = maxHealth;

        manaBar.SetMaxValue(maxMana);
        currentMana = 0;
    }

    public void Heal(int h)
    {
        currentHealth += h;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        healthBar.SetValue(currentHealth);
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth < 0) currentHealth = 0;
        healthBar.SetValue(currentHealth);
    }

    public void GainMana(int mana)
    {
        currentMana += mana;
        if (currentMana > maxMana) currentMana = maxMana;
        manaBar.SetValue(currentMana);
    }

    public void LoseMana(int mana)
    {
        currentMana -= mana;
        if (currentMana < 0) currentMana = maxMana;
        manaBar.SetValue(currentMana);
    }
}
