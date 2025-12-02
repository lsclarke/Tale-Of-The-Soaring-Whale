using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatformerPlayerMovement : MonoBehaviour
{
    //--Component Reference variables
    
    [SerializeField]
    private Transform Orientation;

    private PlatformerPlayerManager _player_Manager;

    private PlatformerPlayerCrouch _player_Crouch;

    private PlatformerCollisionDetection _player_Collision_Detection;

    private Rigidbody _rigidbody;

    public GameObject staminaBar;

    [HideInInspector]
    public Vector3 move_Direction;

    private float move_Speed;

    private float player_Stamina;

    private float original_Speed; 

    private float input_Horizontal;

    private float input_Vertical;

    private bool isGrounded;

    public bool is_Walking;

    public bool is_Sprinting;

    public float speed_Multiplier;

    public float air_Speed_Multiplier;



    public float jump_Force;

    public bool can_Jump;

    public bool is_Jumping;

    [SerializeField]
    private CinemachineCamera _cinemachine_Camera;

    public PlatformerPlayerMovement()
    {

    }

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _player_Collision_Detection = GetComponent<PlatformerCollisionDetection>();

        _player_Manager = GetComponent<PlatformerPlayerManager>();

        _player_Crouch = GetComponent<PlatformerPlayerCrouch>();

        move_Speed = _player_Manager.Speed;

        player_Stamina = _player_Manager.Stamina;


        original_Speed = move_Speed;
    }

    public void MovePlayer()
    {
        isGrounded = _player_Collision_Detection.OnGround();

        move_Speed = _player_Manager.Speed;

        player_Stamina = _player_Manager.Stamina;

        input_Horizontal = Input.GetAxis("Horizontal");

        input_Vertical = Input.GetAxis("Vertical");

        move_Direction = Orientation.forward * input_Vertical + Orientation.right * input_Horizontal;

        if (_player_Collision_Detection.OnSlope() && !_player_Collision_Detection.is_Leaving_Slope)
        {
            _rigidbody.AddForce(_player_Collision_Detection.GetSlopeMovementDirection(move_Direction) * move_Speed * 25f, ForceMode.Force);

            if (_rigidbody.linearVelocity.y > 0)
                _rigidbody.AddForce(Vector3.down * 80f, ForceMode.Force);
        }
        else if (_player_Collision_Detection.OnGround())
            _rigidbody.AddForce(move_Direction.normalized * move_Speed * speed_Multiplier, ForceMode.Force);

        else if (!_player_Collision_Detection.OnGround())
            _rigidbody.AddForce(move_Direction.normalized * move_Speed * speed_Multiplier * air_Speed_Multiplier, ForceMode.Force);

        // turn gravity off while on slope
        _rigidbody.useGravity = !_player_Collision_Detection.OnSlope();

        CalculateMoveSpeed();


    }

    public Vector3 CalculateMoveSpeed()
    {
        if (isGrounded)
        {
            is_Walking = true;

            if (is_Sprinting && ((Mathf.Abs(_rigidbody.linearVelocity.z) > 0f) && (Mathf.Abs(_rigidbody.linearVelocity.z) > 0f)))
            {
                is_Walking = false;
                _player_Manager.Stamina -= Time.deltaTime * 0.5f;
                IncrementSpeed();
            }
            else if(!is_Sprinting)
            {
                is_Walking = true;
                _player_Manager.Stamina += Time.deltaTime;
                DencrementSpeed();
            }
        }
        if(is_Walking)
        {
            _player_Manager.Speed = original_Speed;
        }


        if (_player_Collision_Detection.OnSlope() && !_player_Collision_Detection.is_Leaving_Slope)
        {
            if (_rigidbody.linearVelocity.magnitude > move_Speed)
                _rigidbody.linearVelocity = _rigidbody.linearVelocity.normalized * move_Speed;
        }
        else
        {
            Vector3 flat_Surface_Velocity = new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z);

            if (flat_Surface_Velocity.magnitude > move_Speed)
            {
                Vector3 limited_Velocity = flat_Surface_Velocity.normalized * move_Speed;
                _rigidbody.linearVelocity = new Vector3(limited_Velocity.x, _rigidbody.linearVelocity.y, limited_Velocity.z);
            }
        }
        return _rigidbody.linearVelocity;
    }

    public float IncrementSpeed()
    {
        if ((is_Sprinting) && (_player_Manager.Stamina > 0.0f))
        {
            staminaBar.SetActive(true);
            _cinemachine_Camera.Lens.FieldOfView += 0.5f;

            if (_cinemachine_Camera.Lens.FieldOfView > 75.0f)
                _cinemachine_Camera.Lens.FieldOfView = 75.0f;

            if (_player_Manager.Speed < _player_Manager.GetMaxPlayerSpeed())
            {
                _player_Manager.Speed += 1f;
                
                if (_player_Manager.Speed >= _player_Manager.GetMaxPlayerSpeed())
                {
                    _player_Manager.Speed = _player_Manager.GetMaxPlayerSpeed();
                }

                if (_player_Manager.Stamina >= _player_Manager.GetMaxPlayerStamina())
                {
                    _player_Manager.Stamina = _player_Manager.GetMaxPlayerStamina();
                }
            }

        }

        if (player_Stamina <= 0)
        {
            player_Stamina = 0;
            is_Sprinting = false;
        }
        return move_Speed;
    }

    public float DencrementSpeed()
    {
        if (!is_Sprinting)
        {

            staminaBar.SetActive(false);
            _cinemachine_Camera.Lens.FieldOfView -= 1.0f;

            if (_cinemachine_Camera.Lens.FieldOfView <= 60.0f)
                _cinemachine_Camera.Lens.FieldOfView = 60.0f;

            if (_player_Manager.Speed > original_Speed)
            {
                _player_Manager.Speed-= 0.25f;
                if (_player_Manager.Speed <= original_Speed)
                {
                    _player_Manager.Speed = original_Speed;

                }
            }

            if (_player_Manager.Stamina >= _player_Manager.GetMaxPlayerStamina())
            {
                _player_Manager.Stamina = _player_Manager.GetMaxPlayerStamina();
            }
        }
        return move_Speed;
    }

    public bool EnablePlayerSprint(bool On)
    {
        is_Sprinting = On;
        return is_Sprinting;
    }

    public void OnJump()
    {
        if (can_Jump)
        {
            // reset y velocity
            _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z);

            _rigidbody.AddForce(transform.up * jump_Force, ForceMode.Impulse);

            can_Jump = false;

            is_Jumping = true;

            _player_Collision_Detection.is_Leaving_Slope = true;

            Invoke(nameof(ResetJump), 0.25f);
        }
        else return;
    }

    public Rigidbody GetRigidbody()
    {
        return _rigidbody;
    }

    public void ResetJump()
    {
        can_Jump = true;
        is_Jumping = false;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
}
