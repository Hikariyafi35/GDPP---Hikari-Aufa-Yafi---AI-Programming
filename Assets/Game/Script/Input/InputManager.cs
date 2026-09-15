using UnityEngine;
using UnityEngine.Events;
using static GameInputAction;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, IPlayerActions
{
    // Variable untuk menyimpan reference object input action 
    private GameInputAction _inputAction;
    public UnityEvent<Vector2> OnMoveInput;
    public UnityEvent<bool> OnSprintInput; 
    private void Awake()
    {
        // Membuat object GameInputAction dan menyimpan reference nya 
        // ke variable _inputAction 
        _inputAction = new GameInputAction();
        // Mengaktifkan input action 
        _inputAction.Enable();
        // Mengaktifkan action map Player 
        _inputAction.Player.Enable();
        // Memberi tahu bahwa kelas ini akan mendeteksi input dari 
        // action map Player 
        _inputAction.Player.SetCallbacks(this);

    } 
    public void OnInteract(InputAction.CallbackContext context) 
    { 
        if (context.performed) 
        { 
            // Memunculkan log interact di console  
            // ketika input interact ditekan 
            Debug.Log("Interact"); 
        } 
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        // Memanggil on move input ketika input move ditekan dan dilepas 
        // Event akan mengirimkan data arah input 
        OnMoveInput?.Invoke(context.ReadValue<Vector2>());
        Debug.Log(context.ReadValue<Vector2>());
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("SPRINT PERFORMED → TRUE");
            OnSprintInput?.Invoke(true);
        }
        if(context.canceled)
        {
            Debug.Log("SPRINT CANCELED → FALSE");
            OnSprintInput?.Invoke(false);

        }
    }
}
