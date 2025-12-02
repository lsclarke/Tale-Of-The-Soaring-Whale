using UnityEngine;

public class PlatformerPlayerManager : MonoBehaviour
{
    //--Component Reference variables

    [SerializeField]
    private PlatformerPlayer _player_Platformer;

    [Header("Player Stats")]
    [Space(10f)]

    [Range(0f, 100f)]
    public float Health;
    private float MaxHealth = 100f;

    [Range(0f, 100f)]
    public float Stamina;
    private float MaxStamina = 1f;

    public float Speed;
    private float MaxSpeed = 7f;

    public float AttackPower;
    private float MaxAttackPower = 10f;


    // Returns max player mana
    public float GetMaxPlayerStamina()
    {
        return MaxStamina;
    }

    // Returns max player health
    public float GetMaxPlayerHealth()
    {
        return MaxHealth;
    }

    // Returns max player movement speed
    public float GetMaxPlayerSpeed()
    {
        return MaxSpeed;
    }

    public float InstantSubtractHealth(float amount)
    {
        Health -= amount;

        if (Health <= 0)
            Health = 0;
        return Health;
    }

    public float InstantAddHealth(float amount)
    {
        Health += amount;

        if (Health >= MaxHealth)
            Health = MaxHealth;
        return Health;
    }
}
