/*
Author: Mellanie Martin
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateStats : MonoBehaviour
{
    public Creature creature;
    public PointsDistro points;

    public void predatorStats() {
        creature.updateDefense(2);
        creature.updateAttack(7);
        creature.updateHP(20);
        creature.updateStamina(10);
        creature.updateModel(1);
        points.resetPoints();
    }

    public void wormStats() {
        creature.updateDefense(7);
        creature.updateAttack(2);
        creature.updateHP(10);
        creature.updateStamina(2);
        creature.updateModel(2);
        points.resetPoints();
    }

    public void bananaManStats() {
        creature.updateDefense(5);
        creature.updateAttack(5);
        creature.updateHP(15);
        creature.updateStamina(5);
        creature.updateModel(3);
        points.resetPoints();
    }

    public void godStats() {
        creature.updateDefense(15);
        creature.updateAttack(15);
        creature.updateHP(15);
        creature.updateStamina(15);
        creature.updateModel(4);
        points.resetPoints();
    }
}
