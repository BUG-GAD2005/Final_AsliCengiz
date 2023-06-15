using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isDrag = false;

    GameObject touchedShape;
    Vector3 touchedShapePos;
    Vector3 touchedShapeDefaultPos;
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
        isDrag = true;
        touchedShape = gameObject;
        touchedShapePos = touchedShape.transform.position;
        touchedShapeDefaultPos = touchedShapePos;
    }

    void DragShape()
    {
        if (isDrag)
        {
            touchedShapePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchedShapePos.z = 0;
            touchedShape.transform.position = touchedShapePos;
        }
    }

    void DropShape()
    {
        isDrag = false;

        //if (touchedShape != null)
        //{
        //    if (touchedShape.GetComponent<Shape>().TryPlaceShape())
        //    {
        //        touchedShape = null;
        //    }
        //    else
        //    {
        //        touchedShape.transform.position = touchedShapeDefaultPos;
        //    }
        //}
    }
}
