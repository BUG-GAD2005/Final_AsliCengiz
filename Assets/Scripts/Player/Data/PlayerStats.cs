using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class PlayerStats : MonoBehaviour
{
    public GameObject currentResourcesPrefab;
    public Transform resourcesLayout;

    public List<string> statName = new List<string> { "playerGold", "playerGem" };
    public List<int> statValue = new List<int>() { 10, 15 };
    public List<Sprite> statSprite = new List<Sprite>(2);

    private void Start()
    {
        DisplayStats();
    }

    void DisplayStats()
    {
        if (resourcesLayout.childCount > 0)
        {
            for (int i = 0; i < resourcesLayout.childCount; i++)
            {
                Destroy(resourcesLayout.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < statName.Count; i++)
        {
            GameObject currentObj = Instantiate(currentResourcesPrefab, resourcesLayout).gameObject;
            CurrentResource currentObjValues = currentObj.GetComponent<CurrentResource>();
            currentObjValues.image.sprite = statSprite[i];
            currentObjValues.value.text = statValue[i].ToString();
        }
    }

    public void SpendResources(int gold, int gem)
    {
        statValue[0] -= gold;
        statValue[1] -= gem;

        DisplayStats();

        AllBuildingsEnableCheck();
    }

    public void EarnResources(int gold, int gem)
    {
        statValue[0] += gold;
        statValue[1] += gem;

        DisplayStats();

        AllBuildingsEnableCheck();
    }

    void AllBuildingsEnableCheck()
    {
        BuildingSlot[] _buildings = GameObject.FindObjectsOfType<BuildingSlot>();
        foreach (var building in _buildings)
        {
            building.EnableCheck();
        }
    }
}
