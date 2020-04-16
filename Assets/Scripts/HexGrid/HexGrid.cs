using System.Collections.Generic;
using GenericEnums;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour 
{
    #region Public Types

    #endregion Public Types


    #region Public Variables

    public static UnityAction<HexCell, EConflictSide> SelectCellAction;
    
    public Vector3 centerPosition;
    
    public float widthInWorldCoordinates
    {
        get
        {
            var worldWidth = 0f;
            
            for (var i = 0; i < _HexGridSettings.width; i++)
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
            
            for (var i = 0; i < _HexGridSettings.height; i++)
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

    public void CreateCell (int x, int y, int i, HexCell.ECellType cellType = HexCell.ECellType.Field) 
    {
        Vector3 position;
        position.x = x * (HexMetrics.outerRadius * 1.5f);
        position.y = (y + x * 0.5f - x / 2) * (HexMetrics.innerRadius * 2f);
        position.z = 0f;
        
        HexCell cell = _Cells[i] = Instantiate<HexCell>(_HexGridSettings.cellPrefab);
        _Cells2D[x][y] = cell;
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, y);
        cell.SetCellType(cellType);
        cell.SetOrderInLayer(_HexGridSettings.width - y);
        _CellsDictionary.Add(cell.coordinates, cell);

        if (_HexGridSettings.showLabels)
        {
            Text label = Instantiate<Text>(_HexGridSettings.cellLabelPrefab);
            label.rectTransform.SetParent(_GridCanvas.transform, false);
            label.rectTransform.anchoredPosition = new Vector2(position.x, position.y);
            label.text = cell.coordinates.ToStringOnSeparateLines();   
        }
    }
    
    public static HexCell GetCell(Vector2 worldPosition)
    {
        var smallestPositionDifference = Mathf.Infinity;
        HexCell cellToReturn = null;
        
        foreach (var cell in _Cells)
        {
            var xDifference = Mathf.Abs(cell.transform.localPosition.x - worldPosition.x);
            var yDifference = Mathf.Abs(cell.transform.localPosition.y - worldPosition.y);

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

    public static HexCell GetCell(int i, int j)
    {
        if (i >= 0 && j >= 0)
        {
            if (_Cells2D.Length >= i)
            {
                if (_Cells2D[i].Length >= j)
                {
                    return _Cells2D[i][j];
                }
            }   
        }

        Debug.LogError("CellNotFound");
        return null;
    }

    #endregion Public Methods


    #region Inspector Variables

    [SerializeField] private GameInput _GameInput;
    [SerializeField] private HexGridSettings _HexGridSettings;

    #endregion Inspector Variables


    #region Unity Methods
    
    private void Awake ()
    {
        _GridCanvas = GetComponentInChildren<Canvas>();
        
        _Cells = new HexCell[_HexGridSettings.height * _HexGridSettings.width];
        _Cells2D = new HexCell[_HexGridSettings.width][];
        
        for (var i = 0; i < _HexGridSettings.width; i++)
        {
            _Cells2D[i] = new HexCell[_HexGridSettings.height];
        }
        
        centerPosition = new Vector3(widthInWorldCoordinates / 2f - HexMetrics.innerRadius, heightInWorldCoordinates / 2f - HexMetrics.outerRadius, transform.position.z);
    }

    #endregion Unity Methods


    #region Private Variables
    
    private HexGridPerlinNoiseGenerator _PerlinNoiseGenerator;
    private HexGridCityPlanner _HexGridCityPlanner;
    
    private static HexCell[] _Cells;
    private static HexCell[][] _Cells2D;
    private static Dictionary<HexCoordinates, HexCell> _CellsDictionary = new Dictionary<HexCoordinates, HexCell>();
    private Canvas _GridCanvas;

    #endregion Private Variables


    #region Private Methods

    public void CreateGrid()
    {
        _PerlinNoiseGenerator = new HexGridPerlinNoiseGenerator(_HexGridSettings);
        var noise = _PerlinNoiseGenerator.GenerateNoise(Random.Range(0, _HexGridSettings.seedRange),
            _HexGridSettings.xMultiplier, _HexGridSettings.yMultiplier);

        for (int y = 0, i = 0; y < _HexGridSettings.height; y++)
        {
            for (int x = 0; x < _HexGridSettings.width; x++)
            {
                if (noise[y][x] < 0.05f)
                {
                    CreateCell(x, y, i++, HexCell.ECellType.DeepWater);
                }

                if (noise[y][x] >= 0.05f && noise[y][x] < 0.3f)
                {
                    CreateCell(x, y, i++, HexCell.ECellType.Water);
                }

                if (noise[y][x] >= 0.3f && noise[y][x] < 0.55f)
                {
                    CreateCell(x, y, i++, HexCell.ECellType.Field);
                }

                if (noise[y][x] >= 0.55f && noise[y][x] < 0.75f)
                {
                    CreateCell(x, y, i++, HexCell.ECellType.Forest);
                }

                if (noise[y][x] >= 0.75f)
                {
                    CreateCell(x, y, i++, HexCell.ECellType.Mountains);
                }
            }
        }
        
        if (_HexGridSettings.runCityPlanner)
        {
            _HexGridCityPlanner = new HexGridCityPlanner(_HexGridSettings);
            _HexGridCityPlanner.RunCityPlanner();
        }
    }

    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}