using UnityEngine;

[System.Serializable]
public struct HexCoordinates
{
    [SerializeField]
    private int x, y;

    public int X => x;
    public int Y => y;

    public HexCoordinates (int x, int y) 
    {
        this.x = x;
        this.y = y;
    }
    
    public static HexCoordinates FromOffsetCoordinates (int x, int y) 
    {
        return new HexCoordinates(x - y / 2, y);
    }
    
    public override string ToString () 
    {
        return "(" + X.ToString() + ", " + Y.ToString() + ")";
    }

    public string ToStringOnSeparateLines () 
    {
        return X.ToString() + "\n" + Y.ToString();
    }
}
