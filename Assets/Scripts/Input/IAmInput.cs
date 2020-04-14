using UnityEngine;

public interface IAmInput
{
    Vector2 PointerWorldPosition();

    bool SingleClick();
    bool HoldingScreenToLookAround();
    float ZoomIn();
    float ZoomOut();
}
