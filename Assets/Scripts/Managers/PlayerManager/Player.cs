using System.Collections;
using System.Collections.Generic;
using GenericEnums;
using UnityEngine;

public class Player
{
    #region Public Types

    #endregion Public Types


    #region Public Variables

    public EConflictSide conflictSide { get; private set; }
    public List<GridFigure> gridFigures { get; private set; }
    
    #endregion Public Variables


    #region Public Methods

    public Player(EConflictSide conflictSide)
    {
        this.conflictSide = conflictSide;
    }

    public void AddGridFigure(GridFigure newGridFigure)
    {
        if (gridFigures == null)
        {
            gridFigures = new List<GridFigure>();
        }
        
        gridFigures.Add(newGridFigure);
    }

    public void RemoveGridFigure(GridFigure girdFigureToRemove)
    {
        if (gridFigures != null && gridFigures.Contains(girdFigureToRemove))
        {
            gridFigures.Remove(girdFigureToRemove);
        }
    }

    public int GetGridFiguresCount()
    {
        return gridFigures.Count;
    }

    #endregion Public Methods


    #region Inspector Variables

    #endregion Inspector Variables


    #region Unity Methods

    #endregion Unity Methods


    #region Private Variables

    #endregion Private Variables


    #region Private Methods

    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
    
}
