using System;
using System.Collections.Generic;
using GenericEnums;
using UnityEngine;
using Random = UnityEngine.Random;

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
        var offset = _HexGridSettings.CitiesOffset;

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
            
            newCityCell.SetConflictSide((EConflictSide)i+1);
            newCityCell.SetCellType(HexCell.ECellType.City);
            UnblockCell(newCityCell);

            var allNeighbours = newCityCell.neighbourCells;

            foreach (var neighbour in allNeighbours)
            {
                neighbour.SetConflictSide((EConflictSide)i+1);
            }

            foreach (var neighbour in allNeighbours)
            {
                neighbour.CheckBorders();
            }
            
            newCityCell.CheckBorders();
        }
    }

    private void PlaceCities()
    {
        var width = _HexGridSettings.width;
        var height = _HexGridSettings.height;
        var offset = _HexGridSettings.CitiesOffset + 1;
        
        //Capitols positions, cities should avoid these since there are already cities placed.
        Vector2[] positionsImmuneToReplacement =
        {
            new Vector2(offset, offset),
            new Vector2(width - offset - 1, offset),
            new Vector2(width - offset - 1, height - offset - 1),
            new Vector2(offset, height - offset - 1)
        };
        
        for (int i = 1; i < _HexGridSettings.width; i++)
        {
            for (int j = 1; j < _HexGridSettings.height; j++)
            {
                if (j % offset == 0 && i % offset == 0)
                {
                    var skipIteration = false;
                    
                    for (int n = 0; n < positionsImmuneToReplacement.Length; n++)
                    {
                        if (Math.Abs(i - positionsImmuneToReplacement[n].x) < 2 && Math.Abs(j - positionsImmuneToReplacement[n].y) < 2)
                        {
                            skipIteration = true;
                            break;
                        }                        
                    }

                    if (skipIteration)
                    {
                        continue;
                    }
                
                    var selectedCell = HexGrid.GetCell(i - 1, j - 1);
                    var newCityNeighbors = selectedCell.neighbourCells;
                    var randomNeighbor = Random.Range(0, newCityNeighbors.Length);

                    var newCityHexCell = newCityNeighbors[randomNeighbor];
                    newCityHexCell.SetCellType(HexCell.ECellType.City);
                    UnblockCell(newCityHexCell);
                }
            }
        }
    }
    

    private void UnblockCell(HexCell hexCell)
    {
        var amountOfUnwalkableCells = 0;

        var allNeighbours = hexCell.neighbourCells;

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
