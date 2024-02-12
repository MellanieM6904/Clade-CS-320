using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public:
        int defense = 0;
        int attack = 0;
        int fireRes = 0;
        int armor = 0;
        int hp = 0;
        int stamina = 0;

        static void updateStats(int, defense, int attack, int fireRes, int armor, int hp, int stamina) {
            this.defense += defense;
            this.attack += attack;
            this.fireRes += fireRes;
            this.armor += armor;
            this.hp += hp;
            this.stamina += stamina;
        }
}
