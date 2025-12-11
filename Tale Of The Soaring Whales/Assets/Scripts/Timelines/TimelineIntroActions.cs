using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimelineIntroActions : MonoBehaviour
{
    public GameObject DialogueCanvasUI;
    public DialogueSystem dialogueSystem;

    public PlayerInput playerInput;
    public PlatformerInputManager inputManager;
    public PlatformerPlayerManager playerManager;
    public PlatformerPlayerMovement playerMovement;
    public CinemachineBrain brain;

    public float waitTime = 0f;


    private void Start()
    {
        Invoke("ActivateUI",waitTime);
        brain.enabled = false;
        playerInput.enabled = false;
        inputManager.enabled = false;
        playerMovement.enabled = false;
        playerManager.enabled = false;
    }

    public void ActivateUI()
    {
        DialogueCanvasUI.SetActive(true);
    }

    public void EnablePlayerFunctions()
    {
        if(dialogueSystem != null)
        {
            brain.enabled = true;
            playerInput.enabled = true;
            inputManager.enabled = true;
            playerMovement.enabled = true;
            playerManager.enabled = true;
        }
    }

}
