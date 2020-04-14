using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : IAmInput
{
    public Vector2 PointerWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public bool SingleClick()
    {
        return Input.GetMouseButtonDown(0);
    }
    
    public bool HoldingScreenToLookAround()
    {
        return Input.GetMouseButton(0);
    }

    public float ZoomIn()
    {
        return (Input.mouseScrollDelta.y);
    }

    public float ZoomOut()
    {
        return (Input.mouseScrollDelta.y);
    }
}
