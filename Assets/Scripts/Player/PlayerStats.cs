using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class PlayerStats : MonoBehaviour
{
    public GameObject currentResourcesPrefab;
    public Transform resourcesLayout;

    //public Sprite goldSprite;
    //public Sprite gemSprite;

    public List<string> statName = new List<string> { "playerGold", "playerGem" };
    public List<int> statValue = new List<int>() { 10, 15 };
    public List<Sprite> statSprite = new List<Sprite>(2);

    //Dictionary<string, int> statsDictionary = new Dictionary<string, int>()
    //{
    //    { "playerGold", 10 },
    //    { "playerGem", 10 }

    //};


    

    
    

    private void Start()
    {
        //DefaultStatValues();
        DisplayStats();
    }

    //void DefaultStatValues()
    //{
    //    statValue[0] = 10;
    //    statValue[1] = 10;
    //}

    void DisplayStats()
    {

        //foreach(var stat in statsDictionary) 
        //{
        //    GameObject currentObj = Instantiate(currentResourcesPrefab, resourcesLayout);
        //    CurrentResource currentObjValues = currentObj.GetComponent<CurrentResource>();
        //    currentObjValues.image.sprite = stat.Key
        //}

        for (int i = 0; i < statName.Count; i++)
        {
            GameObject currentObj = Instantiate(currentResourcesPrefab, resourcesLayout).gameObject;
            CurrentResource currentObjValues = currentObj.GetComponent<CurrentResource>();
            currentObjValues.image.sprite = statSprite[i];
            currentObjValues.value.text = statValue[i].ToString();
        }


    }
}
