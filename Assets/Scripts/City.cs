using System;
using System.Collections;
using System.Collections.Generic;
using GenericEnums;
using UnityEngine;

public class City : MonoBehaviour
{
    #region Public Types

    #endregion Public Types


    #region Public Variables

    #endregion Public Variables


    #region Public Methods

    public GameSettings GameSettings;
    
    #endregion Public Methods


    #region Inspector Variables

    #endregion Inspector Variables


    #region Unity Methods

    private void OnEnable()
    {
        TurnManager.TurnsReset += OnTurnsReset;
    }

    private void OnDisable()
    {
        TurnManager.TurnsReset -= OnTurnsReset;
    }

    private void Awake()
    {
        _MyHexCell = GetComponentInParent<HexCell>();
    }

    #endregion Unity Methods


    #region Private Variables

    private HexCell _MyHexCell;
    
    #endregion Private Variables


    #region Private Methods

    private void OnTurnsReset()
    {
        if (_MyHexCell.conflictSide != EConflictSide.Independent)
        {
            if (_MyHexCell.currentlyHeldFigure != null)
            {
                _MyHexCell.currentlyHeldFigure.IncreaseStrength();
            }
            else
            {
                CreateNewGridFigure();
            }   
        }
    }

    private void CreateNewGridFigure()
    {
        var newGridFigureObject = Instantiate(GameSettings.firstTierGridFigure);
        newGridFigureObject.transform.position = _MyHexCell.transform.position;
        newGridFigureObject.transform.parent = _MyHexCell.transform;

        var newGridFigure = newGridFigureObject.GetComponent<GridFigure>();
        newGridFigure.SetHexCell(_MyHexCell);
        newGridFigure.SetConflictSide(_MyHexCell.conflictSide);
                
        PlayerManager.GetPlayer(_MyHexCell.conflictSide).AddGridFigure(newGridFigure);
    }

    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
