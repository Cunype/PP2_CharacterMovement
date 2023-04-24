using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Dash : MonoBehaviour
{
    public bool canDash_ = false;
    public bool isDashing_ = false;
    private bool waitingToReset_ = false;
    private float originalGravity = 0.0f;

    [SerializeField]
    private float dashingTime_ = 0.15f;
    [SerializeField]
    private float dashingPower_ = 24.0f;
    [SerializeField]
    private float dashingCoolingTime_ = 1.0f;

    private Gravity gravity_;
    private Rigidbody rb_;
    private Movement mv_;
    private Jump jump_;

    private bool DashedOnAir_;

    private void Start()
    {
        canDash_ = true;
        rb_ = GetComponent<Rigidbody>();
        mv_ = GetComponent<Movement>();
        gravity_ = GetComponent<Gravity>();
        jump_ = GetComponent<Jump>();
        originalGravity = gravity_.gravityScale_;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash_)
        {
            StartCoroutine(Dashing());
            CameraShake.Instance.Shake(0.1f, 0.05f);
        }

        if (waitingToReset_)
        {
            if (jump_.grounded_)
            {
                canDash_ = true;
                waitingToReset_ = false;
            }
        }
    }

    public void StopDash()
    {
        StopCoroutine(Dashing());
        rb_.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        canDash_ = true;
    }

    private IEnumerator Dashing()
    {
        canDash_ = false;
        isDashing_ = true;
        gravity_.gravityScale_ = 0.0f;
        rb_.velocity = new Vector3(transform.localScale.x * dashingPower_ * mv_.currentDirection_, 0.0f, 0.0f);
        yield return new WaitForSeconds(dashingTime_);
        gravity_.gravityScale_ = originalGravity;
        isDashing_ = false;
        rb_.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        yield return new WaitForSeconds(dashingCoolingTime_);
        canDash_ = true;
    }
}
