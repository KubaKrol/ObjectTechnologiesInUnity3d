using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GameCameraInputHandler
{
    #region Public Types

    #endregion Public Types


    #region Public Variables

    #endregion Public Variables


    #region Public Methods

    public GameCameraInputHandler(
        IAmInput currentGameInput,
        GameCamera gameCamera)
    {
        _CurrentGameInput = currentGameInput;
        _GameCamera = gameCamera;
    }
    
    public void HandleInput()
    {
        _GameCamera.Zoom(_CurrentGameInput.ZoomIn());

        _CurrentGameInput.DoubleClickStarted();
        
        if (_CurrentGameInput.Select() || _CurrentGameInput.DoubleClickEnded())
        {
            _InputStartPosition = _CurrentGameInput.PointerWorldPosition();
            _InputStartPositionInCameraLocalSpace = _GameCamera.transform.InverseTransformPoint(_InputStartPosition);
            _GameCameraStartPosition = _GameCamera.transform.position;
        }

        if (_CurrentGameInput.LookingAround())
        {
            var pointerCurrentPositionRelativeToStartingPosition = -(_CurrentGameInput.PointerWorldPosition() - _InputStartPosition);
            _InputStartPosition = _GameCamera.transform.TransformPoint(_InputStartPositionInCameraLocalSpace);

            _GameCamera.SetTargetPosition(_GameCameraStartPosition + pointerCurrentPositionRelativeToStartingPosition);
        }   
    }
    
    #endregion Public Methods


    #region Inspector Variables

    #endregion Inspector Variables


    #region Unity Methods

    #endregion Unity Methods


    #region Private Variables

    private readonly IAmInput _CurrentGameInput;
    private readonly GameCamera _GameCamera;

    private Vector2 _GameCameraStartPosition;
    private Vector2 _InputStartPosition;
    private Vector2 _InputStartPositionInCameraLocalSpace;

    #endregion Private Variables


    #region Private Methods

    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
