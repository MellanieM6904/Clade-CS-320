using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public Text timeText;
    public Text dayText;
    public GameObject pauseScreen;

    private float worldTime = 0f;
    private int worldDay = 1;
    public bool gamePaused;

    private void Start()
    {
        gamePaused = false;
    }

    private void Update()
    {
        if (!gamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                gamePaused = true;
                pauseScreen.SetActive(true);
            }

            // Check for finished day
            if (Mathf.RoundToInt(worldTime) / 1440 > 0)
            {
                worldDay++;
                worldTime = 0;
            }

            // Check for out of days
            if (worldDay > 5)
            {
                // END GAME
                Debug.Log("GAME OVER.");
                worldDay = 0;
            }

            worldTime += Time.deltaTime;

            ShowTime(worldTime);
            ShowDay(worldDay);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseScreen.SetActive(false);
                gamePaused = false;
                Time.timeScale = 1;
            }
        }
    }

    public void ShowTime(float worldTime)
    {
        float hour = worldTime / 60;
        float minute = worldTime % 60;

        if (hour < 10 && minute < 10) timeText.text = "Time: 0" + Mathf.Floor(hour) + ":0" + Mathf.Floor(minute);
        else if (hour < 10 && minute >= 10) timeText.text = "Time: 0" + Mathf.Floor(hour) + ":" + Mathf.Floor(minute);
        else if (hour >= 10 && minute < 10) timeText.text = "Time: " + Mathf.Floor(hour) + ":0" + Mathf.Floor(minute);
        else timeText.text = "Time: " + Mathf.Floor(hour) + ":" + Mathf.Floor(minute);
    }

    public void ShowDay(int worldDay)
    {
        dayText.text = "Day: " + worldDay;
    }

}

