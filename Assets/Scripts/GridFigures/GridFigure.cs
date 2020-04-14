using System;
using System.Collections;
using UnityEngine;

public class GridFigure : MonoBehaviour
{
    #region Public Types

    #endregion Public Types


    #region Public Variables

    public GenericEnums.ESelectionState selectionState;
    public GenericEnums.EConflictSide conflictSide;

    #endregion Public Variables


    #region Public Methods

    public virtual void Select()
    {
        selectionState = GenericEnums.ESelectionState.Selected;
        ShowMovementRange(true);
    }

    public virtual void Deselect()
    {
        selectionState = GenericEnums.ESelectionState.Idle;
        ShowMovementRange(false);
    }
    
    public virtual void MoveFigure(HexCell hexCell)
    {
        if (_MoveCoroutine == null)
        {
            _MoveCoroutine = StartCoroutine(MoveFigureCoroutine(hexCell));   
        }
    }

    public virtual void ShowMovementRange(bool active)
    {
        _CurrentMovementRange = DefaultMovementRange;

        if (_MyHexCell.cellType == HexCell.ECellType.Forest)
        {
            _CurrentMovementRange /= 2;
        }
        
        var currentHexCoordinates = _MyHexCell.coordinates;
        
        for(var i = currentHexCoordinates.X - DefaultMovementRange; i <= currentHexCoordinates.X + DefaultMovementRange; i++)
        {
            for (var j = currentHexCoordinates.Y - DefaultMovementRange; j <= currentHexCoordinates.Y + DefaultMovementRange; j++)
            {
                var targetHexCell = HexGrid.GetCell(new HexCoordinates(i, j));

                if (targetHexCell != null && HexMetrics.Distance(_MyHexCell, targetHexCell) <= _CurrentMovementRange)
                {
                    targetHexCell.ShowMovementAvailability(active);
                }
            }
        }
    }

    #endregion Public Methods


    #region Inspector Variables

    [SerializeField] public int DefaultMovementRange = 2;
    [SerializeField] public int FigureStrength = 16;
    [SerializeField] public int FigureMorals = 8;

    #endregion Inspector Variables


    #region Unity Methods

    private void OnEnable()
    {
        HexGrid.SelectCellAction += OnHexCellSelected;
    }

    private void OnDisable()
    {
        HexGrid.SelectCellAction -= OnHexCellSelected;
    }

    private void Start()
    {
        MoveFigure(HexGrid.GetCell(new HexCoordinates(5, 5)));
    }

    #endregion Unity Methods


    #region Private Variables

    protected HexCell _MyHexCell;
    
    protected Coroutine _MoveCoroutine;
    protected Vector2 _MovingVelocity;

    protected int _CurrentMovementRange;
    
    #endregion Private Variables


    #region Private Methods

    private void OnHexCellSelected(HexCell hexCell)
    {
        if (hexCell == _MyHexCell)
        {
            switch (selectionState)
            {
                case GenericEnums.ESelectionState.Idle:
                    Select();
                    break;
                
                case GenericEnums.ESelectionState.Selected:
                    Deselect();
                    break;
                
                default:
                    Select();
                    break;
            }
        }
        else
        {
            if (selectionState == GenericEnums.ESelectionState.Selected)
            {
                if (HexMetrics.Distance(_MyHexCell, hexCell) <= _CurrentMovementRange && hexCell.showingMovementAvailability)
                {
                    MoveFigure(hexCell);
                }
            }
            
            Deselect();
        }
    }

    #endregion Private Methods


    #region Coroutines

    private IEnumerator MoveFigureCoroutine(HexCell hexCell)
    {
        transform.parent = null;
        
        while (Math.Abs(transform.position.x - hexCell.transform.position.x) > 0.05f ||
               Math.Abs(transform.position.y - hexCell.transform.position.y) > 0.05f)
        {
            transform.position =
                Vector2.SmoothDamp(transform.position, hexCell.transform.position, ref _MovingVelocity, 0.1f);

            yield return new WaitForEndOfFrame();
        }

        _MyHexCell = hexCell;
        transform.parent = hexCell.transform;

        _MoveCoroutine = null;
    }
    
    #endregion Coroutines
}
