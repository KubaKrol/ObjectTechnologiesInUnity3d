using UnityEngine;
using UnityEngine.Serialization;

public class HexCell : MonoBehaviour
{
    #region Public Types

    public enum ECellType
    {
        Field,
        Water,
        Forest,
        City,
        Mountains
    }

    public enum ESelectionState
    {
        Idle,
        Selected
    }

    public enum ELocomotionState
    {
        Blocked,
        Walkable,
        Swimmingable
    }

    public enum EConflictSide
    {
        Independent,
        Player_1,
        Player_2,
        Player_3,
        Player_4
    }
    
    #endregion Public Types


    #region Public Variables

    public ECellType cellType;
    public ESelectionState selectionState;
    public ELocomotionState locomotionState;
    public EConflictSide conflictSide;
    
    #endregion Public Variables


    #region Public Methods

    public void Select()
    {
        mySelectionHighlightObject.SetActive(true);
        selectionState = ESelectionState.Selected;
    }

    public void Deselect()
    {
        mySelectionHighlightObject.SetActive(false);
        selectionState = ESelectionState.Idle;
    }

    public void SetCellType(ECellType cellType)
    {
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
    
    #endregion Inspector Variables


    #region Unity Methods

    private void Awake()
    {
        _MySpriteRenderer = GetComponent<SpriteRenderer>();

        cellType = ECellType.Field;
        selectionState = ESelectionState.Idle;
        conflictSide = EConflictSide.Independent;
    }
    
    private void Start()
    {
       SetCellType(cellType);
    }
    
    #endregion Unity Methods


    #region Private Variables

    private SpriteRenderer _MySpriteRenderer;

    #endregion Private Variables


    #region Private Methods

    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
