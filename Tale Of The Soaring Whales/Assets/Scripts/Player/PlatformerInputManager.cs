using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerInputManager : MonoBehaviour
{
    [SerializeField]
    private PlayerInput PlayerInput;
    private PlatformerPlayerMovement _player_Movement;
    private PlatformerCollisionDetection _player_CollisionDetection;
    private PlatformerBounceStomp _bounce_Stomp;
    private PlatformerPlayerLeap _player_PlayerLeap;
    private PlatformerPlayerCrouch _player_Crouch;
    private PlayerCombat _player_Combat;

    private void Start()
    {
        PlayerInput = GetComponent<PlayerInput>();

        _player_Movement = GetComponent<PlatformerPlayerMovement>();

        _bounce_Stomp = GetComponent<PlatformerBounceStomp>();

        _player_CollisionDetection = GetComponent<PlatformerCollisionDetection>();

        _player_PlayerLeap = GetComponent<PlatformerPlayerLeap>();

        _player_Crouch = GetComponent<PlatformerPlayerCrouch>();

        _player_Combat = GetComponent<PlayerCombat>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MoveController(InputAction.CallbackContext move)
    {
        _player_Movement.move_Direction = move.ReadValue<Vector3>();
    }

    public void CrouchButton(InputAction.CallbackContext crouch)
    {
        if (crouch.performed)
        {
            _player_Crouch.OnCrouch(true);
        }
        else
        {
            _player_Crouch.OnCrouch(false);
        }
    }

    public void SprintButton(InputAction.CallbackContext sprint)
    {
        if (sprint.performed)
        {
            _player_Movement.EnablePlayerSprint(true);
        }
        else {
            _player_Movement.EnablePlayerSprint(false); 
        }
    }

    public void JumpButton(InputAction.CallbackContext jump)
    {
        if (jump.performed)
        {
            _player_Movement.OnJump();
        }
    }

    public void PlayerSimpleAttack(InputAction.CallbackContext spinAttack)
    {
        if (spinAttack.performed)
            _player_Combat.SetInput();
    }

    public void AirLeapButton(InputAction.CallbackContext air_Leap)
    {
        if (air_Leap.performed)
            _player_PlayerLeap.AirLeap();
    }
}
