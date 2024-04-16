/*
Author(s): Mellanie Martin, Koby Grah
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {
    // Written by Mellanie Martin
    public int arms = 1;
    public int legs = 1;
    public int eyes = 2;
    public string name = "Default";
    // Written by Koby Grah
    public int defense = 0;
    public int attack = 0;
    public int vision = 0; // MM
    public int hp = 0;
    public int stamina = 0;
    public int tempHp = 0; // track the player's current health
    public int tempStamina = 0;

    // Written by Mellanie Martin
    public void updateArms(int a) {
        if (arms + a <= 3 && arms + a > 0) {
            this.arms += a;
        }
    }

    public void updateLegs(int l) {
        if (legs + l <= 3 && legs + l > 0) {
            this.legs += l;
        }
    }

    public void updateEyes(int e) {
        if (eyes + e <= 8 && eyes + e > 0) {
            this.eyes += e;
        }
    }

    public void updateName(string newName) { // store user-given creature name
        if (newName.Length <= 26 && newName.Length > 0) {
            this.name = newName;
        }
    }
    
    // Written by Koby Grah
    public void updateDefense(int newDefense) {
        this.defense = newDefense;
    }
    public void updateAttack(int newAttack) {
        this.attack = newAttack;
    }
    public void updateVision(int newVision) {
        this.vision = newVision;
    }
    public void updateHP(int newHp) {
        this.hp = newHp;
        this.tempHp = newHp;
    }
    public void updateStamina(int newStamina) {
        this.stamina = newStamina;
        this.tempStamina = newStamina;
    }

    public void UpdateTempStats(int hp, int stamina)
    {
        this.tempHp += hp;
        this.tempStamina += stamina;
    }
}