using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatWindow : MonoBehaviour
{
    public PlayerStats myCharacter;
    public Text healthText;
    public Text attackText; 
    public Text defenseText;
    public Text fireResText;
    public Text armorText;
    public Text staminaText;
    public Text classText;

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
        fireResText.text = "Fire Res: " + myCharacter.fireRes.ToString();
        armorText.text = "Armor: " + myCharacter.armor.ToString();
        staminaText.text = "Stamina: " + myCharacter.stamina.ToString();
        return;
    }

    public void ClassAnalyzer() {
        int health = myCharacter.hp;
        int attack = myCharacter.attack;
        int defense = myCharacter.defense;
        int fireRes = myCharacter.fireRes;
        int armor = myCharacter.armor;
        int stamina = myCharacter.stamina;

        if (attack > 5) {
            classText.text = "Predator";
        } else if (armor > 5) {
            classText.text = "Prey";
        } else {
            classText.text = "Destined for Extinction";
        }

        return;
    }
}

