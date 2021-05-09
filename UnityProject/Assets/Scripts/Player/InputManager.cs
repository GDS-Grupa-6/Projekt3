using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private InputSystemControls inputSystem;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        inputSystem = new InputSystemControls();
    }

    private void OnEnable()
    {
        inputSystem.Enable();
    }

    private void OnDisable()
    {
        inputSystem.Disable();
    }

    public Vector2 MovementControls()
    {
        return inputSystem.Player.Movement.ReadValue<Vector2>();
    }

    public bool PlayerJumpedThisFrame()
    {
        return inputSystem.Player.Jump.triggered;
    }

    public Vector2 GetMouseDelta()
    {
        return inputSystem.Camera.MouseLook.ReadValue<Vector2>();
    }
}
