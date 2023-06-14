using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[Serializable]
[CreateAssetMenu(menuName = "Building")]
public class BuildingData : ScriptableObject
{
    public string _name;

    public int _costGold;
    public int _costGem;

    public Sprite thumbnail;
}
