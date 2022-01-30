using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float _moveSpeed;

    [SerializeField] private FixedJoystick _fixedJoystick;

    private Rigidbody _rb;

    private void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        _rb.velocity = new Vector3(_fixedJoystick.Horizontal * _moveSpeed, _rb.velocity.y, 
            _fixedJoystick.Vertical * _moveSpeed);

        if (_fixedJoystick.Horizontal != 0 || _fixedJoystick.Vertical != 0) {
            transform.rotation = Quaternion.LookRotation(_rb.velocity);
        }
    }

}