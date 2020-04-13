using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour 
{
    #region Public Types

    #endregion Public Types


    #region Public Variables
    
    public Vector3 centerPosition;
    
    public float widthInWorldCoordinates
    {
        get
        {
            var worldWidth = 0f;
            
            for (var i = 0; i < width; i++)
            {
                worldWidth += HexMetrics.innerRadius * 2f;
            }

            worldWidth += HexMetrics.innerRadius;

            return worldWidth;
        }
    }

    public float heightInWorldCoordinates
    {
        get
        {
            var worldHeight = 0f;
            
            for (var i = 0; i < height; i++)
            {
                if (i % 2 == 0)
                {
                    worldHeight += HexMetrics.outerRadius * 2f;
                }
                else
                {
                    worldHeight += HexMetrics.innerRadius;
                }
            }

            return worldHeight;
        }
    }

    #endregion Public Variables


    #region Public Methods

    public void SelectCell(HexCell cellToSelect)
    {
        if (cellToSelect.currentState == HexCell.ECurrentState.Selected)
        {
            return;
        }
        else
        {
            foreach (var cell in _Cells)
            {
                cell.Deselect();
            }
            
            cellToSelect.Select();
        }
    }

    public void SelectCell(Vector2 selectionWorldPosition)
    {
        var smallestPositionDifference = Mathf.Infinity;
        HexCell cellToSelect = null;
        
        foreach (var cell in _Cells)
        {
            var xDifference = Mathf.Abs(cell.transform.localPosition.x - selectionWorldPosition.x);
            var yDifference = Mathf.Abs(cell.transform.localPosition.y - selectionWorldPosition.y);

            var differenceInTotal = xDifference + yDifference;
            
            if(differenceInTotal < smallestPositionDifference)
            {
                cellToSelect = cell;
                smallestPositionDifference = differenceInTotal;
            }
        }

        if (cellToSelect != null)
        {
            SelectCell(cellToSelect);
        }
    }

    #endregion Public Methods


    #region Inspector Variables

    [SerializeField] private GameInput _GameInput;
    
    [SerializeField] public HexCell cellPrefab;
    [SerializeField] public Text cellLabelPrefab;

    [SerializeField] public int width = 6;
    [SerializeField] public int height = 6;
    
    #endregion Inspector Variables


    #region Unity Methods
    
    private void Awake ()
    {
        _InputHandler = new HexGridInputHandler(_GameInput.currentInput, this);
        
        _GridCanvas = GetComponentInChildren<Canvas>();
        _Cells = new HexCell[height * width];
        
        CreateGrid();

        centerPosition = new Vector3(widthInWorldCoordinates / 2f - HexMetrics.innerRadius, heightInWorldCoordinates / 2f - HexMetrics.outerRadius, transform.position.z);
    }

    private void Start()
    {
    }

    private void Update()
    {
        _InputHandler.HandleInput();
    }

    #endregion Unity Methods


    #region Private Variables

    private HexGridInputHandler _InputHandler;    
    
    private HexCell[] _Cells;
    private Canvas _GridCanvas;

    private Vector3 _CenterPosition;
    
    #endregion Private Variables


    #region Private Methods

    private void CreateCell (int x, int y, int i, HexCell.ECellType cellType = HexCell.ECellType.Field) 
    {
        Vector3 position;
        position.x = (x + y * 0.5f - y / 2) * (HexMetrics.innerRadius * 2f);
        position.y = y * (HexMetrics.outerRadius * 1.5f);
        position.z = 0f;
        
        HexCell cell = _Cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, y);
        cell.cellType = cellType;
        
        Text label = Instantiate<Text>(cellLabelPrefab);
        label.rectTransform.SetParent(_GridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.y);
        label.text = cell.coordinates.ToStringOnSeparateLines();
    }

    private void CreateGrid()
    {
        for (int y = 0, i = 0; y < height; y++) 
        {
            for (int x = 0; x < width; x++) 
            {
                if (x == 1 && y == 1)
                {
                    CreateCell(x, y, i++, HexCell.ECellType.City);
                }
                else
                {
                    CreateCell(x, y, i++);   
                }
            }
        }
    }
    
    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}