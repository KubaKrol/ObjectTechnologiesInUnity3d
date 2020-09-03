using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingFigure : GridFigure
{
    #region Public Types

    #endregion Public Types


    #region Public Variables

    #endregion Public Variables


    #region Public Methods
    
    public override void ShowMovementRange(bool active)
    {
        _CurrentMovementRange = DefaultMovementRange;

        if (_MyHexCell.cellType == HexCell.ECellType.Forest)
        {
            _CurrentMovementRange /= 2;
        }
        
        var reachableHexes = ReachableHexes(_MyHexCell, _CurrentMovementRange);

        foreach (var reachableHex in reachableHexes)
        {
            reachableHex.ShowMovementAvailability(active);
        }
    }

    #endregion Public Methods


    #region Inspector Variables

    #endregion Inspector Variables


    #region Unity Methods

    #endregion Unity Methods


    #region Private Variables
    
    private List<HexCell> ReachableHexes(HexCell origin, int movementRange)
    {
        movementRange += 1;
        
        List<HexCell> visited = new List<HexCell>();
        List<List<HexCell>> fringes = new List<List<HexCell>>();
        
        fringes.Add(new List<HexCell>());
        fringes[0].Add(origin);
        
        for (int i = 1; i < movementRange; i++)
        {
            fringes.Add(new List<HexCell>());

            foreach (var hex in fringes[i-1])
            {
                var allNeighbours = HexMetrics.GetAllNeighbours(hex);

                for (int j = 0; j < 6; j++)
                {
                    if (allNeighbours[j] != null && !visited.Contains(allNeighbours[j]) && allNeighbours[j].locomotionState == HexCell.ELocomotionState.Walkable)
                    {
                        fringes[i].Add(allNeighbours[j]);
                        visited.Add(allNeighbours[j]);
                    }  
                }
                
                /*foreach (var neighbour in allNeighbours)
                {
                    if (!visited.Contains(neighbour) && neighbour.locomotionState == HexCell.ELocomotionState.Walkable)
                    {
                        fringes[i].Add(neighbour);
                        visited.Add(neighbour);
                    }   
                }*/
            }
        }

        return visited;
    }
    
    #endregion Private Variables


    #region Private Methods

    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
