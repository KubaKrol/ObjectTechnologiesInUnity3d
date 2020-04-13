using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerInputHandler
{
    #region Public Types

    #endregion Public Types


    #region Public Variables

    #endregion Public Variables


    #region Public Methods

    public PlayerInputHandler(IAmInput input)
    {
        _CurrentInput = input;
    }

    public void HandleInput()
    {
        if (_CurrentInput.SingleClick())
        {
            HexGrid.SelectCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
    
    #endregion Public Methods


    #region Inspector Variables

    #endregion Inspector Variables


    #region Unity Methods

    #endregion Unity Methods


    #region Private Variables

    private IAmInput _CurrentInput;

    #endregion Private Variables


    #region Private Methods

    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
