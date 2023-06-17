using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
    GameSaveLoad gameSaveLoad;
    PlayerStats playerStats;

    private void Start()
    {
        gameSaveLoad = GameObject.FindObjectOfType<GameSaveLoad>();
        playerStats = GameObject.FindObjectOfType<PlayerStats>();

    }
    public void RestartButton()
    {
        ClearBoard();
        DestroyTimers();
        SetPlayerResourcesDefault();
        StartCoroutine(RestartButtonWait());
    }

    IEnumerator RestartButtonWait()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        gameSaveLoad.SaveGameData();
        gameSaveLoad.LoadGame();
    }
    void ClearBoard()
    {
        GameObject[] grids = GameObject.FindGameObjectsWithTag("GridSquare");
        foreach (GameObject grid in grids) 
        { 
            Destroy(grid);
        }
        Debug.Log("clear board");
    }

    void DestroyTimers()
    {
        GameObject[] timers = GameObject.FindGameObjectsWithTag("Timer");
        foreach (GameObject timer in timers)
        {
            Destroy(timer);
        }
        Debug.Log("destroy timers");
    }

    void SetPlayerResourcesDefault()
    {
        playerStats.statValue[0] = 10;
        playerStats.statValue[1] = 10;
        playerStats.DisplayStats();
        playerStats.AllBuildingsEnableCheck();
    }
}
