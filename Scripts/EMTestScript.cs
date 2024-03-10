using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EMTestScript
{
    /* WHITE BOX TESTS
    METHOD BEING TESTED FOR greaterThanBoundsTest & lesserThanBoundsTest:
    public void updateParts(int a, int l, int h) {
        if (arms + a <= 3) {
            this.arms += a;
        }
        if (legs + l <= 3) {
            this.legs += l;
        }
        if (heads + h <= 3) {
            this.heads += h;
        }
    }
    */
    [Test] // provides coverage for a/l/h values that result in an arms/legs/heads value over 3. Full coverage is achieved between greaterThanBoundsTest & lessThanBoundsTest
    public void greaterThanBoundsTest()
    {
        Creature creature = new GameObject().AddComponent<Creature>();
        creature.updateParts(4, 5, 9);
        Assert.AreEqual(creature.arms, 1);
        Assert.AreEqual(creature.legs, 1);
        Assert.AreEqual(creature.heads, 1);
    }

    [Test] // provides coverage for a/l/h values that result in an arms/legs/heads value less than 1. Full coverage is achieved between greaterThanBoundsTest & lessThanBoundsTest
    public void lessThanBoundsTest() {
        Creature creature = new GameObject().AddComponent<Creature>();
        creature.updateParts(-4, -5, -9);
        Assert.AreEqual(creature.arms, 1);
        Assert.AreEqual(creature.legs, 1);
        Assert.AreEqual(creature.heads, 1);
    }
    /*
    METHOD BEING TESTED FOR minStrlenTest & maxStrlenTest:
    public void updateName(string newName) {
        if (newName.Length <= 26 && newName.Length > 0) {
            this.name = newName;
        }
    }
    */
    [Test] // provides coverage for names less than 1 character long. Full coverage is achieved between minStrLenTest & maxStrLenTest
    public void minStrlenTest() {
        Creature creature = new GameObject().AddComponent<Creature>();
        creature.updateName("");
        Assert.AreEqual(creature.name, "Default");
    }
    [Test] // provides coverage for names more than 26 characters long
    public void maxStrlenTest() {
        Creature creature = new GameObject().AddComponent<Creature>();
        creature.updateName("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz");
        Assert.AreEqual(creature.name, "Default");
    }
    /*
    METHOD BEING TESTED FOR minCtTest & maxCtTest
    public void incVal() {
        if (ct < 3) {
            ct++;
            txtNum.text = ct.ToString();
        }
    }
    */
    [Test] // Full coverage
    public void maxCtTest() {
        IncTxt txt = new GameObject().AddComponent<IncTxt>();
        txt.incVal(); txt.incVal(); txt.incVal(); txt.incVal(); // increment 4 times
        Assert.AreEqual(txt.ct, 3);
    }
    [Test] // Full coverage
    public void minCtTest() {
        IncTxt txt = new GameObject().AddComponent<IncTxt>();
        txt.decVal(); txt.decVal(); // decrement twice
        Assert.AreEqual(txt.ct, 0);
    }

    

    // BLACK BOX TESTS
    // Acceptance Test
    [Test]
    public void updatePartsTest() {
        Creature creature = new GameObject().AddComponent<Creature>();
        creature.updateParts(1, 2, 0);
        Assert.AreEqual(creature.arms, 2);
        Assert.AreEqual(creature.legs, 3);
        Assert.AreEqual(creature.heads, 1);
    }
    // Acceptance Test
    [Test]
    public void updateNameTest() {
        Creature creature = new GameObject().AddComponent<Creature>();
        creature.updateName("Magneto");
        Assert.AreEqual(creature.name, "Magneto");
    }
    // Acceptance Test
    [Test]
    public void incValTest() {
        IncTxt txt = new GameObject().AddComponent<IncTxt>();
        txt.incVal(); txt.incVal();
        Assert.AreEqual(txt.ct, 2);
    }
    // Acceptance Test
    [Test]
    public void decValTest() {
        IncTxt txt = new GameObject().AddComponent<IncTxt>();
        txt.incVal(); txt.decVal();
        Assert.AreEqual(txt.ct, 0);
    }
    
}

