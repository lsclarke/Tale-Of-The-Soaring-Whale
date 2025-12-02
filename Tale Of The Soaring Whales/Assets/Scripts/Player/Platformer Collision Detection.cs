using UnityEngine;

public class PlatformerCollisionDetection : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;

    private PlatformerPlayerMovement _player_Movement;
    private PlatformerBounceStomp _bounce_Stomp;
    private PlatformerPlayerLeap _player_Leap;

    [SerializeField]
    private bool is_Grounded;

    public float detection_Ray_Length;

    public LayerMask ground_LayerMask;

    public float ground_Drag;

    private bool is_On_Slope;

    public bool is_Leaving_Slope;

    private RaycastHit _slopeHit;

    public float maxSlopeAngle;

    private Transform originalTransformParent;

    private Transform newTransformParent;

    public PlatformerCollisionDetection()
    {

    }

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _player_Movement = GetComponent<PlatformerPlayerMovement>();

        _bounce_Stomp = GetComponent<PlatformerBounceStomp>();

        _player_Leap = GetComponent<PlatformerPlayerLeap>();
    }

    public bool OnGround()
    {
        is_Grounded = Physics.Raycast(transform.position, Vector3.down, detection_Ray_Length, ground_LayerMask);

        Debug.DrawLine(this.transform.position, new Vector3(transform.position.x, transform.position.y - detection_Ray_Length, transform.position.z), Color.yellow);

        if (is_Grounded)
        {
            _player_Movement.ResetJump();
            _player_Movement.can_Jump = true;
           // _bounce_Stomp.readyToBounce = false;
            _player_Leap.readyToLeap = false;
            _player_Leap.isLeaping = false;

            if (!_player_Leap.isLeaping)
                _player_Leap.readyToLeap = true;

        }
        else
        {
            _player_Movement.can_Jump = false;

            //if(!_bounce_Stomp.isBouncing)
            //    _bounce_Stomp.readyToBounce = true;
            if(!_player_Leap.isLeaping)
                _player_Leap.readyToLeap = true;
        }

        CalculateGroundMovementDrag();

        return is_Grounded;
    }

    public float CalculateGroundMovementDrag()
    {
        if (is_Grounded)
            _rigidbody.linearDamping = ground_Drag;

        if (!is_Grounded)
            _rigidbody.linearDamping = 0;

        return _rigidbody.linearDamping;
    }

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, detection_Ray_Length, ground_LayerMask))
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);

            if ((angle > 0) && (angle <= maxSlopeAngle))
            {
                is_On_Slope = true;
                Debug.Log($"Slope Angle: {angle}");
            }

        }
        else
        {
            is_On_Slope = false;
        }
        return is_On_Slope;
    }


    public bool IsExitingSlope(bool On)
    {
        if (On) is_Leaving_Slope = true;

        if (!On) is_Leaving_Slope = false;

        return is_Leaving_Slope;
    }

    public Vector3 GetSlopeMovementDirection(Vector3 movementDirection)
    {
        return Vector3.ProjectOnPlane(movementDirection, _slopeHit.normal).normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        originalTransformParent = transform.parent;

        if (collision.gameObject.CompareTag("Moving Platform"))
        {
            newTransformParent = collision.gameObject.transform;
            this.transform.parent = newTransformParent;
        }
        else
        {
            return;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        this.transform.parent = originalTransformParent;

        if (collision.gameObject.CompareTag("Moving Platform"))
        {
            this.transform.parent = null;
        }
    }


    private void FixedUpdate()
    {
        OnGround();
    }

}
