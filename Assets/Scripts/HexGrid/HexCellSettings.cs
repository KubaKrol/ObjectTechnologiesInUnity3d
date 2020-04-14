using UnityEngine;

[CreateAssetMenu(fileName = "HexCellsSettings", menuName = "HexGrid/HexCellsSettings")]
public class HexCellSettings : ScriptableObject
{
    [SerializeField] public Color fieldColor;
    [SerializeField] public Color waterColor;
    [SerializeField] public Color deepWaterColor;
    [SerializeField] public Color forestColor;
    [SerializeField] public Color cityColor;
    [SerializeField] public Color mountainsColor;
}
