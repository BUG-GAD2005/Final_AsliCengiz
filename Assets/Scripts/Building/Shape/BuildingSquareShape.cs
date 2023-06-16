using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSquareShape : MonoBehaviour
{
    public bool isMovable { get; private set; } = true;

    public Sprite tile_building;

    public GameObject squareShapeImage;
    public BuildingData buildingData;
    public List<GameObject> _currentShape = new List<GameObject>();
    public void RequestNewShape(BuildingData shapeData)
    {
        CreateShape(shapeData);
    }

    public void PlacingShapeColoring()
    {
        foreach (var square in _currentShape)
        {
            if (CanPlaceShape())
            {
                square.GetComponent<Image>().color = new Color(0, 1, 0, 0.5f); //green
            }
            else
            {
                square.GetComponent<Image>().color = new Color(1, 0, 0, 0.5f); //red
            }
        }      
    }

    public void PlacingShapeInGrid()
    {
        foreach (var square in _currentShape)
        {
            if (CanPlaceShape())
            {
                //square.transform.position = square.GetComponent<PlacingBuilding>().placeableGrid.transform.position;
                square.GetComponent<PlacingBuilding>().placeableGrid.GetComponent<Image>().sprite = tile_building;
            }
        }

    }

    public bool CanPlaceShape()
    {
        foreach (var square in _currentShape)
        {
            if (!square.GetComponent<PlacingBuilding>().canPlacingSquare)
            {
                return false;
            }
        }
        return true;
    }

    public void CreateShape(BuildingData shapeData)
    {
        buildingData = shapeData;
        var totalSquareNumber = GetNumberOfSquares(shapeData);

        while (_currentShape.Count < totalSquareNumber)
        {
            _currentShape.Add(Instantiate(squareShapeImage, transform) as GameObject);
        }
        foreach (var square in _currentShape)
        {
            square.gameObject.transform.position = Vector3.zero;
            square.gameObject.SetActive(false);
        }

        var squareRect = squareShapeImage.GetComponent<RectTransform>();
        var moveDistance = new Vector2(squareRect.rect.width * squareRect.localScale.x, squareRect.rect.height * squareRect.localScale.y);

        int currentIndexList = 0;
        //Set positions to form final shape
        for (var row = 0; row < shapeData._rows; row++)
        {
            for (var column = 0; column < shapeData._columns; column++)
            {
                if (shapeData.board[row].columns[column])
                {
                    _currentShape[currentIndexList].SetActive(true);
                    _currentShape[currentIndexList].GetComponent<RectTransform>().localPosition = new Vector2(GetXPositionForShapeSquare(shapeData, column, moveDistance),
                    GetYPositionForShapeSquare(shapeData, row, moveDistance));

                    currentIndexList++;
                }
            }
        }
    }
    private float GetYPositionForShapeSquare(BuildingData shapeData, int row, Vector2 moveDistance)
    {
        float shiftOnY = 0f;
        if (shapeData._rows > 1)
        {
            if (shapeData._rows % 2 != 0)
            {
                var middleSquareIndex = (shapeData._rows - 1) / 2;
                var multiplier = (shapeData._rows - 1) / 2 - row;

                if (row < middleSquareIndex)//move it on minus
                {
                    shiftOnY = moveDistance.y;
                    shiftOnY *= multiplier;
                }
                else if (row > middleSquareIndex)//move it on plus
                {
                    shiftOnY = moveDistance.y;
                    shiftOnY *= multiplier;
                }
            }
            else
            {
                var middleSquareIndex2 = (shapeData._rows == 2) ? 1 : shapeData._rows / 2;
                var middleSquareIndex1 = (shapeData._rows == 2) ? 0 : shapeData._rows - 2;
                var multiplier = shapeData._rows / 2;

                if (row == middleSquareIndex1 || row == middleSquareIndex2)
                {
                    if (row == middleSquareIndex2)
                        shiftOnY = moveDistance.y / 2 * -1;
                    if (row == middleSquareIndex1)
                        shiftOnY = (moveDistance.y / 2);
                }
                if (row < middleSquareIndex1 && row < middleSquareIndex2)//move it on minus
                {
                    shiftOnY = moveDistance.y * 1;
                    shiftOnY *= multiplier;
                }
                else if (row > middleSquareIndex1 && row > middleSquareIndex2)//move it on plus
                {
                    shiftOnY = moveDistance.y * -1;
                    shiftOnY *= multiplier;
                }
            }
        }
        return shiftOnY;
    }
    private float GetXPositionForShapeSquare(BuildingData shapeData, int column, Vector2 moveDistance)
    {
        float shiftOnX = 0f;
        if (shapeData._columns > 1)//vertical position calculate
        {
            if (shapeData._columns % 2 != 0)
            {
                var middleSquareIndex = (shapeData._columns - 1) / 2;
                var multiplier = (shapeData._columns - 1) / 2 - column;
                if (column < middleSquareIndex)//move it on the negative
                {
                    shiftOnX = moveDistance.x;
                    shiftOnX *= multiplier;
                }
                else if (column > middleSquareIndex)//move it on plus
                {
                    shiftOnX = moveDistance.x;
                    shiftOnX *= multiplier;
                }
            }
            else
            {
                var middleSquareIndex2 = (shapeData._columns == 2) ? 1 : shapeData._columns / 2;
                var middleSquareIndex1 = (shapeData._columns == 2) ? 0 : shapeData._columns - 1;
                var multiplier = shapeData._columns / 2;
                if (column == middleSquareIndex1 || column == middleSquareIndex2)
                {
                    if (column == middleSquareIndex2)
                        shiftOnX = moveDistance.x / 2;
                    if (column == middleSquareIndex1)
                        shiftOnX = (moveDistance.x / 2) * -1;

                }

                if (column < middleSquareIndex1 && column < middleSquareIndex2)//move it on the negative
                {
                    shiftOnX = moveDistance.x * -1;
                    shiftOnX *= multiplier;
                }
                else if (column > middleSquareIndex1 && column > middleSquareIndex2)//move it on plus
                {
                    shiftOnX = moveDistance.x * 1;
                    shiftOnX *= multiplier;
                }
            }
        }
        return shiftOnX;
    }
    private int GetNumberOfSquares(BuildingData shapeData)
    {
        int number = 0;

        foreach (var rowData in shapeData.board)
        {
            foreach (var active in rowData.columns)
            {
                if (active)
                {
                    number++;
                }
            }
        }
        return number;
    }
}
