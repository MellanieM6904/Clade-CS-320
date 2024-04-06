using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private float worldTime = 0f;
    private int worldDay = 0;

    private void Start()
    {

    }

    private void Update()
    {
        worldTime += Time.deltaTime;
    }
}
