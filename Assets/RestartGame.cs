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
    }
    public void RestartButton()
    {
        Debug.Log("a");
        gameData.emptyGridIndexInScene.Clear();
        gameSaveLoad.LoadGame();
        Debug.Log("b");
    }
}
