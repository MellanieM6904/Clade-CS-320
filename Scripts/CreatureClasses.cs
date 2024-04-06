/*
Author: Mellanie Martin
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Creature : MonoBehaviour {
    public int arms = 1;
    public int legs = 1;
    public int heads = 1;
    public int eyes = 2;
    public string name = "Default";

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

    public void updateHeads(int h) {
        if (heads + h <= 3 && heads + h > 0) {
            this.heads += h;
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
}