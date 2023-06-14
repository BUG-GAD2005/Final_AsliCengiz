using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingShapeData : MonoBehaviour
{
    public Image occuipedImage;

    private void Start()
    {
        occuipedImage.gameObject.SetActive(false);   
    }
}
