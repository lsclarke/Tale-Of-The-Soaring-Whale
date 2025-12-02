using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public PlatformerPlayerMovement movement;
    public PlatformerCollisionDetection collisionDetection;
    public PlayerCombat playerCombat;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        playerCombat.inputPressed = true;
        movement.GetRigidbody().useGravity = false;
        playerCombat.canAttack = false;
        playerCombat.isAttacking = true;
        playerCombat.attackTrail.gameObject.SetActive(true);
    }

    public void EndAttack()
    {
        playerCombat.inputPressed = false;
        playerCombat.canAttack = true;
        movement.GetRigidbody().useGravity = true;
        playerCombat.isAttacking = false;
        playerCombat.attackTrail.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (movement != null)
        {
            if (movement.is_Sprinting && !playerCombat.isAttacking) animator.SetTrigger("Swim2");

            if(!movement.is_Sprinting && collisionDetection.OnGround() && !playerCombat.inputPressed) animator.SetTrigger("Swim1");

            if (playerCombat.isAttacking)
            {
                movement.GetRigidbody().useGravity = false;
            }
        }
        else { return; }
    }
}
