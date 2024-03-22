using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HidePanel : MonoBehaviour
{
    public GameObject panel; // access panel object
    public void hide() {
        panel.gameObject.SetActive(false); // flip panel's boolean
    }

    public void show() {
        panel.gameObject.SetActive(true); // flip panel's boolean
    }
}
