using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
public class TestScript
{
    // Acceptance Test
    [Test]
    public void UpdatePlayerStatsTest()
    {
        PlayerStats myPlayer = new GameObject().AddComponent<PlayerStats>();
        myPlayer.UpdatePermStats(1, 2, 3, 4, 5, 6);
        Assert.AreEqual(myPlayer.hp, 5);
    }

    // Acceptance Test
    [Test]
    public void UpdateComponentStatsTest() {
        ComponentStats myComp = new GameObject().AddComponent<ComponentStats>();
        myComp.UpdateComponentStats(1, 2, 3, 4, 5, 6);
        Assert.AreEqual(myComp.hp, 5);
    }

    // Acceptance Test
    [Test]
    public void UpdateTempStatsTest() {
        PlayerStats myPlayer = new GameObject().AddComponent<PlayerStats>();
        myPlayer.UpdateTempStats(1, 2);
        Assert.AreEqual(myPlayer.tempHp, 1);
    }

    //Integration test
    //Big Bang
    //Testing PlayerStats with ComponentStats
    [Test]
    public void IntegrationTestForCharAndComp() {
        PlayerStats myPlayer = new GameObject().AddComponent<PlayerStats>();
        ComponentStats myComp = new GameObject().AddComponent<ComponentStats>();
        myComp.UpdateComponentStats(1, 2, 3, 4, 5, 6);
        myPlayer.UpdatePermStats(myComp.defense, myComp.attack, myComp.fireRes, myComp.armor, myComp.hp, myComp.stamina);
        Assert.AreEqual(myPlayer.hp, 5);
    }

    //White box test, full coverage
    /*
    public void UpdatePermStats(int newDefense, int newAttack, int newFireRes, int newArmor, int newHp, int newStamina)
    {
        defense += newDefense;
        attack += newAttack;
        fireRes += newFireRes;
        armor += newArmor;
        hp += newHp;
        stamina += newStamina;
        tempHp += newHp;
        tempStamina += newStamina;

        if (hp < 1) hp = 1;
        if (stamina < 1) stamina = 1;
    }
    */
    [Test]
    public void negativePlayerPermStatsTest() {
        PlayerStats myPlayer = new GameObject().AddComponent<PlayerStats>();
        myPlayer.UpdatePermStats(-1, -2, -3, -4, -5, -6);
        Assert.AreEqual(myPlayer.hp, 1);
    }

    // Acceptance Test
    [Test]
    public void CorrectInitialPlayerValuesTest() {
        PlayerStats myPlayer = new GameObject().AddComponent<PlayerStats>();
        Assert.AreEqual(myPlayer.hp, 0);
    }

    // Acceptance Test
    [Test]
    public void correctInitialComponentStats() {
        ComponentStats myComp = new GameObject().AddComponent<ComponentStats>();
        Assert.AreEqual(myComp.hp, 0);
    }

    //White box test, full coverage
    /*
    public void UpdateComponentStats(int newDefense, int newAttack, int newFireRes, int newArmor, int newHp, int newStamina) {
        defense += newDefense;
        attack += newAttack;
        fireRes += newFireRes;
        armor += newArmor;
        hp += newHp;
        stamina += newStamina;

        if (hp < 1) hp = 1;
        if (stamina < 1) stamina = 1;
        return;
    }
    */
    [Test]
    public void negativeComponentStatsTest() {
        ComponentStats myComp = new GameObject().AddComponent<ComponentStats>();
        myComp.UpdateComponentStats(-1, -2, -3, -4, -5, -6);
        Assert.AreEqual(myComp.hp, 1);
    }

    // Acceptance Test
    [Test]
    public void ClassAnalyzerTestOnStart() {
        StatWindow myWindow = new GameObject().AddComponent<StatWindow>();
        Assert.AreEqual(myWindow.classText, null);
    }

    //Integration Test
    //Big Bang
    //Testing StatWindow with PlayerStats
    [Test]
    public void StatWindowIntegrationTest() {
        StatWindow myWindow = new GameObject().AddComponent<StatWindow>();
        myWindow.myCharacter.UpdatePermStats(1, 2, 3, 4, 5, 6);
        Assert.AreEqual(myWindow.myCharacter.hp, 5);
    }

    //Acceptance Test
    [Test]
    public void StatWindowTextTestOnStart() {
        StatWindow myWindow = new GameObject().AddComponent<StatWindow>();
        Assert.AreEqual(myWindow.healthText, "Health: 0");
    }

    //Integration Test
    //Big Bang
    //Testing StatWindow with PlayerStats
    [Test]
    public void StatWindowTextIntegrationTestOnUpdate() {
        StatWindow myWindow = new GameObject().AddComponent<StatWindow>();
        myWindow.myCharacter.UpdatePermStats(1, 2, 3, 4, 5, 6);
        Assert.AreEqual(myWindow.healthText, "Health: 5");
    }
}

