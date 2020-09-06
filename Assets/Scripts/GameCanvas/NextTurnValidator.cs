using System.Collections;
using System.Collections.Generic;
using GenericEnums;
using TMPro;
using UnityEngine;

public class NextTurnValidator : MonoBehaviour
{
#region Inspector Variables

    [SerializeField] private GameObject NextTurnValidatorGUIObject;
    [SerializeField] private TextMeshProUGUI ValidationText;

#endregion Inspector Variables


#region Unity Methods
    //Methods from MonoBehaviour.
    //OnEnable(), OnDisable(), Awake(), Start(), Update() etc...

#endregion UnityMethods


#region Public Variables
    //Variables accessible from every other script referencing this class.
    //These variables should not be visible in Inspector and should be hidden by using [HideInInspector] or [ReadOnly]
    
#endregion Public Variables


#region Public Methods

    public void CheckPlayerMoves(EConflictSide currentPlayer, TurnManager turnManager)
    {
        if (_TurnManager == null)
        {
            _TurnManager = turnManager;
        }
        
        if (PlayerManager.GetPlayer(currentPlayer).movesLeft > 0)
        {
            ShowNotification(currentPlayer);
        }
        else
        {
            turnManager.NextTurn();
        }
    }

    public void NextTurn()
    {
        _TurnManager.NextTurn();
        HideNotification();
    }
    
    public void HideNotification()
    {
        NextTurnValidatorGUIObject.SetActive(false);
    }

#endregion Public Methods


#region Private Variables

    private TurnManager _TurnManager;
    
#endregion Private Variables


#region Private Methods

    private void ShowNotification(EConflictSide currentPlayer)
    {
        NextTurnValidatorGUIObject.SetActive(true);
        ValidationText.text = currentPlayer + " still have " + PlayerManager.GetPlayer(currentPlayer).movesLeft + " moves left, wanna end turn anyways?";
    }

    #endregion Private Methods


#region Coroutines
    //IEnumerators<>


#endregion Coroutines


#region Public Types
    //enums, structs etc...
    
    
#endregion Public Types
}
