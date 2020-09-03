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

    public GameSettings GameSettings;
    public SpriteRenderer MushroomHatSprite;

    #endregion Public Variables


    #region Public Methods

    public void UpdateMushroomHatColor()
    {
        SetMushroomHatColor(GameSettings.GetConflictSideColor(_MyHexCell.conflictSide));  
    }
    
    #endregion Public Methods


    #region Inspector Variables

    #endregion Inspector Variables


    #region Unity Methods

    private void OnEnable()
    {
        TurnManager.TurnsReset += OnTurnsReset;
        GridFigure.FigureMoveAction += OnFigureMoved;
    }

    private void OnDisable()
    {
        TurnManager.TurnsReset -= OnTurnsReset;
        GridFigure.FigureMoveAction -= OnFigureMoved;
    }

    private void Awake()
    {
        _MyHexCell = GetComponentInParent<HexCell>();
    }

    private void Start()
    {
        SetMushroomHatColor(GameSettings.GetConflictSideColor(_MyHexCell.conflictSide));
    }

    #endregion Unity Methods


    #region Private Variables

    private HexCell _MyHexCell;
    
    #endregion Private Variables


    #region Private Methods

    private void OnFigureMoved(GridFigure movedFigure)
    {
        if (MushroomHatSprite.color != GameSettings.GetConflictSideColor(_MyHexCell.conflictSide))
        {
            UpdateMushroomHatColor();
        }
    }

    private void SetMushroomHatColor(Color color)
    {
        MushroomHatSprite.color = color;
    }

    private Color GetMushroomHatColor()
    {
        return MushroomHatSprite.color;
    }

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
        var newGridFigureObject = Instantiate(GameSettings.firstTierGridFigure, _MyHexCell.transform, true);
        newGridFigureObject.transform.position = _MyHexCell.transform.position;

        var newGridFigure = newGridFigureObject.GetComponent<GridFigure>();
        newGridFigure.SetHexCell(_MyHexCell);
        newGridFigure.SetConflictSide(_MyHexCell.conflictSide);
                
        PlayerManager.GetPlayer(_MyHexCell.conflictSide).AddGridFigure(newGridFigure);
    }

    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
