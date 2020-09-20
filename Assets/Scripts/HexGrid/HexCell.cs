using System.Runtime.InteropServices;
using GenericEnums;
using UnityEngine;

public class HexCell : MonoBehaviour, IAmMemorized
{
    #region Public Types

    public enum ECellType
    {
        Field,
        Water,
        DeepWater,
        Forest,
        City,
        Mountains
    }

    public enum ELocomotionState
    {
        Blocked,
        Walkable,
        Swimmable,
        Flyable
    }
    
    private struct HexCellMementoData : IMementoData
    {
        private GridFigure currentlyHeldGridFigure;
        private GenericEnums.EConflictSide conflictSide;

        public void Save(IAmMemorized memorizedObject)
        {
            if (memorizedObject is HexCell hexCell)
            {
                currentlyHeldGridFigure = hexCell.currentlyHeldFigure;
                conflictSide = hexCell.conflictSide;
            }
        }

        public void Load(IAmMemorized memorizedObject)
        {
            if (memorizedObject is HexCell hexCell)
            {
                if (currentlyHeldGridFigure == null)
                {
                    hexCell.ShowFigureAvailabilityHighlight(false);
                }
                else
                {
                    hexCell._CurrentFigure = currentlyHeldGridFigure;   
                }

                hexCell.conflictSide = conflictSide;
                hexCell.CheckBorders();
                
                foreach (var neighbourHexCell in hexCell.neighbourCells)
                {
                    if(neighbourHexCell == null)
                        return;
                    
                    neighbourHexCell.CheckBorders();
                }

                if (hexCell.cellType == ECellType.City)
                {
                    hexCell.myCity.UpdateMushroomHatColor();
                }
            }
        }
    }

    #endregion Public Types


    #region Public Variables

    public ECellType cellType { get; private set; }
    public GenericEnums.ESelectionState selectionState { get; private set; }
    public ELocomotionState locomotionState { get; private set; }
    public GenericEnums.EConflictSide conflictSide { get; private set; }
    public IMementoData MyMementoData { get; set; }
    
    public HexCell[] neighbourCells
    {
        get
        {
            if (_MyNeighbourCells == null)
            {
                _MyNeighbourCells = HexMetrics.GetAllNeighbours(this);
            }

            return _MyNeighbourCells;
        }
    }

    public float myNoiseValue { get; private set; }
    
    public int orderInRenderingLayer { get; private set; }
    
    public bool showingMovementAvailability { get; private set; }

    public GridFigure currentlyHeldFigure
    {
        get
        {
            _CurrentFigure = GetComponentInChildren<GridFigure>();
            return _CurrentFigure;
        }
    }

    public City myCity
    {
        get
        {
            if (_MyCity == null)
            {
                _MyCity = GetComponent<City>();
            }

            return _MyCity;
        }
    }
    
    #endregion Public Variables


    #region Public Methods

    public void Select()
    {
        mySelectionHighlightObject.SetActive(true);
        selectionState = GenericEnums.ESelectionState.Selected;
    }

    public void Deselect()
    {
        mySelectionHighlightObject.SetActive(false);
        selectionState = GenericEnums.ESelectionState.Idle;
    }

    public void ShowMovementAvailability(bool active)
    {
        myMovementRangeHighlightObject.SetActive(active);
        showingMovementAvailability = active;
    }

    public void SetCellType(ECellType cellType)
    {
        this.cellType = cellType;
        
        switch (cellType)
        {
            case ECellType.Field:
                _MySpriteRenderer.sprite = cellSettings.fieldSprite;
                locomotionState = ELocomotionState.Walkable;
                break;
            
            case ECellType.Water:
                _MySpriteRenderer.sprite = cellSettings.waterSprite;
                locomotionState = ELocomotionState.Swimmable;
                break;
            
            case ECellType.DeepWater:
                _MySpriteRenderer.sprite = cellSettings.deepWaterSprite;
                locomotionState = ELocomotionState.Swimmable;
                break;
            
            case ECellType.Forest:
                
                if (myNoiseValue < 0.65f)
                {
                    _MySpriteRenderer.sprite = cellSettings.forestSprite;
                }
                else
                {
                    _MySpriteRenderer.sprite = cellSettings.denseForestSprite;   
                }

                locomotionState = ELocomotionState.Walkable;
                break;
            
            case ECellType.City:
                _MySpriteRenderer.sprite = cellSettings.citySprite;
                locomotionState = ELocomotionState.Walkable;
                InstantiateCity(_GameSettings);
                break;
            
            case ECellType.Mountains:
               
                if (myNoiseValue < 0.85f)
                {
                    _MySpriteRenderer.sprite = cellSettings.mountainsSprite;   
                }
                else
                {
                    _MySpriteRenderer.sprite = cellSettings.highMountainSprite;   
                }
                
                locomotionState = ELocomotionState.Blocked;
                break;
            
            default:
                _MySpriteRenderer.sprite = cellSettings.fieldSprite;
                locomotionState = ELocomotionState.Walkable;
                break;
        }
    }
    
