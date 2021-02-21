using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour {

    [SerializeField] float _Speed;
    [SerializeField] GameObject _Tap, _Hold, _HoldRelease, _HoldReleaseSuccess, _HoldReleaseCancel;

    PlayerInputs _Inputs;
    Vector2 _Movement;
    Rigidbody2D _Rigidbody;

    private void Awake () {
        _Rigidbody = GetComponent<Rigidbody2D> ();
        _Inputs = new PlayerInputs ();
        SetAllInactive ();
    }

    private void OnEnable () {
        _Inputs.Enable ();

        // Assign Methods to the actions
        _Inputs.Gameplay.Movement.performed += OnMovement;
        _Inputs.Gameplay.Movement.canceled += OnMovement;

        _Inputs.Gameplay.Interaction.started += OnInteractionStarted;
        _Inputs.Gameplay.Interaction.performed += OnInteractionPerformed;
        _Inputs.Gameplay.Interaction.canceled += OnInteractionCanceled;
    }

    private void OnDisable () {
        // disable them, cause they areN#t needed here anymore
        _Inputs.Gameplay.Movement.performed -= OnMovement;
        _Inputs.Gameplay.Movement.canceled -= OnMovement;

        _Inputs.Gameplay.Interaction.started -= OnInteractionStarted;
        _Inputs.Gameplay.Interaction.performed -= OnInteractionPerformed;
        _Inputs.Gameplay.Interaction.canceled -= OnInteractionCanceled;

        _Inputs.Disable ();
    }

    private void OnMovement (InputAction.CallbackContext context) {
        _Movement = context.ReadValue<Vector2> ();
    }

    public void OnInteractionStarted (InputAction.CallbackContext context) {
        SetAllInactive ();
        if (context.interaction is SlowTapInteraction) {
            _Hold.SetActive (true);
        }
    }
    public void OnInteractionPerformed (InputAction.CallbackContext context) {
        SetAllInactive ();
        if (context.interaction is SlowTapInteraction) {
            _HoldRelease.SetActive (true);
            _HoldReleaseSuccess.SetActive (true);
        } else {
            _Tap.SetActive (true);
        }
    }
    public void OnInteractionCanceled (InputAction.CallbackContext context) {
        SetAllInactive ();
        _HoldRelease.SetActive (true);
        _HoldReleaseCancel.SetActive (true);
    }

    // this will just basically hide all the text objects      
    private void SetAllInactive () {
        _Tap.SetActive (false);
        _Hold.SetActive (false);
        _HoldRelease.SetActive (false);
        _HoldReleaseSuccess.SetActive (false);
        _HoldReleaseCancel.SetActive (false);
    }

    private void FixedUpdate () {
        if (_Movement.sqrMagnitude > 0)
            _Rigidbody.velocity = _Movement * _Speed;
    }

}