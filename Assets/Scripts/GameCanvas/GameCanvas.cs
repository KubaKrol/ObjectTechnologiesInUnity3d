using System;
using GenericEnums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Button _UndoTurnButton;

    #endregion Inspector Variables


    #region Unity Methods

    private void OnEnable()
    {
        TurnManager.EndTurn += OnTurnChanged;
        TurnManager.TurnUndone += OnTurnUndone;
        GridFigure.FigureMoveAction += OnFigureMove;
    }

    private void OnDisable()
    {
        TurnManager.EndTurn -= OnTurnChanged;
        TurnManager.TurnUndone -= OnTurnUndone;
        GridFigure.FigureMoveAction -= OnFigureMove;
    }

    private void Awake()
    {
        _UndoTurnButton.interactable = false;
    }

    #endregion Unity Methods


    #region Private Variables

    private static TextMeshProUGUI _StaticDebugText;
    
    private EConflictSide _CurrentConflictSide;

    private bool _TurnUndone;
    
    #endregion Private Variables


    #region Private Methods

    private void OnTurnChanged(EConflictSide currentTurn)
    {
        _CurrentConflictSide = currentTurn;
        _CurrentTurnText.text = "Current turn: " + currentTurn + " moves left: " + PlayerManager.GetPlayer(currentTurn).movesLeft;
        _CurrentTurnText.color = _GameSettings.GetConflictSideColor(currentTurn);
        _UndoTurnButton.interactable = false;
        _TurnUndone = false;
    }

    private void OnTurnUndone(EConflictSide currentTurn)
    {
        _CurrentTurnText.text = "Current turn: " + _CurrentConflictSide + " moves left: " + PlayerManager.GetPlayer(_CurrentConflictSide).movesLeft;
        _UndoTurnButton.interactable = false;
        _TurnUndone = true;
    }

    private void OnFigureMove(GridFigure movedFigure)
    {
        _CurrentTurnText.text = "Current turn: " + _CurrentConflictSide + " moves left: " + PlayerManager.GetPlayer(_CurrentConflictSide).movesLeft;
        
        if (!_UndoTurnButton.interactable && !_TurnUndone)
        {
            _UndoTurnButton.interactable = true;
        }
    }

    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
