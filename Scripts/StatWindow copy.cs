/*
Author: Mellanie Martin
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatWindow : MonoBehaviour
{
    public Creature myCharacter;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI attackText; 
    [SerializeField] TextMeshProUGUI defenseText;
    [SerializeField] TextMeshProUGUI visionText;
    [SerializeField] TextMeshProUGUI staminaText;
    [SerializeField] TextMeshProUGUI classText;

    public void Start() {
        GetStatStrings();
        ClassAnalyzer();
        return;
    }

    void Update() { //refresh dynamic text box
        GetStatStrings();
        ClassAnalyzer();
        return;
    }

    public void GetStatStrings() {
        healthText.text = "Health: " + myCharacter.hp.ToString();
        attackText.text = "Damage: " + myCharacter.attack.ToString();
        defenseText.text = "Defense: " + myCharacter.defense.ToString();
        visionText.text = "Vision: " + myCharacter.vision.ToString();
        staminaText.text = "Stamina: " + myCharacter.stamina.ToString();
        return;
    }

    public void ClassAnalyzer() {
        int health = myCharacter.hp;
        int attack = myCharacter.attack;
        int defense = myCharacter.defense;
        int vision = myCharacter.vision;
        int stamina = myCharacter.stamina;

        if (attack > 5 && vision > 4) { // MM
            classText.text = "Predator";
        } else if (defense > 5 && vision > 4) { // MM
            classText.text = "Prey";
        } else if (defense > 10 && attack > 10 && vision > 8 && stamina > 10) {
            classText.text = "Destined for Greatness";
        } else {
            classText.text = "Destined for Extinction";
        }

        return;
    }
}

