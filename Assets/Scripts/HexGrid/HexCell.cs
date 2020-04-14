using UnityEngine;

public class HexCell : MonoBehaviour
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
        Swimmingable
    }

    #endregion Public Types


    #region Public Variables

    public ECellType cellType;
    public GenericEnums.ESelectionState selectionState;
    public ELocomotionState locomotionState;
    public GenericEnums.EConflictSide conflictSide;
    
    public bool showingMovementAvailability { get; private set; }

    public GridFigure currentlyHeldFigure
    {
        get
        {
            _CurrentFigure = GetComponentInChildren<GridFigure>();
            return _CurrentFigure;
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
                _MySpriteRenderer.color = cellSettings.fieldColor;
                locomotionState = ELocomotionState.Walkable;
                break;
            
            case ECellType.Water:
                _MySpriteRenderer.color = cellSettings.waterColor;
                locomotionState = ELocomotionState.Swimmingable;
                break;
            
            case ECellType.DeepWater:
                _MySpriteRenderer.color = cellSettings.deepWaterColor;
                locomotionState = ELocomotionState.Swimmingable;
                break;
            
            case ECellType.Forest:
                _MySpriteRenderer.color = cellSettings.forestColor;
                locomotionState = ELocomotionState.Walkable;
                break;
            
            case ECellType.City:
                _MySpriteRenderer.color = cellSettings.cityColor;
                locomotionState = ELocomotionState.Walkable;
                break;
            
            case ECellType.Mountains:
                _MySpriteRenderer.color = cellSettings.mountainsColor;
                locomotionState = ELocomotionState.Blocked;
                break;
            
            default:
                _MySpriteRenderer.color = cellSettings.fieldColor;
                locomotionState = ELocomotionState.Walkable;
                break;
        }
    }
    
    #endregion Public Methods


    #region Inspector Variables

    [SerializeField] public HexCoordinates coordinates;

    [SerializeField] private HexCellSettings cellSettings;

    [SerializeField] public GameObject mySelectionHighlightObject;
    [SerializeField] public GameObject myMovementRangeHighlightObject;
    
    #endregion Inspector Variables


    #region Unity Methods

    private void OnEnable()
    {
        HexGrid.SelectCellAction += SelectCellAction;
    }

    private void OnDisable()
    {
        HexGrid.SelectCellAction -= SelectCellAction;
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

    #endregion Private Variables


    #region Private Methods

    private void SelectCellAction(HexCell hexCell)
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
    
    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
