using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<int> playerStatList = new List<int>();

    public List<int> emptyGridIndexInScene = new List<int>();

    public List<(int, int, int)> buildingStatsList = new List<(int, int, int)>();
    public List<Vector3> timerPosList = new List<Vector3>();
    public List<int> timerValuesList = new List<int>();


}
