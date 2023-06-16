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
    GameData _gameData;
    string fullPath;

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
    }

    #region Load
    public void LoadGame()
    {
        //Read File
        if (File.Exists(fullPath))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(fullPath);
            _gameData = JsonUtility.FromJson<GameData>(fileContents);

            //LoadGameData();
            playerStats.statValue = _gameData.playerStatList;
            instantiateGridAtLayout.InstantiateGrid(_gameData.emptyGridIndexInScene);
        }
    }

    void LoadGameData()
    {
        LoadPlayerStats();
        LoadGridsWithSprite();
        //LoadTimerObjects(_gameData);
        //LoadTimerValues();
        //LoadTimerPos();
        LoadTimers();
    }

    void LoadPlayerStats()
    {
        playerStats.statValue = _gameData.playerStatList;
    }
    void LoadGridsWithSprite()
    {
        //_gameData.emptyGridIndexInScene = gameData.emptyGridIndexInScene;
        instantiateGridAtLayout.InstantiateGrid(_gameData.emptyGridIndexInScene);
    }

    //void LoadTimerObjects(GameData gameData)
    //{
    //    timersInScene = gameData.timerList;
    //}

    //void LoadTimerValues()
    //{
    //    _gameData.timerValuesList = gameData.timerValuesList;
    //}
    //void LoadTimerPos()
    //{
    //    _gameData.timerPosList = gameData.timerPosList;
    //}
    void LoadTimers()
    {
        //_gameData.buildingStatsList = gameData.buildingStatsList;
        for (int i = 0; i < _gameData.buildingStatsList.Count; i++)
        {
            instantiateTimer.InstantiateTimerPrefabForLoad(_gameData.timerPosList[i], _gameData.timerValuesList[i]);

        }
        //LoadTimerObjects(gameData);
        //LoadTimerValues(gameData);
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
        if (_gameData.buildingStatsList != null && _gameData.buildingStatsList.Count > 0)
        {
            _gameData.buildingStatsList.Clear();
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
            _gameData.buildingStatsList.Add((building._seconds, building._earnGold, building._earnGem));
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

            buildingStatsList = _gameData.buildingStatsList,
            timerValuesList = _gameData.timerValuesList,
            timerPosList = _gameData.timerPosList,
        };

        SaveGame(gameData);
    }
    #endregion

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SaveGameData();
            Debug.Log("saved");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
            Debug.Log("loaded");
        }
    }
}
