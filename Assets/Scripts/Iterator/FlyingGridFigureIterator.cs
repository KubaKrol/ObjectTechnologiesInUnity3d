using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingGridFigureIterator : IGridFigureIterator
{
#region Inspector Variables
    //These are variables that should be set in the Inspector - Use [SerializeField] or [ShowInInspector]
    //Can be public or private.

#endregion Inspector Variables


#region Unity Methods
    //Methods from MonoBehaviour.
    //OnEnable(), OnDisable(), Awake(), Start(), Update() etc...

#endregion UnityMethods


#region Public Variables
    //Variables accessible from every other script referencing this class.
    //These variables should not be visible in Inspector and should be hidden by using [HideInInspector] or [ReadOnly]
    
#endregion Public Variables


#region Public Methods

    public FlyingGridFigureIterator()
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
            if (gridFigure is FlyingFigure)
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
