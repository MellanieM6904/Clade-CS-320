/*
Author: Mellanie Martin
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spawner : MonoBehaviour {
    public GameObject model;

    public void Start() {
        model.gameObject.SetActive(false);
    }

    public void hide() {
        model.gameObject.SetActive(false);
    }

    public void show() {
        model.gameObject.SetActive(true);
    }
}