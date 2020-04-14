using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridCityPlanner
{
    #region Public Types

    #endregion Public Types


    #region Public Variables

    #endregion Public Variables


    #region Public Methods

    public HexGridCityPlanner(HexGrid hexGrid, HexGridSettings hexGridSettings)
    {
        _HexGrid = hexGrid;
        _HexGridSettings = hexGridSettings;
    }

    public void RunCityPlanner()
    {
        if (_HexGridSettings.width < 12 || _HexGridSettings.height < 12)
        {
            Debug.LogError("City planner cannot work on grids smaller than 12x12");
        }
        
        PlaceCapitols();
    }
    
    #endregion Public Methods


    #region Inspector Variables

    #endregion Inspector Variables


    #region Unity Methods

    #endregion Unity Methods


    #region Private Variables

    private HexGrid _HexGrid;
    private HexGridSettings _HexGridSettings;

    #endregion Private Variables


    #region Private Methods

    private void PlaceCapitols()
    {
        var width = _HexGridSettings.width;
        var height = _HexGridSettings.height;
        var offset = _HexGridSettings.DefaultCapitolCornerOffset;
        
        HexGrid.GetCell(offset, offset).SetCellType(HexCell.ECellType.City);
        HexGrid.GetCell(width - offset, offset).SetCellType(HexCell.ECellType.City);
        HexGrid.GetCell(width - offset, height - offset).SetCellType(HexCell.ECellType.City);
        HexGrid.GetCell(offset, height - offset).SetCellType(HexCell.ECellType.City);
    }
    
    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
