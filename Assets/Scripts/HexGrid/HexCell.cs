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
        City
    }

    public enum ECurrentState
    {
        Idle,
        Selected
    }
    
    #endregion Public Types


    #region Public Variables

    public ECellType cellType;
    public ECurrentState currentState;
    
    #endregion Public Variables


    #region Public Methods

    public void Select()
    {
        mySelectionHighlightObject.SetActive(true);
        currentState = ECurrentState.Selected;
    }

    public void Deselect()
    {
        mySelectionHighlightObject.SetActive(false);
        currentState = ECurrentState.Idle;
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
        currentState = ECurrentState.Idle;
    }
    
    private void Start()
    {
        switch (cellType)
        {
            case ECellType.Field:
                _MySpriteRenderer.color = cellSettings.fieldColor;
                break;
            
            case ECellType.Water:
                _MySpriteRenderer.color = cellSettings.waterColor;
                break;
            
            case ECellType.Forest:
                _MySpriteRenderer.color = cellSettings.forestColor;
                break;
            
            case ECellType.City:
                _MySpriteRenderer.color = cellSettings.cityColor;
                break;
            
            default:
                _MySpriteRenderer.color = cellSettings.fieldColor;
                break;
        }
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
