using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacingBuilding : MonoBehaviour
{
    public bool canPlacingSquare;

    public GameObject placeableGrid;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsGridEmpty(collision))
        {
            canPlacingSquare = true;
            placeableGrid = collision.gameObject;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (IsGridEmpty(collision))
        {
            canPlacingSquare = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == placeableGrid)
        {
            canPlacingSquare = false;
            placeableGrid = null;
        }
    }

    bool IsGridEmpty(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GridSquare") 
            && 
            collision.gameObject.GetComponent<Image>().sprite.name.Contains("empty"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
