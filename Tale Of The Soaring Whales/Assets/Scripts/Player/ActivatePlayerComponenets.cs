using UnityEngine;
using UnityEngine.InputSystem;

public class ActivatePlayerComponenets : MonoBehaviour
{
    public PlayerInput playerInput;
    public PlatformerInputManager platformerInputManager;
    public PlatformerPlayerManager platformerPlayerManager;
    public PlatformerPlayerMovement playerMovement;
    public PlatformerCollisionDetection collisionDetection; 
    public DialogueSystem dialogueSystem;

    // Update is called once per frame
    void Update()
    {
        if(dialogueSystem.enablePlayer)
        {
            playerInput.enabled = true;
            platformerInputManager.enabled = true;
            platformerPlayerManager.enabled = true;
            playerMovement.enabled = true;
            collisionDetection.enabled = true;
        }
        else
        {
            playerInput.enabled = false;
            platformerInputManager.enabled = false;
            platformerPlayerManager.enabled = false;
            playerMovement.enabled = false;
            collisionDetection.enabled = false;
        }
    }
}
