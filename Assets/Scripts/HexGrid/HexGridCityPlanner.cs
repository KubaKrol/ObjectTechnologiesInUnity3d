using UnityEngine;

public class HexGridCityPlanner
{
    #region Public Types

    #endregion Public Types


    #region Public Variables

    #endregion Public Variables


    #region Public Methods

    public HexGridCityPlanner(HexGridSettings hexGridSettings)
    {
        _HexGridSettings = hexGridSettings;
    }

    public void RunCityPlanner()
    {
        if (_HexGridSettings.width < 12 || _HexGridSettings.height < 12)
        {
            Debug.LogError("City planner cannot work on grids smaller than 12x12");
        }
        
        PlaceCapitols();
        PlaceCities();
    }
    
    #endregion Public Methods


    #region Inspector Variables

    #endregion Inspector Variables


    #region Unity Methods

    #endregion Unity Methods


    #region Private Variables
    
    private HexGridSettings _HexGridSettings;

    #endregion Private Variables


    #region Private Methods

    private void PlaceCapitols()
    {
        var width = _HexGridSettings.width;
        var height = _HexGridSettings.height;
        var offset = _HexGridSettings.DefaultCapitolCornerOffset;

        Vector2[] positions =
        {
            new Vector2(offset, offset),
            new Vector2(width - offset - 1, offset),
            new Vector2(width - offset - 1, height - offset - 1),
            new Vector2(offset, height - offset - 1)
        };
        
        for (var i = 0; i < positions.Length; i++)
        {
            var newCityCell = HexGrid.GetCell((int) positions[i].x, (int) positions[i].y);
            
            newCityCell.SetCellType(HexCell.ECellType.City);
            UnblockCell(newCityCell);
        }
    }

    private void PlaceCities()
    {
        var width = _HexGridSettings.width;
        var height = _HexGridSettings.height;
        
        /*var amountOfCellsInQuarter = (width / 2) * (height / 2);
        var cityOffset = Mathf.Ceil(amountOfCellsInQuarter / ((float)_HexGridSettings.CitiesPerQuarter));*/
        
        //first quarter
        DistributeCitiesPerQuarter(0, 0, width / 2, height / 2);
        
        //second quarter
        DistributeCitiesPerQuarter((width / 2) - 1, (height / 2) - 1, width - 1, height - 1);
        
        //third quarter
        DistributeCitiesPerQuarter(0, (height / 2) - 1, width / 2, height - 1);
    }

    private void DistributeCitiesPerQuarter(int start_x_index, int start_y_index, int end_x_index, int end_y_index)
    {
        var width = end_x_index - start_x_index;
        var height = end_y_index - start_y_index;

        var amountOfCellsInQuarter = width * height;
        var cityOffset = amountOfCellsInQuarter / _HexGridSettings.CitiesPerQuarter;

        for (int i = start_x_index, x = 0; i <= end_x_index; i++)
        {
            for (int j = start_y_index; j <= end_y_index; j++, x++)
            {
                if (x % cityOffset == 0 && x != 0 && x != amountOfCellsInQuarter)
                {
                    var selectedCell = HexGrid.GetCell(i, j);
                    var newCityNeighbors = HexMetrics.GetAllNeighbours(selectedCell);
                    var randomNeighbor = Random.Range(0, newCityNeighbors.Length);

                    newCityNeighbors[randomNeighbor].SetCellType(HexCell.ECellType.City);
                    UnblockCell(newCityNeighbors[randomNeighbor]);
                }
                
                //HexGrid.GetCell(i, j).ShowMovementAvailability(true);
            }
        }
    }

    private void UnblockCell(HexCell hexCell)
    {
        var amountOfUnwalkableCells = 0;

        var allNeighbours = HexMetrics.GetAllNeighbours(hexCell);

        for (var i = 0; i < allNeighbours.Length; i++)
        {
            if (allNeighbours[i].locomotionState != HexCell.ELocomotionState.Walkable)
            {
                amountOfUnwalkableCells++;
            }
        }

        if (amountOfUnwalkableCells >= allNeighbours.Length - 1)
        {
            for (var i = 0; i < allNeighbours.Length; i++)
            {
                if (allNeighbours[i].cellType == HexCell.ECellType.Mountains)
                {
                    allNeighbours[i].SetCellType(HexCell.ECellType.Forest);
                }

                if (allNeighbours[i].cellType == HexCell.ECellType.Water ||
                    allNeighbours[i].cellType == HexCell.ECellType.DeepWater)
                {
                    allNeighbours[i].SetCellType(HexCell.ECellType.Field);
                }
            }
        }
    }
    
    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
