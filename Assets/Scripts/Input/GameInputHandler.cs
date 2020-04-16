using GenericEnums;
using UnityEngine;

public class GameInputHandler
{
    #region Public Types

    #endregion Public Types


    #region Public VariablesH

    #endregion Public Variables


    #region Public Methods

    public GameInputHandler(
        IAmInput currentGameInput,
        TurnManager turnManager)
    {
        _CurrentGameInput = currentGameInput;
        _TurnManager = turnManager;
    }

    public void HandleInput()
    {
        if (_CurrentGameInput.SingleClick())
        {
            HexGrid.SelectCellAction?.Invoke(HexGrid.GetCell(_CurrentGameInput.PointerWorldPosition()), _TurnManager.CurrentTurn);
        }
    }
    
    #endregion Public Methods


    #region Inspector Variables

    #endregion Inspector Variables


    #region Unity Methods

    #endregion Unity Methods


    #region Private Variables

    private IAmInput _CurrentGameInput;
    private TurnManager _TurnManager;

    private Vector2 _InputStartPosition;
    private Vector2 _HexGridCameraStartPosition;

    #endregion Private Variables


    #region Private Methods

    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
