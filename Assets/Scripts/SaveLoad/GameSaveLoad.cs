using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;
using static UnityEditor.Progress;
using UnityEngine.Playables;

[System.Serializable]
public class GameSaveLoad : MonoBehaviour
{
    public GameData _gameData;
    string fullPath;
    public int automaticSaveSecond;

    PlayerStats playerStats;
    InstantiateTimer instantiateTimer;
    InstantiateGridAtLayout instantiateGridAtLayout;

    public List<BuildingShape> buildingShapesList;


    void Start()
    {
        playerStats = GameObject.FindObjectOfType<PlayerStats>();
        instantiateTimer = GameObject.FindObjectOfType<InstantiateTimer>();
        instantiateGridAtLayout = GameObject.FindObjectOfType<InstantiateGridAtLayout>();
        fullPath = Application.persistentDataPath + "/GameData.txt";
        LoadGame();

        StartCoroutine(AutomaticSave(automaticSaveSecond));
    }

    #region Load
    public void LoadGame()
    {
        //Read File
        if (File.Exists(fullPath))
        {
            string fileContents = File.ReadAllText(fullPath);
            _gameData = JsonUtility.FromJson<GameData>(fileContents);

            LoadGameData();
        }
    }

    void LoadGameData()
    {
        LoadPlayerStats();
        LoadGridsWithSprite();
        LoadTimers();
    }

    void LoadPlayerStats()
    {
        playerStats.statValue = _gameData.playerStatList;
    }
    void LoadGridsWithSprite()
    {
        instantiateGridAtLayout.InstantiateGrid(_gameData.emptyGridIndexInScene);
    }

    void LoadTimers()
    {
        instantiateTimer.InstantiateTimerPrefabForLoad(_gameData);
    }
    #endregion

    #region Save
    public void SaveGame(GameData gameData)
    {
        if (Directory.Exists(fullPath))
        {
            Directory.CreateDirectory(fullPath);
        }
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(fullPath, json);
    }

    public void SaveGameData()
    {
        ClearArraysForSave();
        AssignListsForSave();
        AssignValuesInGameData();
    }

    void ClearArraysForSave()
    {
        if (_gameData.buildingStatsList_seconds != null && _gameData.buildingStatsList_seconds.Count > 0)
        {
            _gameData.buildingStatsList_seconds.Clear();
        }
        if (_gameData.buildingStatsList_earnGold != null && _gameData.buildingStatsList_earnGold.Count > 0)
        {
            _gameData.buildingStatsList_earnGold.Clear();
        }
        if (_gameData.buildingStatsList_earnGem != null && _gameData.buildingStatsList_earnGem.Count > 0)
        {
            _gameData.buildingStatsList_earnGem.Clear();
        }

        if (_gameData.timerPosList != null && _gameData.timerPosList.Count > 0)
        {
            _gameData.timerPosList.Clear();
        }
        if (_gameData.timerValuesList != null && _gameData.timerValuesList.Count > 0)
        {
            _gameData.timerValuesList.Clear();
        }

        if(_gameData.emptyGridIndexInScene != null && _gameData.emptyGridIndexInScene.Count > 0)
        {
            _gameData.emptyGridIndexInScene.Clear();
        }
    }

    void AssignListsForSave()
    {
        GameObject[] grids = GameObject.FindGameObjectsWithTag("GridSquare");
        for (int i = 0; i < grids.Length; i++)
        {
            if (grids[i].GetComponent<Image>().sprite.name.Contains("empty"))
            {
                _gameData.emptyGridIndexInScene.Add(i);
            }
        }

        BuildingStats[] buildingStats = GameObject.FindObjectsOfType<BuildingStats>();
        foreach (BuildingStats building in buildingStats)
        {
            _gameData.buildingStatsList_seconds.Add(building._seconds);
            _gameData.buildingStatsList_earnGold.Add(building._earnGold);
            _gameData.buildingStatsList_earnGem.Add(building._earnGem);

            _gameData.timerValuesList.Add((int)building.timerSlider.value);
            _gameData.timerPosList.Add(building.gameObject.transform.position);
        }       
    }

    void AssignValuesInGameData()
    {
        var gameData = new GameData
        {
            playerStatList = playerStats.statValue,

            emptyGridIndexInScene = _gameData.emptyGridIndexInScene,

            buildingStatsList_seconds = _gameData.buildingStatsList_seconds,
            buildingStatsList_earnGold = _gameData.buildingStatsList_earnGold,
            buildingStatsList_earnGem = _gameData.buildingStatsList_earnGem,

            timerValuesList = _gameData.timerValuesList,
            timerPosList = _gameData.timerPosList,
        };

        SaveGame(gameData);
    }
    #endregion

    IEnumerator AutomaticSave(int second)
    {
        yield return new WaitForSecondsRealtime(second);

        SaveGameData();
        StartCoroutine(AutomaticSave(automaticSaveSecond));
        Debug.Log("saved");
    }
}
