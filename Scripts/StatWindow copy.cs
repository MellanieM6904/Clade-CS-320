using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatWindow : MonoBehaviour
{
    public PlayerStats myCharacter;
    public Text healthStat;
    public Text attackStat;

    public void UpdateStatText()
    {
        healthStat.text = "Health: " + myCharacter.hp.ToString();
        attackStat.text = "Damage: " + myCharacter.attack.ToString();
    }
}

