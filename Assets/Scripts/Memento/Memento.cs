using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Memento : MonoBehaviour
{
#region Inspector Variables
    //These are variables that should be set in the Inspector - Use [SerializeField] or [ShowInInspector]
    //Can be public or private.

#endregion Inspector Variables


#region Unity Methods

    private void OnEnable()
    {
        TurnManager.SaveTurn += Save;
        TurnManager.UndoTurn += Load;
    }

    private void OnDisable()
    {
        //TurnManager.SaveTurn -= Save;
        //TurnManager.UndoTurn -= Load;
    }

    private void Awake()
    {
        _MyMemorizedObject = GetComponent<IAmMemorized>();
    }

#endregion UnityMethods


#region Public Variables

    
#endregion Public Variables


#region Public Methods

    public void Save()
    {
        if (_MyMemorizedObject != null)
        {
            if (_MyMemorizedObject.MyMementoData == null)
            {
                _MyMemorizedObject.CreateMementoData();
            }
        }
        else
        {
            return;
        }
        
        _MyMemorizedObject.MyMementoData.Save(_MyMemorizedObject);
    }

    public void Load()
    {
        _MyMemorizedObject?.MyMementoData.Load(_MyMemorizedObject);
    }
    
#endregion Public Methods


#region Private Variables

    private IAmMemorized _MyMemorizedObject;

#endregion Private Variables


#region Private Methods

//Private methods, accessible only from this class.


#endregion Private Methods


#region Coroutines

//IEnumerators<>


#endregion Coroutines


#region Public Types

//enums, structs etc...


#endregion Public Types
}
