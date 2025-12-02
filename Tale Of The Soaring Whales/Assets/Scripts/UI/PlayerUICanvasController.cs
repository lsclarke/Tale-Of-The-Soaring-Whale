using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUICanvasController : MonoBehaviour
{
    [SerializeField]
    private PlayerInventory playerInventory;

    [SerializeField]
    private PlatformerPlayerManager _player_Platformer_Manager;

    public TextMeshProUGUI CoinText;

    public TextMeshProUGUI GemsText;

    public Image healthBarSlider;

    public Image staminaBarSlider;

    private void Start()
    {
        playerInventory.Points = 0;
        playerInventory.Coins = 0;
        playerInventory.Gems = 0;
        playerInventory.Memories = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CoinText.text = $"{playerInventory.Coins}";
        GemsText.text = $"{playerInventory.Gems}";

        staminaBarSlider.fillAmount = _player_Platformer_Manager.Stamina;


    }
}
