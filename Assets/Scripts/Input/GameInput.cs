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
                InitializeMouseInput();
                return _CurrentGameInput;
            }
        }
    }

    private void InitializeMouseInput()
    {
        _CurrentGameInput = new MouseInput();
    }

    public void InitializeTouchInput()
    {
        //todo
    }

    private IAmInput _CurrentGameInput;
}
