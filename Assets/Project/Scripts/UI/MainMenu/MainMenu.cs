using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class MainMenu : MonoBehaviour
{
    [SerializeField] //������������ ������ � ������ ������ �� ������
    private TextMeshProUGUI BestScoreText;

    [SerializeField] //������������ ������ � ����� ����� �� ������
    private TextMeshProUGUI CoinText;

    [SerializeField] //������������ ������ � ���� �������� �������� ������ �� ������
    private TextMeshProUGUI UpgradeWalkSpeedPriceText;
    [SerializeField] //������������ ������ � ������ �������� ������ �� ������
    private TextMeshProUGUI LevelWalkSpeedPriceText;

    [SerializeField] //������������ ������ � ���� �������� ������ �� ������
    private TextMeshProUGUI UpgradeCharacterRegenPriceText;
    [SerializeField] //������������ ������ � ������ ������ �� ������
    private TextMeshProUGUI LevelCharacterRegenPriceText;


    [SerializeField, Tooltip("������ ��������� ��� ��������")]
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