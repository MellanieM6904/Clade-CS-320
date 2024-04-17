using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickWeather : MonoBehaviour
{
    public enum curWeather {Sunny, Overcast, Rainy, Windy, Stormy}
    public curWeather Current_Weather;

    public float elapsedTime = 0f;  // Timer starts at zero seconds
    public float diceTimer = 300f;  // Weather should be considered every five minutes

    // Start is called before the first frame update
    void Start()
    {
        Current_Weather = ChooseWeatherPattern();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= diceTimer)
        {
            Current_Weather = ChooseWeatherPattern();
            elapsedTime = 0f;
        }
    }


    curWeather ChooseWeatherPattern()
    {
        return (curWeather)Random.Range(0, System.Enum.GetValues(typeof(curWeather)).Length);
    }
}
