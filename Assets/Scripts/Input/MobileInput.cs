using UnityEngine;

/// <summary>
/// Input.touches[0].tapCount != 2
/// </summary>
public class MobileInput : IAmInput
{
    public Vector2 PointerWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.touches[0].position);
    }

    public bool SingleClick()
    {
        return Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began;
    }

    public bool HoldingScreenToLookAround()
    {
        return Input.touchCount > 0;
    }

    public float ZoomIn()
    {
        if (Input.touchCount >= 2)
        {
            float _CurrentDistance;
            _Touch0 = Input.GetTouch(0).position;
            _Touch1 = Input.GetTouch(1).position;
            _CurrentDistance = Vector2.Distance(_Touch0, _Touch1);
            
            if (Input.touches[1].phase == TouchPhase.Began)
            {
                _PreviousZoomDistance = _CurrentDistance;
            }

            return _CurrentDistance - _PreviousZoomDistance;
        }

        return 0f;
    }

    public float ZoomOut()
    {
        throw new System.NotImplementedException();
    }
    
    
    #region Private Variables

    private Vector2 _Touch0, _Touch1;
    private float _CurrentDistance = 0f;
    private float _PreviousZoomDistance = 0f;

    #endregion Private Variables
}
