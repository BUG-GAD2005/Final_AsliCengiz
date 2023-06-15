using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InstantiateBuildingSlotCell : MonoBehaviour
{
    public GameObject BuildingSlotPrefab;
    //List<BuildingData> buildings = new List<BuildingData>();
    BuildingData[] buildings;

    void GetAllBuildingResources()
    {
        if(buildings != null)
        {
            for (int i = 0; i < buildings.Length; i++)
            {
                Array.Clear(buildings, i, buildings.Length);
                //Debug.Log(buildings[i].name);
            }
        }
        else
        {
            //Debug.Log("zatenboþþ???");
            buildings = Resources.LoadAll<BuildingData>("Buildings");
        }
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
