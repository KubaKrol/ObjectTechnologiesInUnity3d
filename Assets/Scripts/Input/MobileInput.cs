using TMPro;
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

    public bool Select()
    {
        return Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began;
    }

    public bool DoubleClickStarted()
    {
        if (Input.touchCount == 2 && !_DoubleTouch)
        {
            _DoubleTouch = true;
            return true;
        }

        return false;
    }

    public bool DoubleClickEnded()
    {
        if (Input.touchCount == 1 && _DoubleTouch)
        {
            _DoubleTouch = false;
            return true;
        }

        return false;
    }

    public bool LookingAround()
    {
        return Input.touchCount == 1 && Input.touches[0].phase != TouchPhase.Ended;
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

            var _DistanceDelta = _CurrentDistance - _PreviousZoomDistance;
            _PreviousZoomDistance = _CurrentDistance;

            _DistanceDelta *= 0.1f;
            
            if (_DistanceDelta < 3f && _DistanceDelta > 0f)
            {
                return _DistanceDelta;
            }

            if (_DistanceDelta > 3f)
            {
                return 3f;
            }

            if (_DistanceDelta < 0f && _DistanceDelta > -3f)
            {
                return _DistanceDelta;
            }
            
            if (_DistanceDelta < -3f)
            {
                return -3f;
            }
        }

        return 0;
    }

    public float ZoomOut()
    {
        throw new System.NotImplementedException();
    }

    #region Private Variables

    private Vector2 _Touch0, _Touch1;
    private float _CurrentDistance = 0f;
    private float _PreviousZoomDistance = 0f;

    private bool _DoubleTouch;

    #endregion Private Variables
}
