using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacingBuilding : MonoBehaviour
{
    public List<GameObject> _currentShape;
    BuildingShape buildingShape;
    PlayerStats playerStats;

    public Sprite tile_building;
    public GameObject timerPrefab;
    public GameObject canvas;

    Vector3 upperSquare;

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        buildingShape = GetComponent<BuildingShape>();
        _currentShape = buildingShape._currentShape;
        playerStats = GameObject.FindObjectOfType<PlayerStats>();
    }

    public void PlacingShapeColoring()
    {
        foreach (var square in _currentShape)
        {
            if (CanPlaceShape())
            {
                square.GetComponent<Image>().color = new Color(0, 1, 0, 0.5f); //green
            }
            else
            {
                square.GetComponent<Image>().color = new Color(1, 0, 0, 0.5f); //red
            }
        }
    }

    public void PlacingShapeInGrid()
    {
        foreach (var square in _currentShape)
        {
            if (CanPlaceShape())
            {
                square.GetComponent<PlacingBuildingSquareCheck>().placeableGrid.GetComponent<Image>().sprite = tile_building;               
            }
        }
    }

    public bool CanPlaceShape()
    {
        foreach (var square in _currentShape)
        {
            if (!square.GetComponent<PlacingBuildingSquareCheck>().canPlacingSquare)
            {
                return false;
            }
        }
        return true;
    }

    public void DestroyBuildingShape(GameObject instantiatedBuildingShape)
    {
        Destroy(instantiatedBuildingShape);
    }

    public void TryPlaceBuilding()
    {
        PlacingShapeColoring();
    }

    public void PlaceBuilding()
    {
        PlacingShapeInGrid();
        SpendResources();
        FindUpperSquareVector();
    }

    public void SpendResources()
    {
        int gold = buildingShape.buildingData._costGold;
        int gem = buildingShape.buildingData._costGem;
        if (CanPlaceShape())
        {
            playerStats.SpendResources(gold, gem);
        }
    }

    public void StartTimer(Vector3 upperSquare)
    {
        GameObject timerPref = Instantiate(timerPrefab, canvas.transform);
        timerPref.transform.position = upperSquare + new Vector3 (0, 0.5f, 0);

        AssignBuildingStats(timerPref);
    }

    void FindUpperSquareVector()
    {       
        upperSquare = gameObject.transform.position;
        foreach (var square in _currentShape)
        {
            Vector3 squareY = square.transform.position;
            if (upperSquare == null)
            {
                upperSquare.y = squareY.y;
            }
            else if(squareY.y > upperSquare.y)
            {
                upperSquare.y = squareY.y;
            }
        }

        StartTimer(upperSquare);
    }

    void AssignBuildingStats(GameObject timerPref)
    {
        timerPref.GetComponent<BuildingStats>()._seconds = buildingShape.buildingData._InSeconds;
        timerPref.GetComponent<BuildingStats>()._earnGold = buildingShape.buildingData._earnGold;
        timerPref.GetComponent<BuildingStats>()._earnGem = buildingShape.buildingData._earnGem;
    }

}
