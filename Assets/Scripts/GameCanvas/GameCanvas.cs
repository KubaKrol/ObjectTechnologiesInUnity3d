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

    [SerializeField] private TextMeshProUGUI currentTurnText;
    
    #endregion Inspector Variables


    #region Unity Methods

    private void OnEnable()
    {
        TurnManager.TurnChanged += OnTurnChanged;
    }

    private void OnDisable()
    {
        TurnManager.TurnChanged -= OnTurnChanged;
    }

    #endregion Unity Methods


    #region Private Variables

    #endregion Private Variables


    #region Private Methods

    private void OnTurnChanged(EConflictSide currentTurn)
    {
        currentTurnText.text = currentTurn.ToString();
    }
    
    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
