using System.Collections.Generic;

public static class FiguresManager
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

    public static List<GridFigure> AllFigures = new List<GridFigure>();

#endregion Public Variables


#region Public Methods

    public static void AddFigure(GridFigure figure)
    {
        if (!AllFigures.Contains(figure))
        {
            AllFigures.Add(figure);
        }
    }

    public static void RemoveFigure(GridFigure figure)
    {
        if (AllFigures.Contains(figure))
        {
            AllFigures.Remove(figure);
        }
    }

    public static IGridFigureIterator GetWalkingFigureIterator()
    {
        return new WalkingGridFigureIterator();
    }

    public static IGridFigureIterator GetFlyingFigureIterator()
    {
        return new FlyingGridFigureIterator();
    }

#endregion Public Methods


#region Private Variables

//Private variables, accessible only from this class.


#endregion Private Variables


#region Private Methods

//Private methods, accessible only from this class.


#endregion Private Methods


#region Coroutines

//IEnumerators<>


#endregion Coroutines


#region Public Types

//enums, structs etc...


#endregion Public Types
}
