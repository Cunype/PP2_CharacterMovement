using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorGestor : MonoBehaviour
{
    public Animator animator;
    private Movement movement;
    private Dash dash;
    private Jump jump_;

    private void Start()
    {
        movement = GetComponent<Movement>();
        jump_ = GetComponent<Jump>();
        dash = GetComponent<Dash>();
    }

    private void Update()
    {
        // Check if the player is running
        if (movement.GetSpeed() != 0.0f)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
        animator.SetBool("OnAir", !jump_.grounded_);
        animator.SetBool("Dash", dash.isDashing_);
    }
}
