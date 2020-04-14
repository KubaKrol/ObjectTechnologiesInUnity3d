using System;
using System.Collections;
using UnityEngine;

public class GridFigure : MonoBehaviour
{
    #region Public Types

    public enum ESelectionState
    {
        Idle,
        Selected
    }
    
    #endregion Public Types


    #region Public Variables

    public ESelectionState selectionState;
    
    #endregion Public Variables


    #region Public Methods

    public virtual void Select()
    {
        selectionState = ESelectionState.Selected;
        ShowMovementRange(true);
    }

    public virtual void Deselect()
    {
        selectionState = ESelectionState.Idle;
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
        var currentHexCoordinates = _MyHexCell.coordinates;
        
        for(int i = currentHexCoordinates.X - movementRange; i <= currentHexCoordinates.X + movementRange; i++)
        {
            for (int j = currentHexCoordinates.Y - movementRange; j <= currentHexCoordinates.Y + movementRange; j++)
            {
                var targetHexCell = HexGrid.GetCell(new HexCoordinates(i, j));

                if (targetHexCell != null && HexMetrics.Distance(_MyHexCell, targetHexCell) <= 2)
                {
                    targetHexCell.ShowMovementAvailability(active);
                }
            }
        }
    }

    #endregion Public Methods


    #region Inspector Variables

    [SerializeField] public int movementRange = 2;
    
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

    private HexCell _MyHexCell;
    
    private Coroutine _MoveCoroutine;
    private Vector2 _MovingVelocity;
    
    #endregion Private Variables


    #region Private Methods

    private void OnHexCellSelected(HexCell hexCell)
    {
        if (hexCell == _MyHexCell)
        {
            Select();
        }
        else
        {
            if (selectionState == ESelectionState.Selected)
            {
                if (HexMetrics.Distance(_MyHexCell, hexCell) <= 2)
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
