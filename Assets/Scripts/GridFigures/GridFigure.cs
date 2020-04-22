using System;
using System.Collections;
using GenericEnums;
using UnityEngine;
using UnityEngine.Events;

public class GridFigure : MonoBehaviour
{
    #region Public Types
    
    #endregion Public Types


    #region Public Variables

    public static UnityAction<GridFigure> FigureMoveAction; 
    
    public GenericEnums.ESelectionState selectionState { get; private set; }
    public GenericEnums.EConflictSide conflictSide { get; private set; }

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

    public virtual void SetConflictSide(EConflictSide newConflictSide)
    {
        conflictSide = newConflictSide;
        _MySpriteRenderer.color = _GameSettings.GetConflictSideColor(conflictSide);
    }

    public virtual void SetHexCell(HexCell hexCell)
    {
        _MyHexCell = hexCell;
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

    public virtual void IncreaseStrength()
    {
        FigureStrength += FigureStrengthIncreaseRate;
    }

    #endregion Public Methods


    #region Inspector Variables

    [Header("Settings")]
    
    [SerializeField] public int DefaultMovementRange = 2;
    [SerializeField] public int FigureStrength = 16;
    [SerializeField] public int FigureMorals = 8;

    [SerializeField] public int FigureStrengthIncreaseRate = 16;

    [Header("Dependencies")] 
    
    [SerializeField] private GameSettings _GameSettings;
    [SerializeField] private SpriteRenderer _MySpriteRenderer;

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

    #endregion Unity Methods


    #region Private Variables

    protected HexCell _MyHexCell;
    
    protected Coroutine _MoveCoroutine;
    protected Vector2 _MovingVelocity;

    protected int _CurrentMovementRange;
    
    #endregion Private Variables


    #region Private Methods

    private void OnHexCellSelected(HexCell hexCell, EConflictSide conflictSide)
    {
        if (conflictSide == this.conflictSide)
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
                        Deselect();
                        MoveFigure(hexCell);
                    }
                
                    Deselect();
                }
            }   
        }
    }

    #endregion Private Methods


    #region Coroutines

    private IEnumerator MoveFigureCoroutine(HexCell hexCell)
    {
        _MyHexCell = hexCell;
        
        transform.parent = null;
        
        while (Math.Abs(transform.position.x - hexCell.transform.position.x) > 0.05f ||
               Math.Abs(transform.position.y - hexCell.transform.position.y) > 0.05f)
        {
            transform.position =
                Vector2.SmoothDamp(transform.position, hexCell.transform.position, ref _MovingVelocity, 0.1f);

            yield return new WaitForEndOfFrame();
        }
        
        transform.parent = hexCell.transform;
        _MyHexCell.Deselect();
        _MyHexCell.SetConflictSide(conflictSide);

        var allNewNeighbours = _MyHexCell.neighbourCells;

        for (int i = 0; i < 6; i++)
        {
            if (allNewNeighbours[i] != null)
            {
                if (allNewNeighbours[i].cellType != HexCell.ECellType.City)
                {
                    allNewNeighbours[i].SetConflictSide(conflictSide);
                }
            }
        }

        FigureMoveAction?.Invoke(this);
        
        _MoveCoroutine = null;
    }
    
    #endregion Coroutines
}
