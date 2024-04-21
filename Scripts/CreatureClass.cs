/*
Author: Mellanie Martin
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Creature : MonoBehaviour {
    // Written by Mellanie Martin
    public int model = 0; // 1 - Predator, 2 - Prey, 3 - God 
    // Written by Koby Grah
    public int defense = 0;
    public int attack = 0;
<<<<<<< Updated upstream
    public int vision = 0; // MM
    //public int armor = 0;
=======
>>>>>>> Stashed changes
    public int hp = 0;
    public int stamina = 0;
    public int tempHp = 0; // track the player's current health
    public int tempStamina = 0;
<<<<<<< Updated upstream
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
=======
    
    // Written by Koby Grah, modififed by Mellanie Martin
>>>>>>> Stashed changes
    public void updateDefense(int newDefense) {
        defense = newDefense;
    }
    public void updateAttack(int newAttack) {
        attack = newAttack;
    }
<<<<<<< Updated upstream
    public void updateVision(int newVision) {
        vision = newVision;
    }
=======
>>>>>>> Stashed changes
    public void updateHP(int newHp) {
        hp = newHp;
        tempHp = newHp;
    }
    public void updateStamina(int newStamina) {
        stamina = newStamina;
        tempStamina = newStamina;
    }

    public void UpdateTempStats(int hp, int stamina)
    {
        tempHp += hp;
        tempStamina += stamina;
    }

    // MM
    public void updateModel(int newModel) {
        this.model = newModel;
    }
}