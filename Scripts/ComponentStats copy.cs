using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentStats : MonoBehaviour {
    public int defense;
    public int attack;
    public int fireRes;
    public int armor;
    public int hp;
    public int stamina;
    PlayerStats player;

    public void UpdateComponentStats(int newDefense, int newAttack, int newFireRes, int newArmor, int newHp, int newStamina) {
        defense += newDefense;
        attack += newAttack;
        fireRes += newFireRes;
        armor += newArmor;
        hp += newHp;
        stamina += newStamina;
        return;
    }

    public void RemoveComponent() {
        player.UpdatePermStats(-defense, -attack, -fireRes, -armor, -hp, -stamina);
        return;
    }

    public void Start() {
        player.UpdatePermStats(defense, attack, fireRes, armor, hp, stamina);
        return;
    }

}
