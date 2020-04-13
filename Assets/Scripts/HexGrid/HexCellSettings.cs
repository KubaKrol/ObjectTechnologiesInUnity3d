using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HexCellsSettings", menuName = "HexGrid/HexCellsSettings")]
public class HexCellSettings : ScriptableObject
{
    [SerializeField] public Color fieldColor;
    [SerializeField] public Color waterColor;
    [SerializeField] public Color forestColor;
    [SerializeField] public Color cityColor;
    [SerializeField] public Color mountainsColor;
}
