using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Public Types

    #endregion Public Types


    #region Public Variables

    #endregion Public Variables


    #region Public Methods

    #endregion Public Methods


    #region Inspector Variables

    [SerializeField] private GameCamera _GameCamera;
    [SerializeField] private HexGrid _HexGrid;
    
    #endregion Inspector Variables


    #region Unity Methods

    private void Awake()
    {
        
    }

    private void Start()
    {
        _GameCamera.SetTargetPosition(_HexGrid.centerPosition);
    }

    private void Update()
    {
        
    }
    
    #endregion Unity Methods


    #region Private Variables

    #endregion Private Variables


    #region Private Methods

    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
