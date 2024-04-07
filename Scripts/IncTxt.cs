using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IncTxt : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI armsTxt;
    [SerializeField] TextMeshProUGUI legsTxt;
    [SerializeField] TextMeshProUGUI eyesTxt;
    [SerializeField] TextMeshProUGUI newName;
    public Creature creature;

    public void Start() {
        armsTxt.text = creature.arms.ToString();
        legsTxt.text = creature.legs.ToString();
        eyesTxt.text = creature.eyes.ToString();
        creature.updateHP(20);
        creature.updateAttack(creature.arms*2);
        creature.updateDefense(creature.arms*2);
        creature.updateVision(creature.eyes*2);
        creature.updateStamina(creature.legs*2);
    }
    public void Update() {
        updateName();
    }
    public void incArms() {
        creature.updateArms(1);
        armsTxt.text = creature.arms.ToString();
        creature.updateAttack(creature.arms*2);
        creature.updateDefense(creature.arms*2);
    }
    public void decArms() {
        creature.updateArms(-1);
        armsTxt.text = creature.arms.ToString();
        creature.updateAttack(creature.arms*2);
        creature.updateDefense(creature.arms*2);
    }
    public void incLegs() {
        creature.updateLegs(1);
        legsTxt.text = creature.legs.ToString();
        creature.updateStamina(creature.legs*2);
    }
    public void decLegs() {
        creature.updateLegs(-1);
        legsTxt.text = creature.legs.ToString();
        creature.updateStamina(creature.legs*2);
    }
    public void incEyes() {
        creature.updateEyes(1);
        eyesTxt.text = creature.eyes.ToString();
        creature.updateVision(creature.eyes*2);
    }
    public void decEyes() {
        creature.updateEyes(-1);
        eyesTxt.text = creature.eyes.ToString();
        creature.updateVision(creature.eyes*2);
    }
    public void updateName() {
        creature.updateName(newName.ToString());
    }
}
