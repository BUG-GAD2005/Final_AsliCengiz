using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacingBuilding : MonoBehaviour
{
    BuildingShape buildingShape;
    PlayerStats playerStats;
    GameSaveLoad gameSaveLoad;
    InstantiateTimer instantiateTimer;
    public List<GameObject> _currentShape;


    public Sprite tile_building;
    
    private void Start()
    {       
        buildingShape = GetComponent<BuildingShape>();
        playerStats = GameObject.FindObjectOfType<PlayerStats>();
        instantiateTimer = GameObject.FindObjectOfType<InstantiateTimer>();
        gameSaveLoad = GameObject.FindObjectOfType<GameSaveLoad>(); 

        _currentShape = buildingShape._currentShape;
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
        instantiateTimer.InstantiateTimerPrefab(_currentShape, buildingShape);
        gameSaveLoad.buildingShapesList.Add(buildingShape);
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
}
