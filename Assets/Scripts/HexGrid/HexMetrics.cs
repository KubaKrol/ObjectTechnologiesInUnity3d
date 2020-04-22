using UnityEngine;

public static class HexMetrics 
{
    public const float outerRadius = 10f;
    public const float innerRadius = outerRadius * 0.866025404f;
    
    public static Vector3[] corners = 
    {
        new Vector3(0f, 0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius)
    };

    public static int Distance(HexCell firstCell, HexCell secondCell)
    {
        return (Mathf.Abs(firstCell.coordinates.X - secondCell.coordinates.X) +
                Mathf.Abs(firstCell.coordinates.X + firstCell.coordinates.Y - secondCell.coordinates.X - secondCell.coordinates.Y) +
                Mathf.Abs(firstCell.coordinates.Y - secondCell.coordinates.Y)) / 2;
    }

    public static HexCell[] GetAllNeighbours(HexCell hexCell)
    {
        HexCell[] neighbours;

        HexCoordinates[] directions =
        {
            new HexCoordinates(hexCell.coordinates.X , hexCell.coordinates.Y + 1),
            new HexCoordinates(hexCell.coordinates.X + 1, hexCell.coordinates.Y),        
            new HexCoordinates(hexCell.coordinates.X + 1, hexCell.coordinates.Y - 1),
            new HexCoordinates(hexCell.coordinates.X , hexCell.coordinates.Y - 1),
            new HexCoordinates(hexCell.coordinates.X - 1, hexCell.coordinates.Y),
            new HexCoordinates(hexCell.coordinates.X - 1, hexCell.coordinates.Y + 1),
        };

        neighbours = new HexCell[6];
        
        for (int i = 0; i < 6; i++)
        {
            if (HexGrid.GetCell(directions[i]) != null)
            {
                neighbours[i] = HexGrid.GetCell(directions[i]);
            }
            else
            {
                neighbours[i] = null;
            }
        }

        return neighbours;
    }
}