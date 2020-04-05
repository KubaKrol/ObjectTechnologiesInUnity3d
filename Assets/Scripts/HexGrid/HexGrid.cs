using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour 
{
    #region Public Types

    #endregion Public Types


    #region Public Variables

    #endregion Public Variables


    #region Public Methods

    #endregion Public Methods


    #region Inspector Variables

    [SerializeField] public HexCell cellPrefab;
    [SerializeField] public Text cellLabelPrefab;
    
    [SerializeField] public int width = 6;
    [SerializeField] public int height = 6;
    
    #endregion Inspector Variables


    #region Unity Methods
    
    private void Awake ()
    {
        _GridCanvas = GetComponentInChildren<Canvas>();
        _Cells = new HexCell[height * width];
    }

    private void Start()
    {
        CreateGrid();
    }

    #endregion Unity Methods


    #region Private Variables

    private HexCell[] _Cells;
    private Canvas _GridCanvas;
    
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