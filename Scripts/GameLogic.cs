using Susing System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public GameObject playerModel;
    private int time;

    private void Start()
    {

    }

    private void Update()
    {
        time += 1 * Time.deltaTime;
    }
}
