using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;

[Serializable]
[CreateAssetMenu(menuName = "Building")]
public class BuildingData : ScriptableObject
{
    public string _name;
    public Sprite _iconBuilding;

    public int _costGold;
    public int _costGem;

    public int _generateGold;
    public int _generateGem;
    public float _InSeconds;

    [Serializable]
    public class Row
    {
        public bool[] columns;
        private int _size = 0;

        public Row() { }
        public Row(int size)
        {
            CreateRow(size);
        }
        public void CreateRow(int size)
        {
            _size = size;
            columns = new bool[_size];
            ClearRow();

        }
        public void ClearRow()
        {
            for (int i = 0; i < _size; i++)
            {
                columns[i] = false;
            }
        }
    }
    public int _columns = 0;
    public int _rows = 0;

    public Row[] board;
    public void Clear()
    {
        for (var i = 0; i < _rows; i++)
        {
            board[i].ClearRow();
        }
    }
    public void CreateNewBoard()
    {
        board = new Row[_rows];
        for (var i = 0; i < _rows; i++)
        {
            board[i] = new Row(_columns);
        }
    }

}
