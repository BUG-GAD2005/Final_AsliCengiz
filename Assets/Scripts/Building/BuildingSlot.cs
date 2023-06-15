using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingSlot : MonoBehaviour
{
    public BuildingData buildingData;

    public TextMeshProUGUI _name;
    public Image _iconBuilding;

    public TextMeshProUGUI _costGold;
    public TextMeshProUGUI _costGem;

    public void DisplaySlotInputs()
    {
        _name.text = buildingData.name;
        _iconBuilding.sprite = buildingData._iconBuilding;
        _costGold.text = buildingData._costGold.ToString();
        _costGem.text = buildingData._costGem.ToString();
    }
}
