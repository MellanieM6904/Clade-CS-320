using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int defense = 0;
    public int attack = 0;
    public int fireRes = 0;
    public int armor = 0;
    public int hp = 0;
    public int stamina = 0;
    public int tempHp = 0; // track the player's current health
    public int tempStamina = 0;

    public void UpdatePermStats(int newDefense, int newAttack, int newFireRes, int newArmor, int newHp, int newStamina)
    {
        defense += newDefense;
        attack += newAttack;
        fireRes += newFireRes;
        armor += newArmor;
        hp += newHp;
        stamina += newStamina;
        tempHp += newHp;
        tempStamina += newStamina;
    }

    public void UpdateTempStats(int hp, int stamina)
    {
        tempHp += hp;
        tempStamina += stamina;
    }
}
