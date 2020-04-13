using System;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    #region Public Types

    #endregion Public Types


    #region Public Variables

    public float cameraOrthographicWidth => _CameraComponent.orthographicSize * 2f * Screen.width / Screen.height;
    public float cameraOrthographicHeight => _CameraComponent.orthographicSize * 2f;

    #endregion Public Variables


    #region Public Methods

    public void ZoomIn(float value)
    {
        if (_TargetOrthographicSize > minCameraSize)
        {
            if (_TargetOrthographicSize - value < minCameraSize)
            {
                _TargetOrthographicSize = minCameraSize;
            }
            else
            {
                _TargetOrthographicSize -= value * zoomSensitivity;
            }
        }
    }

    public void ZoomOut(float value)
    {
        if (_TargetOrthographicSize < maxCameraSize)
        {
            if (_TargetOrthographicSize + value > maxCameraSize)
            {
                _TargetOrthographicSize = maxCameraSize;
            }
            else
            {
                _TargetOrthographicSize += value * zoomSensitivity;
            }
        }
    }

    public void Zoom(float value)
    {
        if (_TargetOrthographicSize > maxCameraSize)
        {
            _TargetOrthographicSize = maxCameraSize;
        }
        
        if (_TargetOrthographicSize < minCameraSize)
        {
            _TargetOrthographicSize = minCameraSize;
        }

        _TargetOrthographicSize -= value * zoomSensitivity;
    }

    public void SetTargetPosition(Vector2 targetPosition)
    {
        _TargetPosition = targetPosition;
    }
    
    #endregion Public Methods


    #region Inspector Variables

    [SerializeField] private GameInput _GameInput;
    
    [SerializeField] private float minCameraSize = 30f;
    [SerializeField] private float maxCameraSize = 150f;

    [SerializeField] private float zoomSensitivity = 3f;
    
    #endregion Inspector Variables


    #region Unity Methods

    private void Awake()
    {
        _InputHandler = new GameCameraInputHandler(_GameInput.currentInput, this);
        _CameraComponent = GetComponent<Camera>();
        
        _TargetOrthographicSize = _CameraComponent.orthographicSize;
        _TargetPosition = transform.position;
    }
    
    private void Start()
    {
    }

    private void Update()
    {
        _InputHandler.HandleInput();
        
        AdjustCameraOrthographicSizeToTargetValue();
        MoveToTargetPosition(_TargetPosition, 0.1f);
    }
    
    #endregion Unity Methods


    #region Private Variables

    private GameCameraInputHandler _InputHandler;
    
    private Camera _CameraComponent;

    private float _TargetOrthographicSize;
    private float _ZoomVelocity;

    private Vector2 _TargetPosition;
    private Vector3 _MovingVelocity;
    
    #endregion Private Variables


    #region Private Methods
    
    private void AdjustCameraOrthographicSizeToTargetValue()
    {
        if (Math.Abs(_CameraComponent.orthographicSize - _TargetOrthographicSize) > 0.01f)
        {
            _CameraComponent.orthographicSize = Mathf.SmoothDamp(_CameraComponent.orthographicSize, _TargetOrthographicSize, ref _ZoomVelocity, 0.1f);
        }
    }

    private void MoveToTargetPosition(Vector3 targetPosition, float speed)
    {
        targetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _MovingVelocity, speed);
    }

    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
