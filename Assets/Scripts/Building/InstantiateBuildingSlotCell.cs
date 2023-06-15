using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InstantiateBuildingSlotCell : MonoBehaviour
{
    public GameObject BuildingSlotPrefab;
    BuildingData[] buildings;

    void GetAllBuildingResources()
    {
        buildings = Resources.LoadAll<BuildingData>("Buildings");
    }

    void AssignValuesInBuildingSlot()
    {
        foreach (var building in buildings) 
        {
            Instantiate(BuildingSlotPrefab, gameObject.transform);

            BuildingSlot buildingSlot = BuildingSlotPrefab.GetComponent<BuildingSlot>();
            buildingSlot.buildingData = building;
            buildingSlot.DisplaySlotInputs();
        }
    }



    private void Start()
    {

        GetAllBuildingResources();
        AssignValuesInBuildingSlot();
    }
}
