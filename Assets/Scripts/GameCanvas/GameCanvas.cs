using System;
using System.Collections;
using System.Collections.Generic;
using GenericEnums;
using TMPro;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    #region Public Types

    #endregion Public Types


    #region Public Variables

    #endregion Public Variables


    #region Public Methods

    #endregion Public Methods


    #region Inspector Variables

    [SerializeField] private TextMeshProUGUI _CurrentTurnText;
    [SerializeField] private GameSettings _GameSettings;
    
    #endregion Inspector Variables


    #region Unity Methods

    private void OnEnable()
    {
        TurnManager.TurnChanged += OnTurnChanged;
        GridFigure.FigureMoveAction += OnFigureMove;
    }

    private void OnDisable()
    {
        TurnManager.TurnChanged -= OnTurnChanged;
        GridFigure.FigureMoveAction -= OnFigureMove;
    }

    #endregion Unity Methods


    #region Private Variables

    private EConflictSide _CurrentConflictSide;
    
    #endregion Private Variables


    #region Private Methods

    private void OnTurnChanged(EConflictSide currentTurn)
    {
        _CurrentConflictSide = currentTurn;
        _CurrentTurnText.text = "Current turn: " + currentTurn + " moves left: " + PlayerManager.GetPlayer(currentTurn).movesLeft;
        _CurrentTurnText.color = _GameSettings.GetConflictSideColor(currentTurn);
    }

    private void OnFigureMove(GridFigure movedFigure)
    {
        _CurrentTurnText.text = "Current turn: " + _CurrentConflictSide + " moves left: " + PlayerManager.GetPlayer(_CurrentConflictSide).movesLeft;
    }

    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
