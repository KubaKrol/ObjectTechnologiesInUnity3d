using UnityEngine;

public interface IAmInput
{
    Vector2 PointerWorldPosition();

    bool Select();
    bool DoubleClickStarted();
    bool DoubleClickEnded();
    bool LookingAround();
    float ZoomIn();
    float ZoomOut();
}
