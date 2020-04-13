using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Public Types

    #endregion Public Types


    #region Public Variables

    #endregion Public Variables


    #region Public Methods

    #endregion Public Methods


    #region Inspector Variables

    [SerializeField] private GameInput currentGameInput;
    
    #endregion Inspector Variables


    #region Unity Methods

    private void Awake()
    {
        _InputHandler = new PlayerInputHandler(currentGameInput.currentInput);
    }
    
    private void Update()
    {
        _InputHandler.HandleInput();
    }
    
    #endregion Unity Methods


    #region Private Variables

    private PlayerInputHandler _InputHandler;

    #endregion Private Variables


    #region Private Methods

    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
