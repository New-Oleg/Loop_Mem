using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    [SerializeField, Tooltip("cc���� �� ����� � ������������ �����")]
    private TextMeshProUGUI CoinText;

    [SerializeField, Tooltip("cc���� �� ����� � ����� �������� ������")]
    private TextMeshProUGUI NextLevelPrice;

    [SerializeField, Tooltip("cc���� �� ����� � ����� �������� ����� ����� �����")]
    private TextMeshProUGUI UpgradeEpicPriceText;
    [SerializeField, Tooltip("cc���� �� ����� � ������ ����� �����")]
    private TextMeshProUGUI EpicChanse;

    [SerializeField, Tooltip("cc���� �� ����� � ����� �������� ����� ����� ������")]
    private TextMeshProUGUI UpgradeRarPriceText;
    [SerializeField, Tooltip("cc���� �� ����� � ������ ����� ������")]
    private TextMeshProUGUI RarChanse;

    [SerializeField, Tooltip("cc���� �� ������ pop up ������")]
    private TextMeshProUGUI DamageText;

    private void OnEnable()
    {
        ShopController.OnCoinChannget += UpdateUICoin;
        ShopController.OnPriceChannget += UpdatePriceText;
        CardDroper.ChangeChance += UpdateChanceText;
        Unit.OnTakeDamage += PopUpDamage;
    }

    private void OnDisable()
    {
        ShopController.OnCoinChannget -= UpdateUICoin;
        ShopController.OnPriceChannget -= UpdatePriceText;
        CardDroper.ChangeChance -= UpdateChanceText;
        Unit.OnTakeDamage -= PopUpDamage;
    }

    private void UpdateUICoin(int c)
    {
        CoinText.text = "" + c;
    }

    private void PopUpDamage(int d, Transform t)
    {
        TextMeshProUGUI DT = Instantiate(DamageText, t.transform.position, Quaternion.identity , transform);
        DT.text = "" + d;
        DT.transform.DOMoveY(t.position.y + 1, 1).OnComplete(() => Destroy(DT.gameObject));
    }

    private void UpdatePriceText(int? epic, int? rar, int? level)
    {
        if (epic != null) UpgradeEpicPriceText.text = epic + "";
        if (rar != null) UpgradeRarPriceText.text = rar + "";
        if (level != null) NextLevelPrice.text = level + "";
    }
    private void UpdateChanceText(float? epic, int? rar)
    {
        if (epic != null) EpicChanse.text = epic + "";
        if (rar != null) RarChanse.text = rar + "";
    }

}
