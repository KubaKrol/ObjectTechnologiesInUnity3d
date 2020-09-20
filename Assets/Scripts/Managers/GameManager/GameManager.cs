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
        _NextTurnValidator.CheckPlayerMoves(_TurnManager.CurrentTurn, _TurnManager);
    }

    public void UndoCurrentTurn()
    {
        _TurnManager.UndoCurrentTurn();
    }
    
    #endregion Public Methods


    #region Inspector Variables

    [Header("Settings")]
    
    [SerializeField] private GameSettings _GameSettings;

    [Header("Dependencies")]
    
    [SerializeField] private GameInput _GameInput;
    [SerializeField] private GameCamera _GameCamera;
    [SerializeField] private HexGrid _HexGrid;
    [SerializeField] private GameCanvas _GameCanvas;

    #endregion Inspector Variables


    #region Unity Methods

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 1000;
        
        _PlayerManager = new PlayerManager();
        _TurnManager = new TurnManager(_GameSettings.AmountOfPlayers);
        _GameInputHandler = new GameInputHandler(_GameInput.currentInput, _TurnManager);
        
        _NextTurnValidator = GetComponent<NextTurnValidator>();
        
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
    private NextTurnValidator _NextTurnValidator;

    #endregion Private Variables


    #region Private Methods

    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
