using System;
using System.Collections;
using System.Collections.Generic;
using GenericEnums;
using UnityEngine;
using UnityEngine.Events;

public class GridFigure : MonoBehaviour, IAmMemorized
{
    #region Public Types
    
    private struct GridFigureMementoData : IMementoData
    {
        private Vector3 Position;
        private int FigureStrength;
        private bool MadeMoveThisTurn;
        private HexCell myHexCell;
        private bool enabled;
        private GenericEnums.ESelectionState selectionState;

        public void Save(IAmMemorized memorizedObject)
        {
            if (memorizedObject is GridFigure gridFigure)
            {
                Position = gridFigure.transform.position;
                MadeMoveThisTurn = gridFigure.MadeMoveThisTurn;
                FigureStrength = gridFigure.FigureStrength;
                myHexCell = gridFigure._MyHexCell;
                enabled = gridFigure.gameObject.activeSelf;
                selectionState = gridFigure.selectionState;
            }
        }

        public void Load(IAmMemorized memorizedObject)
        {
            if (memorizedObject is GridFigure gridFigure)
            {
                gridFigure.transform.position = Position;
                gridFigure.MadeMoveThisTurn = MadeMoveThisTurn;
                gridFigure.FigureStrength = FigureStrength;
                gridFigure._MyHexCell = myHexCell;
                gridFigure.transform.parent = gridFigure._MyHexCell.transform;
                gridFigure.selectionState = selectionState;

                if (enabled)
                {
                    if (!gridFigure.gameObject.activeSelf)
                    {
                        gridFigure.gameObject.SetActive(true);
                        FigureRevoked?.Invoke(gridFigure);
                    }
                }
                else
                {
                    if (gridFigure.gameObject.activeSelf)
                    {
                        gridFigure.gameObject.SetActive(false);
                    }
                }
                
                gridFigure._MyStats.UpdateStats();
            }
        }
    }
    
    #endregion Public Types


    #region Public Variables
    
    public static bool FigureCurrentlyMoving;

    public IMementoData MyMementoData { get; set; }

    public bool MadeMoveThisTurn { get; private set; }
    
    public static UnityAction<GridFigure> FigureMoveAction;
    public static UnityAction<GridFigure> FigureDestroyed;
    public static UnityAction<GridFigure> FigureRevoked;
    
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

    public virtual void Merge(GridFigure allyFigure)
    {
        IncreaseStrength(allyFigure.FigureStrength);
        allyFigure.Destroy();
    }

    public virtual void Attack(GridFigure opponent)
    {
        var damageFromOpponent = opponent.FigureStrength;

        //TEMP SOLUTION
        if (damageFromOpponent == FigureStrength)
        {
            if(UnityEngine.Random.value >= 0.5f)
            {
                opponent.GetDamage(FigureStrength);
                GetDamage(FigureStrength - 1);
            }
            else
            {
                opponent.GetDamage(opponent.FigureStrength - 1);
                GetDamage(damageFromOpponent);
            }

            return;
        }
        
        opponent.GetDamage(FigureStrength);
        GetDamage(damageFromOpponent);
    }

    public virtual void GetDamage(int damage)
    {
        FigureStrength -= damage;
        _MyStats.UpdateStats();
        
        if (FigureStrength <= 0)
        {
            Destroy();
        }
    }

    public virtual void Destroy()
    {
        //transform.parent = null;
        FigureDestroyed?.Invoke(this);
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    public virtual void IncreaseStrength(int customValue = 0)
    {
        if (customValue == 0)
        {
            FigureStrength += FigureStrengthIncreaseRate;    
        }
        else
        {
            FigureStrength += customValue;
        }
        
        _MyStats.UpdateStats();
    }
    
    public void CreateMementoData()
    {
        if (MyMementoData == null)
        {
            MyMementoData = new GridFigureMementoData();
        }
    }

    #endregion Public Methods


    #region Inspector Variables

    [Header("Settings")]
    
    [SerializeField] public int DefaultMovementRange = 2;
    [SerializeField] public int FigureStrength = 16;

    [SerializeField] public int FigureStrengthIncreaseRate = 16;

    [Header("Dependencies")] 
    
    [SerializeField] private GameSettings _GameSettings;
    [SerializeField] private SpriteRenderer _MySpriteRenderer;
    [SerializeField] private FigureStats _MyStats;

    #endregion Inspector Variables


    #region Unity Methods

    private void OnEnable()
    {
        HexGrid.SelectCellAction += OnHexCellSelected;
        TurnManager.TurnsReset += ResetMovementStatus;
    }

    private void OnDisable()
    {
        HexGrid.SelectCellAction -= OnHexCellSelected;
        TurnManager.TurnsReset -= ResetMovementStatus;
    }

    private void Start()
    {
        _MyStats.UpdateStats();
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
        if (MadeMoveThisTurn || FigureCurrentlyMoving)
            return;
        
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

    private void ClaimTerritory(HexCell hexCell)
    {
        if (hexCell.currentlyHeldFigure == this)
        {
            hexCell.SetConflictSide(conflictSide);

            var allNeighbours = hexCell.neighbourCells;

            for (int i = 0; i < 6; i++)
            {
                if (allNeighbours[i] != null)
                {
                    if (allNeighbours[i].cellType != HexCell.ECellType.City && allNeighbours[i].currentlyHeldFigure == null)
                    {
                        allNeighbours[i].SetConflictSide(conflictSide);
                    }
                }
            }   
        }
    }

    private void ResetMovementStatus()
    {
        MadeMoveThisTurn = false;
    }

    #endregion Private Methods


    #region Coroutines

    private IEnumerator MoveFigureCoroutine(HexCell hexCell)
    {
        FigureCurrentlyMoving = true;
        
        transform.parent = null;
        _MyHexCell = hexCell;
        
        Deselect();
        
        while (Math.Abs(transform.position.x - hexCell.transform.position.x) > 0.05f ||
               Math.Abs(transform.position.y - hexCell.transform.position.y) > 0.05f)
        {
            transform.position =
                Vector2.SmoothDamp(transform.position, hexCell.transform.position, ref _MovingVelocity, 0.1f);

            yield return new WaitForEndOfFrame();
        }
        
        transform.parent = hexCell.transform;
        MadeMoveThisTurn = true;
        
        if (hexCell.currentlyHeldFigure != null && hexCell.currentlyHeldFigure != this)
        {
            if (hexCell.currentlyHeldFigure.conflictSide != conflictSide)
            {
                Attack(hexCell.currentlyHeldFigure);   
            }
            else
            {
                Merge(hexCell.currentlyHeldFigure);
            }
        }
        
        ClaimTerritory(hexCell);
        hexCell.Deselect();
        
        _MoveCoroutine = null;
        
        FigureMoveAction?.Invoke(this);

        FigureCurrentlyMoving = false;
    }
    
    #endregion Coroutines
}
