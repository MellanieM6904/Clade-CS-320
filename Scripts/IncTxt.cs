using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IncTxt : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtNum; // access text object
    int ct = 0;
    public void incVal() { // raise text value by 1
        if (ct < 3) { // value cant be greater than 3
            ct++;
            txtNum.text = ct.ToString();
        }
    }
    public void decVal() { // lower text value by 1
        if (ct > 1) { // value cant be less than 1
            ct--;
            txtNum.text = ct.ToString();
        }
    }
}
