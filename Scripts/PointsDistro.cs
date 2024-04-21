/*
Author: Mellanie Martin
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsDistro : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointsText;
    public Creature creature;
    public int points = 20;

    public void Start() {
        pointsText.text = "Points: " + points.ToString();
    }

    public void incHealth() {
        int health = creature.hp;
        if (points > 0) {
            points--;
            creature.updateHP(health + 1);
        }
        pointsText.text = "Points: " + points.ToString();
    }

    public void decHealth() {
        int health = creature.hp;
        if (points < 20 && health - 1 >= 0) {
            points++;
            creature.updateHP(health - 1);
        }
        pointsText.text = "Points: " + points.ToString();
    }

    public void incStamina() {
        int stamina = creature.stamina;
        if (points > 0) {
            points--;
            creature.updateStamina(stamina + 1);
        }
        pointsText.text = "Points: " + points.ToString();
    }

    public void decStamina() {
        int stamina = creature.stamina;
        if (points < 20 && stamina - 1 >= 0) {
            points++;
            creature.updateStamina(stamina - 1);
        }
        pointsText.text = "Points: " + points.ToString();
    }

    public void incAttack() {
        int attack = creature.attack;
        if (points > 0) {
            points--;
            creature.updateAttack(attack + 1);
        }
        pointsText.text = "Points: " + points.ToString();
    }

    public void decAttack() {
        int attack = creature.attack;
        if (points < 20 && attack - 1 >= 0) {
            points++;
            creature.updateAttack(attack - 1);
        }
        pointsText.text = "Points: " + points.ToString();
    }

    public void incDefense() {
        int defense = creature.defense;
        if (points > 0) {
            points--;
            creature.updateDefense(defense + 1);
        }
        pointsText.text = "Points: " + points.ToString();
    }

    public void decDefense() {
        int defense = creature.defense;
        if (points < 20 && defense - 1 >= 0) {
            points++;
            creature.updateDefense(defense - 1);
        }
        pointsText.text = "Points: " + points.ToString();
    }

    public void resetPoints() {
        this.points = 20;
        pointsText.text = "Points: " + points.ToString();
    }

}
