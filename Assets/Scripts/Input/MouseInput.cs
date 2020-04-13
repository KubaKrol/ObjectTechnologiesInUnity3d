using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : IAmInput
{
    public Vector2 PointerWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    
    public Vector2 ScreenHoldPosition()
    {
        throw new System.NotImplementedException();
    }

    public bool SingleClick()
    {
        return Input.GetMouseButtonUp(0);
    }
    
    public bool HoldingScreen()
    {
        throw new System.NotImplementedException();
    }

    public bool ZoomIn()
    {
        throw new System.NotImplementedException();
    }

    public bool ZoomOut()
    {
        throw new System.NotImplementedException();
    }
}
