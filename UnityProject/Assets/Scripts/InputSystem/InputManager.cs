using UnityEngine;

public class InputManager : MonoBehaviour
{
    public InputSystemControls inputSystem;

    private void Awake()
    {
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

    public bool PlayerShoot()
    {
        return inputSystem.Player.Shoot.triggered;
    }

    public bool PlayerDash()
    {
        return inputSystem.Player.Dash.triggered;
    }

    public Vector2 GetMouseDelta()
    {
        return inputSystem.Camera.MouseLook.ReadValue<Vector2>();
    }
}
