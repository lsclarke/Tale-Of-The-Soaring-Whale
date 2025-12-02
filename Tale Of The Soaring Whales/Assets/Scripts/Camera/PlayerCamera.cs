using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    [Header("References")]

    public Transform player;

    public Transform playerObj;

    public Transform Orientation;

    public Rigidbody physic;

    public float rotationSpeed;

    private void Start()
    {
        //Disable Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void CameraDirection()
    {
        //Orientation rotation

        ///viewDirection equals the difference between the player postion and the cameras postion (excluding the Y position)
        ///then Orientation applys that to the forward direction
        Vector3 viewDirection = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        Orientation.forward = viewDirection.normalized;

        //rotate player

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDirection = Orientation.forward * verticalInput + Orientation.right * horizontalInput;

        //If the player has an input/ moving
        if(inputDirection != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDirection.normalized ,rotationSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        CameraDirection();
    }
}
