using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float _Speed = 3;
    [SerializeField] Camera _Camera;
    PlayerInput _Input;
    Vector2 _Movement;
    Vector2 _MousePos;

    Rigidbody2D _Rigidbody;

    private void Awake () {
        _Input = new PlayerInput ();
        _Rigidbody = GetComponent<Rigidbody2D> ();
    }

    private void OnEnable () {
        _Input.Enable ();

        _Input.Gameplay.Movement.performed += OnMovement;
        _Input.Gameplay.Movement.canceled += OnMovement;

        _Input.Gameplay.MousePos.performed += OnMousePos;

    }

    private void OnDisable () {
        _Input.Gameplay.Movement.performed -= OnMovement;
        _Input.Gameplay.Movement.canceled -= OnMovement;
        _Input.Gameplay.MousePos.performed -= OnMousePos;

        _Input.Disable ();
    }

    private void OnMovement (InputAction.CallbackContext context) {
        _Movement = context.ReadValue<Vector2> ();
    }

    private void OnMousePos (InputAction.CallbackContext context) {
        _MousePos = _Camera.ScreenToWorldPoint (context.ReadValue<Vector2> ());
    }

    private void FixedUpdate () {
        // Immediate move and stop
        // _Rigidbody.velocity = _Movement * _Speed;

        // Accelerated movement with Linear Drag
        _Rigidbody.AddForce (_Movement * _Speed);

        // Calculate and set Rigidbodys Rotation
        Vector2 facingDirection = _MousePos - _Rigidbody.position;
        float angle = Mathf.Atan2 (facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
        _Rigidbody.MoveRotation (angle);
    }
}