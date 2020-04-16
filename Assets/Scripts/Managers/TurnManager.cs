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
        _MovesInCurrentTurnCount = 0;
        TurnChanged?.Invoke(conflictSide);
    }

    public void NextTurn()
    {
        CurrentTurn++;
        _MovesInCurrentTurnCount = 0;
        
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


    #region Unity Methods
    
    #endregion Unity Methods


    #region Private Variables

    private int _MovesInCurrentTurnCount;
    
    #endregion Private Variables


    #region Private Methods

    private void OnFigureMove(GridFigure gridFigure)
    {
        _MovesInCurrentTurnCount++;

        if (_MovesInCurrentTurnCount >= PlayerManager.GetPlayer(gridFigure.conflictSide).GetGridFiguresCount())
        {
            NextTurn();
        }
    }
    
    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
