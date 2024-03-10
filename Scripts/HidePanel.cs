using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HidePanel : MonoBehaviour
{
    public GameObject panel;
    public void hide() {
        panel.gameObject.SetActive(false);
    }

    public void show() {
        panel.gameObject.SetActive(true);
    }
}
