using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour 
{
    #region Public Types

    #endregion Public Types


    #region Public Variables

    public static UnityAction<HexCell> SelectCellAction;
    
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

    public static HexCell GetCell(Vector2 selectionWorldPosition)
    {
        var smallestPositionDifference = Mathf.Infinity;
        HexCell cellToReturn = null;
        
        foreach (var cell in _Cells)
        {
            var xDifference = Mathf.Abs(cell.transform.localPosition.x - selectionWorldPosition.x);
            var yDifference = Mathf.Abs(cell.transform.localPosition.y - selectionWorldPosition.y);

            var differenceInTotal = xDifference + yDifference;
            
            if(differenceInTotal < smallestPositionDifference)
            {
                cellToReturn = cell;
                smallestPositionDifference = differenceInTotal;
            }
        }

        return cellToReturn != null ? cellToReturn : null;
    }

    public static HexCell GetCell(HexCoordinates coordinates)
    {
        if (_CellsDictionary.ContainsKey(coordinates) && _CellsDictionary[coordinates] != null)
        {
            return _CellsDictionary[coordinates];   
        }

        return null;
    }

    #endregion Public Methods


    #region Inspector Variables

    [SerializeField] private GameInput _GameInput;
    
    [SerializeField] public HexCell cellPrefab;
    [SerializeField] public Text cellLabelPrefab;

    [SerializeField] public int width = 6;
    [SerializeField] public int height = 6;

    [SerializeField] public bool showLabels;
    
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

    private void Update()
    {
        _InputHandler.HandleInput();
    }

    #endregion Unity Methods


    #region Private Variables

    private HexGridInputHandler _InputHandler;    
    
    private static HexCell[] _Cells;
    private static Dictionary<HexCoordinates, HexCell> _CellsDictionary = new Dictionary<HexCoordinates, HexCell>();
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
        _CellsDictionary.Add(cell.coordinates, cell);

        if (showLabels)
        {
            Text label = Instantiate<Text>(cellLabelPrefab);
            label.rectTransform.SetParent(_GridCanvas.transform, false);
            label.rectTransform.anchoredPosition = new Vector2(position.x, position.y);
            label.text = cell.coordinates.ToStringOnSeparateLines();   
        }
    }

    private void CreateGrid()
    {
        for (int y = 0, i = 0; y < height; y++) 
        {
            for (int x = 0; x < width; x++) 
            {
                if (y > 10 && x > 10)
                {
                    CreateCell(x, y, i++, HexCell.ECellType.Water);   
                }
                else if (y > 10 && x < 10)
                {
                    CreateCell(x, y, i++, HexCell.ECellType.Forest);
                }
                else if (x < 3 && y < 3)
                {
                    CreateCell(x, y, i++, HexCell.ECellType.Mountains);
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