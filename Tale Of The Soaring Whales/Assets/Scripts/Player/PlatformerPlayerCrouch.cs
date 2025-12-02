using UnityEngine;

public class PlatformerPlayerCrouch : MonoBehaviour
{
    [Header("Crouching")]

    private Rigidbody _rigidbody;

    private PlatformerPlayerMovement _player_Movement;

    private PlatformerCollisionDetection _player_CollisionDetection;

    private PlatformerBounceStomp _bounce_Stomp;

    private PlatformerPlayerLeap _player_PlayerLeap;

    public float crouchSpeed;

    public float crouchYScale;

    private float startYScale;

    public bool isCrouching;



    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _bounce_Stomp = GetComponent<PlatformerBounceStomp>();

        _player_PlayerLeap = GetComponent<PlatformerPlayerLeap>();

        _player_Movement = GetComponent<PlatformerPlayerMovement>();

        startYScale = transform.localScale.y;
    }

    public bool OnCrouch(bool On)
    {
        isCrouching = On;
        return isCrouching;
    }

    private void FixedUpdate()
    {
        if (isCrouching)
        {
            Debug.Log("CROUCH");
            _player_PlayerLeap.readyToLeap = false;
            _player_Movement.is_Sprinting = false;
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            _rigidbody.AddForce(Vector3.down * .5f, ForceMode.Impulse);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }
}
