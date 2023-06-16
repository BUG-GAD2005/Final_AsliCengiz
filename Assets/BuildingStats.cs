using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingStats : MonoBehaviour
{
    public Slider timerSlider;
    public TextMeshProUGUI textValue;

    public int _seconds;
    public int _earnGold;
    public int _earnGem;

    PlayerStats playerStats;

    public void Start()
    {
        playerStats = GameObject.FindObjectOfType<PlayerStats>();
        AssignSliderValue();
        StartCoroutine(Timer(_seconds));
    }
    IEnumerator Timer(int seconds)
    {
        DisplayTimerValues(seconds);

        if (seconds > 0)
        {
            seconds--;
            yield return new WaitForSecondsRealtime(1);

            DisplayTimerValues(seconds);

            StartCoroutine(Timer(seconds));
        }
        else
        {
            yield return new WaitForSecondsRealtime(1);
            TimerCompleted();
            StartCoroutine(Timer(_seconds));
        }
        
    }

    void AssignSliderValue()
    {
        timerSlider.maxValue = _seconds;
    }

    void ChangeSliderValue(int value)
    {
        timerSlider.value = value;
    }

    void ChangeTextValue(int value)
    {
        textValue.text = value.ToString();
    }

    void DisplayTimerValues(int value)
    {
        ChangeSliderValue(_seconds - value);
        ChangeTextValue(value);
    }

    void TimerCompleted()
    {
        playerStats.EarnResources(_earnGold, _earnGem);
    }
}
