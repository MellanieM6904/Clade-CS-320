/*
Author: Mellanie Martin
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spawner : MonoBehaviour {
    public GameObject body;

    public void spawnBody() {
        Instantiate(body);
    }
}