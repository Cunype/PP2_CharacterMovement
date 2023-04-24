using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour
{
    public float gravityScale_ = 1.0f;
    public bool isIndependent = false;

    public static float globalGravity_ = -9.81f;
 
    public Rigidbody rb_;
 
    void OnEnable()
    {
        rb_ = GetComponent<Rigidbody>();
        rb_.useGravity = false;
    }
 
    void FixedUpdate()
    {
        Vector3 gravity = globalGravity_ * gravityScale_ * Vector3.up;
        rb_.AddForce(gravity, ForceMode.Acceleration);
        rb_.velocity = new Vector3(rb_.velocity.x, Mathf.Clamp(rb_.velocity.y, -20.0f, 20.0f), rb_.velocity.z);
    }
}
