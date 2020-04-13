using UnityEngine;

public interface IAmInput
{
    Vector2 PointerWorldPosition();
    Vector2 ScreenHoldPosition();

    bool SingleClick();
    bool HoldingScreen();
    bool ZoomIn();
    bool ZoomOut();
}
