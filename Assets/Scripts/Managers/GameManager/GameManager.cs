using System.Collections;
using System.Collections.Generic;
using GenericEnums;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Public Types

    #endregion Public Types


    #region Public Variables

    #endregion Public Variables


    #region Public Methods

    public void NextTurn()
    {
        _TurnManager.NextTurn();
    }
    
    #endregion Public Methods


    #region Inspector Variables

    [Header("Settings")]
    
    [SerializeField] private GameSettings _GameSettings;

    [Header("Dependencies")]
    
    [SerializeField] private GameInput _GameInput;
    [SerializeField] private GameCamera _GameCamera;
    [SerializeField] private HexGrid _HexGrid;

    #endregion Inspector Variables


    #region Unity Methods

    private void Awake()
    {
        _PlayerManager = new PlayerManager();
        _TurnManager = new TurnManager();
        _GameInputHandler = new GameInputHandler(_GameInput.currentInput, _TurnManager);
        
        _PlayerManager.InitializePlayers(_GameSettings.AmountOfPlayers);
        _HexGrid.CreateGrid();
    }

    private void Start()
    {
        _GameCamera.SetTargetPosition(_HexGrid.centerPosition);
        _TurnManager.InitializeFirstTurn();
    }

    private void Update()
    {
        _GameInputHandler.HandleInput();
    }

    #endregion Unity Methods


    #region Private Variables

    private GameInputHandler _GameInputHandler;
    private PlayerManager _PlayerManager;
    private TurnManager _TurnManager;

    #endregion Private Variables


    #region Private Methods

    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
