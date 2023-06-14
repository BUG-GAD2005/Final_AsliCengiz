using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateGridAtLayout : MonoBehaviour
{
    public Transform GridPanel;
    public GameObject GridPrefab;
    public int columns;
    public int rows;

    void Start()
    {
        InstantiateGrid();
    }

    void InstantiateGrid()
    {
        for (int i = 0; i < (columns * rows); i++)
        {
            Instantiate(GridPrefab, GridPanel);
        }
    }
}
