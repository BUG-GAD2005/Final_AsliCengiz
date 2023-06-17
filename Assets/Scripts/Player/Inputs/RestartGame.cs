using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public GameSaveLoad gameSaveLoad;
    public GameData gameData;

    private void Start()
    {
        gameSaveLoad = GameObject.FindObjectOfType<GameSaveLoad>();
        gameData = gameSaveLoad._gameData;
    }
    public void RestartButton()
    {
        ClearBoard();

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
        Debug.Log("save yap");

    }

    void DestroyTimers()
    {
        GameObject[] timers = GameObject.FindGameObjectsWithTag("Timer");
        foreach (GameObject timer in timers)
        {
            Destroy(timer);
        }
        Debug.Log("save yap");
    }

    void SetPlayerResourcesDefault()
    {

    }
}
