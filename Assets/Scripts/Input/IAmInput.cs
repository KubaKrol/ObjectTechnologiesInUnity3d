using UnityEngine;

public interface IAmInput
{
    Vector2 PointerWorldPosition();

    bool SingleClick();
    bool HoldingScreen();
    float ZoomIn();
    float ZoomOut();
}
