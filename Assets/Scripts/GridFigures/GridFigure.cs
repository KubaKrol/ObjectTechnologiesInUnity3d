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

    public virtual void MoveFigure(HexCell hexCell)
    {
        if (_MoveCoroutine == null)
        {
            _MoveCoroutine = StartCoroutine(MoveFigureCoroutine(hexCell));   
        }
    }

    public virtual void Select()
    {
        selectionState = ESelectionState.Selected;
    }

    public virtual void Deselect()
    {
        selectionState = ESelectionState.Idle;
    }
    
    #endregion Public Methods


    #region Inspector Variables

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
            Deselect();
        }
    }
    
    #endregion Private Methods


    #region Coroutines

    private IEnumerator MoveFigureCoroutine(HexCell hexCell)
    {
        transform.parent = null;
        
        while (Math.Abs(transform.position.x - hexCell.transform.position.x) > 0.01f &&
               Math.Abs(transform.position.y - hexCell.transform.position.y) > 0.01f)
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
