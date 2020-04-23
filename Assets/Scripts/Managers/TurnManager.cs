using System.Collections;
using System.Collections.Generic;
using GenericEnums;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager
{
    #region Public Types

    #endregion Public Types


    #region Public Variables

    public static UnityAction<EConflictSide> TurnChanged;
    public static UnityAction TurnsReset;
    
    public EConflictSide CurrentTurn { get; private set; }
    
    #endregion Public Variables


    #region Public Methods

    public TurnManager()
    {
        GridFigure.FigureMoveAction += OnFigureMove;
    }
    
    public void InitializeFirstTurn()
    {
        SetTurn(EConflictSide.Player_1);
        TurnsReset?.Invoke();
        TurnChanged?.Invoke(CurrentTurn);
    }
    
    public void SetTurn(EConflictSide conflictSide)
    {
        CurrentTurn = conflictSide;
    }

    public void NextTurn()
    {
        CurrentTurn++;
        
        if ((int) CurrentTurn > 4)
        {
            CurrentTurn = (EConflictSide)1;
            TurnsReset?.Invoke();
        }
        
        TurnChanged?.Invoke(CurrentTurn);
    }

    #endregion Public Methods


    #region Inspector Variables

    #endregion Inspector Variables


    #region Private Variables
    
    #endregion Private Variables


    #region Private Methods

    private void OnFigureMove(GridFigure gridFigure)
    {
        if (PlayerManager.GetPlayer(CurrentTurn).movesLeft <= 0)
        {
            NextTurn();
        }
    }
    
    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
