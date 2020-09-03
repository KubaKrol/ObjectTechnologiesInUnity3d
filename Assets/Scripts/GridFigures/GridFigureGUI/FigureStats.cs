using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FigureStats : MonoBehaviour
{
    #region Public Types

    #endregion Public Types


    #region Public Variables

    #endregion Public Variables


    #region Public Methods

    public void UpdateStats()
    {
        _StrengthText.text = _MyGridFigure.FigureStrength.ToString();
    }
    
    #endregion Public Methods


    #region Inspector Variables

    [SerializeField] private TextMeshProUGUI _StrengthText;
    [SerializeField] private Transform _StatsPlaceholder;
    
    #endregion Inspector Variables


    #region Unity Methods

    private void Awake()
    {
        _MyGridFigure = GetComponentInParent<GridFigure>();
        _MyCanvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(_StatsPlaceholder.transform.position);
    }
    
    #endregion Unity Methods


    #region Private Variables

    private GridFigure _MyGridFigure;
    private CanvasGroup _MyCanvasGroup;

    #endregion Private Variables


    #region Private Methods

    #endregion Private Methods


    #region Coroutines

    #endregion Coroutines
}
