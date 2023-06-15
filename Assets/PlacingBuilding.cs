using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacingBuilding : MonoBehaviour
{
    public bool canPlacingSquare;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GridSquare"))
        {
            //CanPlacingSquare(true);
            canPlacingSquare = true;
            //GetComponent<Image>().color = Color.green;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GridSquare"))
        {
            //GetComponent<Image>().color = Color.green;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GridSquare"))
        {
            canPlacingSquare = false;
            //CanPlacingSquare(false);
            //GetComponent<Image>().color = Color.white;
            //Debug.Log("aaaaaa");
            //Debug.Log(collision.gameObject.name);
        }
    }

    //public bool CanPlacingSquare(bool accuracy)
    //{
    //    return accuracy;
    //}
}
