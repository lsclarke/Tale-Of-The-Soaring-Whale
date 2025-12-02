using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float stompTimer = 0;
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Bounce and Stomp")]
    public float bounceForce;
    public bool isBouncing;
    public float stompMultiplier;
    public GameObject bounceBallObj;
    public GameObject PlayerObj;
    public TrailRenderer trail;
    public bool readyToBounce;
    public bool readyToStomp;
    public bool isStomping;


    [Header("Air Dive Jump")]
    public float diveForwardForce;
    public float diveUpForce;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode bounceKey = KeyCode.Q;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;


    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight /** 0.5f + 0.2f*/, whatIsGround);

        Debug.DrawLine(this.transform.position, new Vector3(transform.position.x, transform.position.y - (playerHeight * 0.5f + 0.2f), transform.position.z), Color.yellow);
        MyInput();
        SpeedControl();
        StateHandler();

        // handle drag
        if (grounded)
        {
            rb.linearDamping = groundDrag;
            readyToBounce = false;
            readyToStomp = false;
            stompTimer = 0;
            Invoke(nameof(ResetPlayerObj), 0.1f);
        }
        else
        {
            readyToBounce = true;
            readyToStomp = true;
            rb.linearDamping = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();

        while (Input.GetKey(bounceKey) && !grounded)
        {
            stompTimer += Time.deltaTime;
            break;
        }

        if (Input.GetKeyUp(bounceKey))
        {
            stompTimer = 0;
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // start crouch
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // stop crouch
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }

        //stomp bounce
        if (Input.GetKeyDown(bounceKey) && !grounded && readyToBounce)
        {

            if (stompTimer > .05f)
            {
                Stomp();
            }
            else
            {
                Bounce();
            }
        }

        //stomp bounce
        if (Input.GetKeyDown(KeyCode.E) && !grounded)
        {
            AirDive();
        }

        while (isBouncing)
        {
            PlayerObj.SetActive(false);
            bounceBallObj.SetActive(true);
            trail.emitting = true;
            break;
        }
    }

    private void StateHandler()
    {
        // Mode - Crouching
        if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        // Mode - Sprinting
        else if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        // Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        // Mode - Air
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.linearVelocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
        else if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed *  10f * airMultiplier, ForceMode.Force);

        // turn gravity off while on slope
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        // limiting speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.linearVelocity.magnitude > moveSpeed)
                rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;
        }

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bounce Pad") && isBouncing) return;

        if (isBouncing)
        {
            rb.AddForce(transform.up * bounceForce * stompMultiplier);
            isBouncing = false;

            Invoke(nameof(ResetPlayerObj), 1f);
        }

        if (isStomping)
        {
            isStomping = false;
            Invoke(nameof(ResetPlayerObj), 1f);
        }
    }

    private void ResetPlayerObj()
    {
        PlayerObj.SetActive(true);
        bounceBallObj.SetActive(false);
        trail.emitting = false;
    }

    private void Bounce()
    {
        readyToBounce = false;
        isBouncing = true;
        rb.AddForce(Vector3.down * bounceForce, ForceMode.Impulse);
    }

    private void Stomp()
    {
        readyToStomp = false;
        isStomping = true;
        isBouncing = false;

    }

    public void AirDive()
    {
        PlayerObj.SetActive(false);
        bounceBallObj.SetActive(true);
        trail.emitting = true;
        rb.AddForce(((PlayerObj.transform.up * diveUpForce)+(PlayerObj.transform.forward * diveForwardForce)), ForceMode.Force);
        Invoke(nameof(ResetPlayerObj), 2f);
    }

    private void Jump()
    {
        exitingSlope = true;

        // reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}


