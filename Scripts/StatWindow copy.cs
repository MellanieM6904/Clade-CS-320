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
    [SerializeField] TextMeshProUGUI staminaText;
    [SerializeField] TextMeshProUGUI classText;

    public void Start() {
        GetStatStrings();
        ClassAnalyzer();
        classText.text = "";
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
        staminaText.text = "Stamina: " + myCharacter.stamina.ToString();
        return;
    }

    public void ClassAnalyzer() {
<<<<<<< Updated upstream
        int health = myCharacter.hp;
        int attack = myCharacter.attack;
        int defense = myCharacter.defense;
        int vision = myCharacter.vision;
        //int armor = myCharacter.armor;
        int stamina = myCharacter.stamina;
=======
        int model = myCharacter.model;
>>>>>>> Stashed changes

        if (model == 1) { // MM
            classText.text = "Predator";
        } else if (model == 2) { // MM
            classText.text = "Prey";
<<<<<<< Updated upstream
        } else {
            classText.text = "Destined for Extinction";
=======
        } else if (model == 3) {
            classText.text = "Destined for Mediocrity";
        } else if (model == 4) {
            classText.text = "Destined for Greatness";
>>>>>>> Stashed changes
        }

        return;
    }
}

