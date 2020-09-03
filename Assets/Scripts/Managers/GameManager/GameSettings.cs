using System.Collections;
using System.Collections.Generic;
using GenericEnums;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]
public class GameSettings : ScriptableObject
{
    #region Public Types

    #endregion Public Types


    #region Public Variables

    #endregion Public Variables


    #region Public Methods

    public Color GetConflictSideColor(EConflictSide conflictSide)
    {
        if (_ConflictSideColors == null)
        {
            _ConflictSideColors = new Dictionary<EConflictSide, Color>();
            
            _ConflictSideColors.Add((EConflictSide)0, Color_Independent);
            _ConflictSideColors.Add((EConflictSide)1, Color_Player1Team);
            _ConflictSideColors.Add((EConflictSide)2, Color_Player2Team);
            _ConflictSideColors.Add((EConflictSide)3, Color_Player3Team);
            _ConflictSideColors.Add((EConflictSide)4, Color_Player4Team);
        }

        return _ConflictSideColors[conflictSide];
    }
    
    #endregion Public Methods


    #region Inspector Variables

    [Range(1, 4)]
    [SerializeField] public int AmountOfPlayers;

    [SerializeField] public Color Color_Independent;
    [SerializeField] public Color Color_Player1Team;
    [SerializeField] public Color Color_Player2Team;
    [SerializeField] public Color Color_Player3Team;
    [SerializeField] public Color Color_Player4Team;

    [SerializeField] public GameObject firstTierGridFigure;

    #endregion Inspector Variables


    #region Unity Methods

    #endregion Unity Methods


    #region Private Variables

    private Dictionary<EConflictSide, Color> _ConflictSideColors;

    #endregion Private Variables


    #region Private Methods

    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
