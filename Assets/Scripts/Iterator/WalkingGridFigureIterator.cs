using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingGridFigureIterator : IGridFigureIterator
{
#region Public Methods

    public WalkingGridFigureIterator()
    {
        UpdateCache();
    }

    public GridFigure GetNext()
    {
        if (HasMore())
        {
            _CurrentPosition++;
        }
        
        return _Cache[_CurrentPosition];
    }

    public bool HasMore()
    {
        if (_CurrentPosition < _Cache.Count)
        {
            return true;
        }

        return false;
    }
    
    public void UpdateCache()
    {
        if (_Cache == null)
        {
            _Cache = new List<GridFigure>();
        }
        else
        {
            _Cache.Clear();
            _CurrentPosition = 0;
        }
        
        foreach (var gridFigure in FiguresManager.AllFigures)
        {
            if (gridFigure is WalkingFigure)
            {
                _Cache.Add(gridFigure);
            }
        }
    }

#endregion Public Methods


#region Private Variables

    private List<GridFigure> _Cache;
    private int _CurrentPosition;

#endregion Private Variables


#region Private Methods

    
#endregion Private Methods


#region Coroutines

//IEnumerators<>


#endregion Coroutines


#region Public Types

//enums, structs etc...


#endregion Public Types
}
