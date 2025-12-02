using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool inputPressed;
    public bool canAttack = true;
    public bool isAttacking;

    public float coolDownTime;
    private float coolDownTimer;

    public TrailRenderer attackTrail;

    private void Start()
    {
        coolDownTimer = 0f;
    }

    public void SetInput()
    {
        
    }

    public void inputCheck()
    {
        if (!inputPressed)
        {
            isAttacking = false;
        }
        else if (inputPressed && canAttack)
        {
            coolDownTimer = coolDownTime;
            coolDownTimer -= Time.deltaTime;
            canAttack = false;
            isAttacking =true;

            if(coolDownTime <= 0)
            {
                coolDownTimer = 0f;
                canAttack = true;
                isAttacking = false;
                inputPressed = false;
            }
        }
    }

    public void EndAttack()
    {
        coolDownTimer = 0f;
        canAttack = true;
        isAttacking = false;
        inputPressed = false;
    }

    private void FixedUpdate()
    {
        inputCheck();


    }
}
