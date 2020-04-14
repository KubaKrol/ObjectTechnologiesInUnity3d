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
        //var firstCellSum = firstCell.coordinates.X + firstCell.coordinates.Y;
        //var secondCellSum = secondCell.coordinates.X + secondCell.coordinates.Y;

        return (Mathf.Abs(firstCell.coordinates.X - secondCell.coordinates.X) +
                Mathf.Abs(firstCell.coordinates.X + firstCell.coordinates.Y - secondCell.coordinates.X - secondCell.coordinates.Y) +
                Mathf.Abs(firstCell.coordinates.Y - secondCell.coordinates.Y)) / 2;

        //return Mathf.Abs(firstCell.coordinates.Y - secondCellSum);
    }
    
    /*unction hex_distance(a, b):
        return (abs(a.x - b.x) 
    + abs(a.x + a.y - b.x - b.y)
    + abs(a.y - b.y)) / 2*/
}