    public void SetOrderInLayer(int orderInLayer)
    {
        if (cellType == ECellType.Water)
            orderInLayer -= 2;
        
        orderInRenderingLayer = orderInLayer;
        _MySpriteRenderer.sortingOrder = orderInLayer;
    }

    public void SetConflictSide(EConflictSide conflictSide)
    {
        this.conflictSide = conflictSide;
        CheckBorders();
    }

    public void CheckBorders()
    {
        if (conflictSide != EConflictSide.Independent)
        {
            for (int i = 0; i < 6; i++)
            {
                if (neighbourCells[i] != null)
                {
                    _BorderSpriteRenderers[i].gameObject.SetActive(neighbourCells[i].conflictSide != conflictSide);
                    var newColor = _GameSettings.GetConflictSideColor(conflictSide);
                    newColor.a = 0.6f;
                    _BorderSpriteRenderers[i].color = newColor;
                }
            }
        }
        else
        {
            foreach (var border in _BorderSpriteRenderers)
            {
                border.gameObject.SetActive(false);
            }
        }
    }

    public void SetNoiseValue(float value)
    {
        myNoiseValue = value;
    }

    public void ShowFigureAvailabilityHighlight(bool active)
    {
        gridFigureAvailabilityHighlightObject.gameObject.SetActive(active);
    }
    
    public void CreateMementoData()
    {
        if (MyMementoData == null)
        {
            MyMementoData = new HexCellMementoData();
        } 
    }
    
    #endregion Public Methods


    #region Inspector Variables

    [Header("Settings")]
    
    [SerializeField] public HexCoordinates coordinates;

    [SerializeField] private HexCellSettings cellSettings;
    [SerializeField] private GameSettings gameSettings;

    [SerializeField] public GameObject mySelectionHighlightObject;
    [SerializeField] public GameObject myMovementRangeHighlightObject;
    [SerializeField] public GameObject gridFigureAvailabilityHighlightObject;

    [Header("Dependencies")]
    
    [SerializeField] private GameSettings _GameSettings;

    [SerializeField] private SpriteRenderer[] _BorderSpriteRenderers;

    #endregion Inspector Variables


    #region Unity Methods

    private void OnEnable()
    {
        HexGrid.SelectCellAction += SelectCellAction;
        GridFigure.FigureMoveAction += OnFigureMoved;
        TurnManager.TurnUndone += OnTurnUndone;
    }

    private void OnDisable()
    {
        HexGrid.SelectCellAction -= SelectCellAction;
        GridFigure.FigureMoveAction -= OnFigureMoved;
        TurnManager.TurnUndone -= OnTurnUndone;
    }
    
    private void Awake()
    {
        _MySpriteRenderer = GetComponent<SpriteRenderer>();
        
        selectionState = GenericEnums.ESelectionState.Idle;
        conflictSide = GenericEnums.EConflictSide.Independent;
    }

    #endregion Unity Methods


    #region Private Variables

    private SpriteRenderer _MySpriteRenderer;
    private GridFigure _CurrentFigure;
    private HexCell[] _MyNeighbourCells;
    private City _MyCity;

    #endregion Private Variables


    #region Private Methods

    private void SelectCellAction(HexCell hexCell, EConflictSide conflictSide)
    {
        if (hexCell == this)
        {
            Select();
        }
        else
        {
            Deselect();
        }
    }

    private void InstantiateCity(GameSettings gameSettings)
    {
        var city = gameObject.AddComponent<City>();
        city.gameObject.AddComponent<Memento>();
        city.GameSettings = gameSettings;

        var mushroomHat = Instantiate(cellSettings.mushroomHatPrefab, transform);
        city.MushroomHatSprite = mushroomHat.GetComponentInChildren<SpriteRenderer>();
    }

    private void OnFigureMoved(GridFigure figure)
    {
        CheckBorders();
    }

    private void OnTurnUndone(EConflictSide currentTurn)
    {
        if (currentlyHeldFigure != null)
        {
            if (currentTurn == currentlyHeldFigure.conflictSide)
            {
                if (!currentlyHeldFigure.MadeMoveThisTurn)
                {
                    ShowFigureAvailabilityHighlight(true);   
                }
            }
        }
    }

    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
