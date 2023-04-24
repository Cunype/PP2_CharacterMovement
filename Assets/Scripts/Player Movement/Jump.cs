using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Jump : MonoBehaviour
{
    [SerializeField] private float jumpVelocity_ = 1.0f;
    [SerializeField] private float allowedAirTime_ = 0.10f;
    [SerializeField] private float releaseVelocityChange_ = 3.0f;

    private bool onAirIsMantaining = false;
    public float onAirHangTime = 0.1f;
    public float onAirHangGravityThreshold = 0.1f;
    public float onAirHangSpeedModifier = 5.0f;
    [SerializeField] private float onAirHangGravityMultiplier = 0.5f;
    
    public bool grounded_ = false;
    private float airTime_ = 0.0f;
    private float originalGravity_ = 0.0f;
    
    private Rigidbody rb_;
    private Collider collider_;
    private Gravity gravity_;
    private Dash dash_;

    public float inputOnAirTime_ = 0.1f;
    private bool hasInputJump_ = false;

    // Start is called before the first frame update
    void Start()
    {
        rb_ = GetComponent<Rigidbody>();
        collider_ = GetComponent<Collider>();
        gravity_ = GetComponent<Gravity>();
        dash_ = GetComponent<Dash>();
        originalGravity_ = gravity_.gravityScale_;
    }

    // Update is called once per frame
    void Update()
    {
        grounded_ = Grounded();

        if (!grounded_) airTime_ += Time.deltaTime;
        else if(grounded_) airTime_ = 0.0f;

        if (Input.GetButtonDown("Jump") && !grounded_ && airTime_ >= allowedAirTime_)
        {
            StartCoroutine(JumpedInAir());
        }

        ProcessJump();
        PlayerGravityOnPeak();
    }

    void ProcessJump()
    {
        if ((Input.GetButtonDown("Jump") || hasInputJump_)  && (grounded_ || airTime_ < allowedAirTime_) && !dash_.isDashing_)
        {
            //dash_.StopDash();
            hasInputJump_ = false;
            dash_.canDash_ = true;
            rb_.velocity = new Vector3(rb_.velocity.x, jumpVelocity_, rb_.velocity.z);
        }
        else if (Input.GetButtonUp("Jump") && rb_.velocity.y > 0.0f)
        {
            rb_.velocity = new Vector3(rb_.velocity.x, rb_.velocity.y/releaseVelocityChange_, rb_.velocity.z);
        }
    }

    void PlayerGravityOnPeak()
    {
        if (!grounded_ && Mathf.Abs(rb_.velocity.y) < onAirHangGravityThreshold)
        {
            if (!onAirIsMantaining)
            {
                StartCoroutine(OnPeakCurveModificator());
            }
        }
    }

    bool Grounded()
    {
        int layerMask = 1 << 3; //Third layer bit map
        layerMask = ~layerMask; //Inverse bitmap
        return Physics.CheckCapsule(collider_.bounds.center,
            new Vector3(collider_.bounds.center.x, collider_.bounds.min.y + 0.1f, collider_.bounds.center.z),
            0.18f,layerMask);
    }

    IEnumerator OnPeakCurveModificator()
    {
        onAirIsMantaining = true;
        gravity_.gravityScale_ = onAirHangGravityMultiplier;
        rb_.velocity += new Vector3(Input.GetAxisRaw("Horizontal") * onAirHangSpeedModifier,0.0f,0.0f);
        yield return new WaitForSeconds(onAirHangTime);
        gravity_.gravityScale_ = originalGravity_;
        onAirIsMantaining = false;
    }

    IEnumerator JumpedInAir()
    {
        hasInputJump_ = true;
        yield return new WaitForSeconds(inputOnAirTime_);
        hasInputJump_ = false;
    }
}
