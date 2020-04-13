using UnityEngine;

public class HexGridInputHandler
{
    #region Public Types

    #endregion Public Types


    #region Public Variables

    #endregion Public Variables


    #region Public Methods

    public HexGridInputHandler(
        IAmInput currentGameInput,
        HexGrid hexGrid)
    {
        _CurrentGameInput = currentGameInput;
        _HexGrid = hexGrid;
    }

    public void HandleInput()
    {
        if (_CurrentGameInput.SingleClick())
        {
            _HexGrid.GetCell(_CurrentGameInput.PointerWorldPosition());
        }
    }
    
    #endregion Public Methods


    #region Inspector Variables

    #endregion Inspector Variables


    #region Unity Methods

    #endregion Unity Methods


    #region Private Variables

    private IAmInput _CurrentGameInput;
    private HexGrid _HexGrid;

    private Vector2 _InputStartPosition;
    private Vector2 _HexGridCameraStartPosition;

    #endregion Private Variables


    #region Private Methods

    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
