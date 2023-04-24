using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaGravity : MonoBehaviour
{
    private Bullet bullet_;
    private void Start()
    {
        bullet_ = transform.parent.parent.gameObject.GetComponent<Bullet>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Gravity gravity = other.gameObject.GetComponent<Gravity>();
        if (gravity && !gravity.isIndependent)
        {
            bullet_.affectedGameObjects.Add(other.gameObject);
            if(gravity.gravityScale_ == 0.0f)
                gravity.rb_.velocity = Vector3.zero;
            gravity.gravityScale_ = bullet_.insideGravity_;
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        Gravity gravity = other.gameObject.GetComponent<Gravity>();
        if (gravity && other.CompareTag("Bullet"))
        {
            //gravity.gravityScale_ = 4.25f;
            bullet_.affectedGameObjects.Remove(other.gameObject);
        } 
    }
}
