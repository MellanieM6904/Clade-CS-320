using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IncTxt : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtNum;
    int ct = 0;
    public void incVal() {
        if (ct < 3) {
            ct++;
            txtNum.text = ct.ToString();
        }
    }
    public void decVal() {
        if (ct > 1) {
            ct--;
            txtNum.text = ct.ToString();
        }
    }
}
