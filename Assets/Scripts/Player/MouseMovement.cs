using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    BuildingSlot buildingSlot;

    bool isDrag = false;

    public GameObject touchedShape;
    Vector3 touchedShapePos;
    Vector3 touchedShapeDefaultPos;

    private void Start()
    {
        buildingSlot = gameObject.GetComponent<BuildingSlot>();
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        TouchShape();
    }

    private void FixedUpdate()
    {
        DragShape();
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        DropShape();
    }

    void TouchShape()
    {
        if(!isDrag) 
        {
            touchedShape = buildingSlot.InstantiateBuildingShape();
            isDrag = true;
            touchedShapePos = gameObject.transform.position;
            touchedShapePos.z = 0;
            touchedShapeDefaultPos = touchedShapePos;
        }    
    }

    void DragShape()
    {
        if (isDrag)
        {
            touchedShapePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchedShapePos.z = 0;
            touchedShape.transform.position = touchedShapePos;
            touchedShape.transform.localPosition = new Vector3(touchedShape.transform.localPosition.x, touchedShape.transform.localPosition.y, 0);

            buildingSlot.TryPlaceBuilding();
        }
    }

    void DropShape()
    {
        isDrag = false;
        buildingSlot.PlaceBuilding();
        buildingSlot.DestroyBuildingShape(touchedShape);
        touchedShape = null;
    }
}
