using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingSlot : MonoBehaviour
{
    public PlayerStats playerStats;
    public BuildingData buildingData;
    public GameObject buildingSquareShapePrefab;
    BuildingSquareShape buildingSquareShape;

    MouseMovement mouseMovement;
    public Image _cantPurchaseImage;

    public TextMeshProUGUI _name;
    public Image _iconBuilding;

    public TextMeshProUGUI _costGold;
    public TextMeshProUGUI _costGem;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        mouseMovement = GetComponent<MouseMovement>();
        EnableCheck();
    }

    public void DisplaySlotInputs()
    {
        _name.text = buildingData.name;
        _iconBuilding.sprite = buildingData._iconBuilding;
        _costGold.text = buildingData._costGold.ToString();
        _costGem.text = buildingData._costGem.ToString();
    }

    public bool CanPurchase()
    {
        if (playerStats.statValue[0] >= buildingData._costGold 
            &&
            playerStats.statValue[1] >= buildingData._costGem)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void EnableCheck()
    {
        if (CanPurchase())
        {
            mouseMovement.enabled = true;
            _cantPurchaseImage.GetComponent<Image>().enabled = false;
        }
        else
        {
            mouseMovement.enabled = false;
            _cantPurchaseImage.GetComponent<Image>().enabled = true;
        }
    }

    public GameObject InstantiateBuildingShape()
    {
        GameObject currentPrefab = Instantiate(buildingSquareShapePrefab, transform);
        buildingSquareShape = currentPrefab.GetComponent<BuildingSquareShape>();
        buildingSquareShape.RequestNewShape(buildingData);

        currentPrefab.layer = LayerMask.NameToLayer("UI");
        currentPrefab.transform.position = Vector3.zero;

        return currentPrefab;
    }

    public void DestroyBuildingShape(GameObject instantiatedBuildingShape)
    {
        Destroy(instantiatedBuildingShape);
    }

    public void TryPlaceBuilding()
    {
        buildingSquareShape.PlacingShapeColoring();
    }

    public void PlaceBuilding()
    {
        buildingSquareShape.PlacingShapeInGrid();
        SpendResources();
    }

    public void SpendResources()
    {
        if (buildingSquareShape.CanPlaceShape())
        {
            playerStats.SpendResources(buildingData._costGold, buildingData._costGem);
        }
    }
}
