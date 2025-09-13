using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class MainMenu : MonoBehaviour
{
    [SerializeField] //подт€гиваетт данные о лутшем заходе из сейвов
    private TextMeshProUGUI BestScoreText;

    [SerializeField] //подт€гиваетт данные о числе ден€г из сейвов
    private TextMeshProUGUI CoinText;

    [SerializeField] //подт€гиваетт данные о цене улучшен€ скорости ходьбы из сейвов
    private TextMeshProUGUI UpgradeWalkSpeedPriceText;
    [SerializeField] //подт€гиваетт данные о уровне скорости ходьбы из сейвов
    private TextMeshProUGUI LevelWalkSpeedPriceText;

    [SerializeField] //подт€гиваетт данные о цене улучшен€ регена из сейвов
    private TextMeshProUGUI UpgradeCharacterRegenPriceText;
    [SerializeField] //подт€гиваетт данные о уровне регена из сейвов
    private TextMeshProUGUI LevelCharacterRegenPriceText;


    [SerializeField, Tooltip("префаб чарактера дл€ прокачки")]
    private GameObject CharacterPref;


    private void Start()
    {
        CoinText.text = YG2.saves.Coin + "";
        BestScoreText.text = YG2.saves.BestScore + "";
        UpgradeWalkSpeedPriceText.text = YG2.saves.UpgradeWalkSpeedPrice + "";
        LevelWalkSpeedPriceText.text = YG2.saves.CharacterMoveSpeed + "";
        UpgradeCharacterRegenPriceText.text = YG2.saves.UpgradeCharacterRegenPrice + "";
        LevelCharacterRegenPriceText.text = YG2.saves.CharacterRegen + "";

    }

    public void GoToGameplayScen()
    {
        SceneManager.LoadScene("GameScen");
    }

    //--------------------------------------------

    public void CharacterUpgradeWalkSpeed()
    {
        int price = YG2.saves.UpgradeWalkSpeedPrice;
        if (YG2.saves.Coin >= price)
        {
            RoadWalker roadWalker = CharacterPref.GetComponent<RoadWalker>();
            roadWalker.moveSpeed += 0.5f;

            YG2.saves.CharacterMoveSpeed = roadWalker.moveSpeed;
            YG2.saves.Coin -= price;
            YG2.saves.UpgradeWalkSpeedPrice += price / 3;

            YG2.SaveProgress();

            UpgradeWalkSpeedPriceText.text = YG2.saves.UpgradeWalkSpeedPrice + "";
            LevelWalkSpeedPriceText.text = YG2.saves.CharacterMoveSpeed + "";
            CoinText.text = YG2.saves.Coin + "";
        }
    }

    public void CharacterRegenUpgrade()
    {
        int price = YG2.saves.UpgradeCharacterRegenPrice;

        if (YG2.saves.Coin >= price)
        {
            Character character = CharacterPref.GetComponent<Character>();
            character.UpgradeRegen(1);

            YG2.saves.CharacterRegen = character.RegenCoef;
            YG2.saves.Coin -= price;
            YG2.saves.UpgradeCharacterRegenPrice += price / 3;

            YG2.SaveProgress();

            UpgradeCharacterRegenPriceText.text = YG2.saves.UpgradeCharacterRegenPrice + "";
            LevelCharacterRegenPriceText.text = YG2.saves.CharacterRegen + "";
            CoinText.text = YG2.saves.Coin + "";
        }
    }

}