using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    #region Variables

    public CharacterController _controller;
    public float _speed = 5f;
    public float _rotationSmoothing = 0.1f;

    private float _rotationSmoothingVel;
    
    #endregion

    #region Unity_Methods

    private void Start()
    {
        if (_controller == null)
            _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
       MovePlayer();
    }

    #endregion

    #region Method

    void MovePlayer()
    {
        float _horizontal = Input.GetAxisRaw("Horizontal");
        float _vertical = Input.GetAxisRaw("Vertical");

        Vector3 _direction = new Vector3(_horizontal, 0f, _vertical).normalized;

        if (_direction.magnitude >= 0.1f)
        {
            float _targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
            float _angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, 
                            _targetAngle, ref _rotationSmoothingVel,
                _rotationSmoothing);
            
            transform.rotation = Quaternion.Euler(0f, _angle, 0f);
            _controller.Move(_direction * _speed * Time.deltaTime);
        }
 
    }

    #endregion
}
