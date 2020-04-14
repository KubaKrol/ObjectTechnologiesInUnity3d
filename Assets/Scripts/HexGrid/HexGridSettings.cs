using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "HexGridSettings", menuName = "HexGrid/HexGridSettings")]
public class HexGridSettings : ScriptableObject
{
    #region Public Types

    #endregion Public Types


    #region Public Variables

    #endregion Public Variables


    #region Public Methods

    #endregion Public Methods


    #region Inspector Variables

    [Header("Basic grid Settings")]
    
    [SerializeField] public HexCell cellPrefab;
    [SerializeField] public Text cellLabelPrefab;

    [Range(12,32)]
    [SerializeField] public int width = 12;
    [Range(12,32)]
    [SerializeField] public int height = 12;

    [SerializeField] public bool showLabels;

    [Header("Perlin noise settings")] 
    
    [SerializeField] public float xMultiplier = 0.2f;
    [SerializeField] public float yMultiplier = 0.2f;

    [SerializeField] public float seedRange = 1000;

    [Header("City planner Settings")]
    
    [SerializeField] public bool runCityPlanner;

    [SerializeField] public int DefaultCapitolCornerOffset = 3;
    [SerializeField] public int CitiesPerQuarter = 5;

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
