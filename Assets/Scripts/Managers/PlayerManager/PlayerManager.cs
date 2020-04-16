using System.Collections.Generic;
using System.Runtime.InteropServices;
using GenericEnums;
using UnityEngine;

public class PlayerManager
{
    #region Public Types

    #endregion Public Types


    #region Public Variables

    #endregion Public Variables


    #region Public Methods
    
    public void InitializePlayers(int amountOfPlayers)
    {
        _PlayersDictionary = new Dictionary<EConflictSide, Player>();
        _PlayersList = new List<Player>();

        _AmountOfPlayers = amountOfPlayers;
        
        for (var i = 1; i <= amountOfPlayers; i++)
        {
            var conflictSide = (EConflictSide)i;
            var newPlayer = new Player(conflictSide);

            _PlayersDictionary.Add(conflictSide, newPlayer);
            _PlayersList.Add(newPlayer);
        }
    }

    public static Player GetPlayer(int index)
    {
        return _PlayersList[index];
    }

    public static Player GetPlayer(EConflictSide conflictSide)
    {
        return _PlayersDictionary[conflictSide];
    }

    public static int AmountOfPlayers()
    {
        return _AmountOfPlayers;
    }
    
    #endregion Public Methods


    #region Inspector Variables
    
    #endregion Inspector Variables


    #region Unity Method

    #endregion Unity Methods


    #region Private Variables

    private static int _AmountOfPlayers;
    
    private static List<Player> _PlayersList;
    private static Dictionary<EConflictSide, Player> _PlayersDictionary;

    #endregion Private Variables


    #region Private Methods

    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
