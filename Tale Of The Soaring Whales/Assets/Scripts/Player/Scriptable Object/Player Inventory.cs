using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInventory", menuName = "Scriptable Objects/PlayerInventory")]
public class PlayerInventory : ScriptableObject
{
    public int Coins { get; set; }

    public int Gems { get; set; }

    public int Memories { get; set; }

    public float Points { get; set; }
}
