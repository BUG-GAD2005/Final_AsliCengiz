using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingSlot : MonoBehaviour
{
    public PlayerStats playerStats;
    public BuildingData buildingData;

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

    void EnableCheck()
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
}
