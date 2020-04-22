using UnityEngine;

[CreateAssetMenu(fileName = "HexCellsSettings", menuName = "HexGrid/HexCellsSettings")]
public class HexCellSettings : ScriptableObject
{
    [SerializeField] public Sprite fieldSprite;
    [SerializeField] public Sprite forestSprite;
    [SerializeField] public Sprite denseForestSprite;
    [SerializeField] public Sprite deepWaterSprite;
    [SerializeField] public Sprite waterSprite;
    [SerializeField] public Sprite mountainsSprite;
    [SerializeField] public Sprite highMountainSprite;
    [SerializeField] public Sprite citySprite;

    [SerializeField] public Sprite[] borderSprites;
}
