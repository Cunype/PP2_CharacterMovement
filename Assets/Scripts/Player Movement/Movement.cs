using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private bool orientPlayer_;
    [SerializeField] private float maxSpeedX_ = 40.0f;
    [SerializeField] private float currentSpeed_ = 0.0f;
    [SerializeField] private float accelSpeed_ = 70.0f;
    [SerializeField] private float deccelSpeed_ = 50.0f;
    
    private Rigidbody rb_;
    
    private Vector3 move_;
    private Vector3 currentPos_;
    
    private float inputX_; // -1 -> left || 1 -> right
    private float prevInputX_;
    [HideInInspector]
    public float currentDirection_;

    private Dash dash_;
    private Jump jump_;

    private void Start()
    {
        rb_ = GetComponent<Rigidbody>();
        dash_ = GetComponent<Dash>();
        jump_ = GetComponent<Jump>();
        currentPos_ = transform.position;
        currentDirection_ = 1.0f;
    }

    private void Update()
    {

        currentPos_ = transform.position;
        inputX_ = Input.GetAxisRaw("Horizontal");

        if (inputX_ > 0.0f) currentDirection_ = 1.0f;
        else if (inputX_ < 0.0f) currentDirection_ = -1.0f;
        
        if(orientPlayer_) transform.forward = Vector3.forward * currentDirection_;
        
        if (!dash_.isDashing_)
        {
            ProcessMovement();
        }
        
    }

    private void FixedUpdate()
    {
        if (!dash_.isDashing_)
        {
            Move();
        }
    }

    void ProcessMovement()
    {

        if (Mathf.Abs(inputX_) > 0.0f && (prevInputX_ == inputX_))
        {
            currentSpeed_ += accelSpeed_ * Time.deltaTime;
        }else
        {
            currentSpeed_ -= deccelSpeed_ * Time.deltaTime;
        }

        currentSpeed_ = Mathf.Clamp(currentSpeed_, 0.0f, maxSpeedX_);

        move_.x = currentSpeed_ * currentDirection_;

        prevInputX_ = inputX_;
    }

    void Move()
    {
        rb_.MovePosition((currentPos_ + (move_ * Time.fixedDeltaTime)));
    }

    public float GetSpeed()
    {
        return move_.x;
    }
    
}
