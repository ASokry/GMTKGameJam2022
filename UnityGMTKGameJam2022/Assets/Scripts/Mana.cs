using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana: MonoBehaviour
{
    [SerializeField] private int manaValue = 0;

    public int GetManaValue() { return manaValue; }

    public void GetManaValue(int mana)
    {
        manaValue = mana;
        if (manaValue <= 0) manaValue = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats player = collision.GetComponent<PlayerStats>();
            player.GainMana(manaValue);
            Destroy(gameObject);
        }
    }
}
