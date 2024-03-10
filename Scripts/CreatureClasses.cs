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
    public string name = "Default";

    public void updateParts(int a, int l, int h) {
        if (arms + a <= 3 && arms + a > 0) {
            this.arms += a;
        }
        if (legs + l <= 3 && legs + l > 0) {
            this.legs += l;
        }
        if (heads + h <= 3 && heads + h > 0) {
            this.heads += h;
        }
    }
    public void updateName(string newName) {
        if (newName.Length <= 26 && newName.Length > 0) {
            this.name = newName;
        }
    }
}