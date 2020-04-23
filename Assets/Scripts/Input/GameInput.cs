using UnityEngine;

[CreateAssetMenu(fileName = "GameInput", menuName = "Input/GameInput")]
public class GameInput : ScriptableObject
{
    public IAmInput currentInput
    {
        get
        {
            if (_CurrentGameInput != null)
            {
                return _CurrentGameInput;
            }
            else
            {
                #if UNITY_EDITOR
                InitializeMouseInput();                
                #endif
                
                #if UNITY_ANDROID
                InitializeMobileInput();
                #endif
                
                return _CurrentGameInput;
            }
        }
    }

    private void InitializeMouseInput()
    {
        _CurrentGameInput = new MouseInput();
    }

    public void InitializeMobileInput()
    {
        _CurrentGameInput = new MobileInput();
    }

    private IAmInput _CurrentGameInput;
}
