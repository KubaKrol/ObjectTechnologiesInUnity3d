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
        
        var currentHexCoordinates = _MyHexCell.coordinates;
        
        for(var i = currentHexCoordinates.X - DefaultMovementRange; i <= currentHexCoordinates.X + DefaultMovementRange; i++)
        {
            for (var j = currentHexCoordinates.Y - DefaultMovementRange; j <= currentHexCoordinates.Y + DefaultMovementRange; j++)
            {
                var targetHexCell = HexGrid.GetCell(new HexCoordinates(i, j));

                if (targetHexCell != null && HexMetrics.Distance(_MyHexCell, targetHexCell) <= _CurrentMovementRange)
                {
                    if (targetHexCell.cellType != HexCell.ECellType.Mountains &&
                        targetHexCell.cellType != HexCell.ECellType.Water)
                    {
                        targetHexCell.ShowMovementAvailability(active);   
                    }
                }
            }
        }
    }

    #endregion Public Methods


    #region Inspector Variables

    #endregion Inspector Variables


    #region Unity Methods

    #endregion Unity Methods


    #region Private Variables

    #endregion Private Variables


    #region Private Methods

    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
