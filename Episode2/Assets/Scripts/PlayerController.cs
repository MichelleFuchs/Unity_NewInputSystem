using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour {

    [SerializeField] float _Speed;

    [SerializeField] GameObject _Tap, _Hold, _HoldRelease, _HoldReleaseSuccess, _HoldReleaseCancel;

    Vector2 _Movement;

    Rigidbody2D _Rigidbody;

    private void Awake () {
        _Rigidbody = GetComponent<Rigidbody2D> ();
        SetAllInactive ();
    }

    public void OnMovement (InputAction.CallbackContext context) {
        _Movement = context.ReadValue<Vector2> ();
    }

    // The method below is the final method how it is used at the end of the video
    // For the Log Statements in the middle of the video, check the comments at the end of this file
    public void OnInteraction (InputAction.CallbackContext context) {
        SetAllInactive ();

        switch (context.phase)  {    
            case  InputActionPhase.Started:
                if (context.interaction is SlowTapInteraction) {
                    _Hold.SetActive (true);
                }
                break;   
            case InputActionPhase.Performed:
                if (context.interaction is SlowTapInteraction) {
                    _HoldRelease.SetActive (true);
                    _HoldReleaseSuccess.SetActive (true);
                } else {
                    _Tap.SetActive (true);
                }
                break;     
            case  InputActionPhase.Canceled:
                _HoldRelease.SetActive (true);
                _HoldReleaseCancel.SetActive (true);
                break;
        }
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
        if (_Movement.sqrMagnitude > 0) {
            _Rigidbody.velocity = _Movement * _Speed;
        }
    }

}

// Add the following code into your "OnInteraction" method, if you are interested in the logging
// make sure you are using the same parameter variable name or rename it accordingly
// also remove the comment characters ( /* & */)

/*

switch(context.phase) {
    case InputActionPhase.Performed:
        Debug.Log("Performed");
        break;
    case InputActionPhase.Started:
        Debug.Log("Started");
        break;
    case InputActionPhase.Canceled:
        Debug.Log("Canceled");
        break;
}


*/