using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTimer : MonoBehaviour
{
    public GameObject timerPrefab;
    GameObject canvas;

    Vector3 upperSquare;

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    public void InstantiateTimerPrefab(List<GameObject> _currentShape, BuildingShape buildingShape)
    {
        FindUpperSquareVector(_currentShape, buildingShape);
        StartTimer(upperSquare, buildingShape);
    }

    public void InstantiateTimerPrefabForLoad(GameData gameData)
    {
        for (int i = 0; i < gameData.timerValuesList.Count; i++)
        {
            GameObject timerPref = Instantiate(timerPrefab, canvas.transform);
            timerPref.transform.position = gameData.timerPosList[i];
            timerPref.GetComponent<BuildingStats>()._seconds = gameData.buildingStatsList_seconds[i];
            timerPref.GetComponent<BuildingStats>()._earnGold = gameData.buildingStatsList_earnGold[i];
            timerPref.GetComponent<BuildingStats>()._earnGem = gameData.buildingStatsList_earnGem[i];
            timerPref.GetComponent<BuildingStats>().StartTimer(gameData.timerValuesList[i]);
        }
        
    }
    void StartTimer(Vector3 upperSquare, BuildingShape buildingShape)
    {
        GameObject timerPref = Instantiate(timerPrefab, canvas.transform);
        timerPref.transform.position = upperSquare + new Vector3(0, 0.5f, 0);

        AssignBuildingStats(timerPref, buildingShape);
        timerPref.GetComponent<BuildingStats>().StartTimer(timerPref.GetComponent<BuildingStats>()._seconds);
    }

    void FindUpperSquareVector(List<GameObject> _currentShape, BuildingShape buildingShape)
    {
        upperSquare = buildingShape.gameObject.transform.position;
        foreach (var square in _currentShape)
        {
            Vector3 squareY = square.transform.position;
            if (upperSquare == null)
            {
                upperSquare.y = squareY.y;
            }
            else if (squareY.y > upperSquare.y)
            {
                upperSquare.y = squareY.y;
            }
        }
    }

    void AssignBuildingStats(GameObject timerPref, BuildingShape buildingShape)
    {
        timerPref.GetComponent<BuildingStats>()._seconds = buildingShape.buildingData._InSeconds;
        timerPref.GetComponent<BuildingStats>()._earnGold = buildingShape.buildingData._earnGold;
        timerPref.GetComponent<BuildingStats>()._earnGem = buildingShape.buildingData._earnGem;
    }
}
