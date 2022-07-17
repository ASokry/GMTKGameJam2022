using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private Bar healthBar;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth = 0;

    private Bar manaBar;
    [SerializeField] private int maxMana = 100;
    [SerializeField] private int currentMana = 0;

    [SerializeField] private float dropRadius = 3.5f;
    [SerializeField] private bool iFrames = false;
    [SerializeField] private float iFramesTime = 2f; 

    private ManaManager manaManager;

    public int GetPlayerMana() { return currentMana; }
    public bool IsManaAtMax() { return currentMana == maxMana; }

    private void Awake()
    {
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Bar>();
        manaBar = GameObject.FindGameObjectWithTag("ManaBar").GetComponent<Bar>();
        if (!healthBar) Debug.LogError("Health Bar is Null!");
        if (!manaBar) Debug.LogError("Mana Bar is Null!");

        Portal.OnPortal += OnNewScene;
    }

    private void OnDestroy()
    {
        Portal.OnPortal -= OnNewScene;
    }

    private void OnNewScene()
    {
        ReCenter();
    }

    private void ReCenter()
    {
        transform.position = new Vector2(0, 0);
    }

    private void Start()
    {
        if(healthBar)
        {
            healthBar.SetMaxValue(maxHealth);
            healthBar.SetValue(maxHealth);
        }
        currentHealth = maxHealth;

        if (manaBar) manaBar.SetMaxValue(maxMana);
        currentMana = 0;

        manaManager = GameObject.FindGameObjectWithTag("ManaManager").GetComponent<ManaManager>();
        if (manaManager == null) { Debug.LogError("ManaManager is not in Persistent Scene!"); }
    }

    public bool GetIFrame()
    {
        return iFrames;
    }

    public void GainHp(int h)
    {
        if (!iFrames)
        {
            currentHealth += h;
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            if (healthBar) healthBar.SetValue(currentHealth);
        }
    }

    public void LoseHp(int h)
    {
        if (!iFrames)
        {
            currentHealth -= h;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            if (healthBar) { healthBar.SetValue(currentHealth); }
            if (currentHealth <= 0)
            {
                ZoneManager.GoToLoseScene();
                Destroy(this.gameObject);
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        if (!iFrames)
        {
            LoseHp(dmg);
            manaManager.SpawnManaInRadius(transform.position, dropRadius, CalculateManaToRegain());
            LoseMana(CalculateManaToLose());
            iFrames = true;
            Animator animator = GetComponentInChildren<Animator>();
            animator.SetTrigger("hit");
        }
    }

    public void ResetIFrames()
    {
        iFrames = false;
    }

    public void GainMana(int mana)
    {
        if (!iFrames)
        {
            currentMana += mana;
            if (currentMana > maxMana) currentMana = maxMana;
            if (manaBar) manaBar.SetValue(currentMana);
        }
    }

    public void LoseMana(int mana)
    {
        if (!iFrames)
        {
            currentMana -= mana;
            if (currentMana < 0) currentMana = 0;
            if (manaBar) manaBar.SetValue(currentMana);
        }
    }

    private int CalculateManaToLose()
    {
        int manaToLose = 0;
        List<Vector3Int> chart = manaManager.GetManaProgressChart();
        foreach (Vector3Int loss in chart)
        {
            if (manaManager.GetTotalManaValueToSpawn() == loss.x)
            {
                manaToLose = loss.y;
                //print(manaToLose);
            }
        }

        return manaToLose;
    }

    private int CalculateManaToRegain()
    {
        int manaToRegain = 0;
        List<Vector3Int> chart = manaManager.GetManaProgressChart();
        foreach (Vector3Int regain in chart)
        {
            if (manaManager.GetTotalManaValueToSpawn() == regain.x)
            {
                manaToRegain = regain.z;
            }
        }

        if (currentMana < manaToRegain)
        {
            manaToRegain = currentMana;
        }
        print(manaToRegain);

        return manaToRegain;
    }
}
