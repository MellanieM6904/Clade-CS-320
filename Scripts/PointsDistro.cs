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

    public void incHealth() {
        if (points > 0) {
            points--;
            creature.updateHP(creature.hp++);
        }
        pointsText.text = "Points: " + points.ToString();
    }

    public void decHealth() {
        if (points < 20) {
            points++;
            creature.updateHP(creature.hp--);
        }
        pointsText.text = "Points: " + points.ToString();
    }

    public void incAttack() {
        if (points > 0) {
            points--;
            creature.updateAttack(creature.attack++);
        }
        pointsText.text = "Points: " + points.ToString();
    }

    public void decAttack() {
        if (points < 20) {
            points++;
            creature.updateAttack(creature.attack--);
        }
        pointsText.text = "Points: " + points.ToString();
    }

    public void incDefense() {
        if (points > 0) {
            points--;
            creature.updateDefense(creature.defense++);
        }
        pointsText.text = "Points: " + points.ToString();
    }

    public void decDefense() {
        if (points < 20) {
            points++;
            creature.updateDefense(creature.defense--);
        }
        pointsText.text = "Points: " + points.ToString();
    }

}
