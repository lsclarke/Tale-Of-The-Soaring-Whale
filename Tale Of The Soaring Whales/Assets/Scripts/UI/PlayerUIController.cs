using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField]
    private PlayerInventory playerInventory;

    [SerializeField]
    private PlatformerPlayerManager _player_Platformer_Manager;

    public TextMeshProUGUI ScoreText;

    public TextMeshProUGUI CoinText;

    public TextMeshProUGUI KeysText;

    public TextMeshProUGUI StarText;

    public Slider healthBarSlider;


    public TextMeshProUGUI HealthText;

    public TextMeshProUGUI StaminaText;

    public Slider staminaBarSlider;

    private void Start()
    {
        playerInventory.Points = 0;
        playerInventory.Coins = 0;
        //playerInventory.Keys = 0;
        //playerInventory.Stars = 0;

    }

    private void Update()
    {
        HealthText.text = $"Health: {_player_Platformer_Manager.Health}";
        StaminaText.text = $"Stamina: {(int)_player_Platformer_Manager.Stamina}";

        ScoreText.text = $"Score x {playerInventory.Points}";
        CoinText.text = $"Coins x {playerInventory.Coins}";
        //KeysText.text = $"Keys x {playerInventory.Keys}";
        //StarText.text = $"Stars x {playerInventory.Stars}";

        healthBarSlider.value = _player_Platformer_Manager.Health;

        staminaBarSlider.value = _player_Platformer_Manager.Stamina;
    }
}
