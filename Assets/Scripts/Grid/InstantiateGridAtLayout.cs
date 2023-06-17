using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class InstantiateGridAtLayout : MonoBehaviour
{
    public Transform GridPanel;
    public GameObject GridPrefab;
    public int columns;
    public int rows;

    public Sprite tile_empty;
    public Sprite tile_building;

    public void InstantiateGrid(List<int> emptyGridIndex)
    {
        if (IsListNull(emptyGridIndex))
        {
            for (int i = 0; i < (columns * rows); i++)
            {
                Instantiate(GridPrefab, GridPanel);
            }
        }
        else
        {
            for (int i = 0; i < (columns * rows); i++)
            {
                GameObject grid = Instantiate(GridPrefab, GridPanel);

                for (int j = 0; j < emptyGridIndex.Count; j++)
                {
                    if (i == emptyGridIndex[j])
                    {
                        grid.GetComponent<Image>().sprite = tile_empty;
                        break;
                    }

                    grid.GetComponent<Image>().sprite = tile_building;
                }
            }
        }
        
    }

    bool IsListNull(List<int> emptyGridIndex)
    {
        GameObject[] grids = GameObject.FindGameObjectsWithTag("GridSquare");
        for (int i = 0; i < grids.Length;)
        {
            return false;
        }

        if (emptyGridIndex.Count > 0)
        {
            return false;
        }

        return true;
    }
}
