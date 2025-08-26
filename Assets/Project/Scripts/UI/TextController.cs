using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    [SerializeField, Tooltip("ccылка на текст с колличеством монет")]
    private TextMeshProUGUI CoinText;

    [SerializeField, Tooltip("ccылка на текст с ценой повышеня уровня")]
    private TextMeshProUGUI NextLevelPrice;

    [SerializeField, Tooltip("ccылка на текст с ценой повышеня шанса дропа экика")]
    private TextMeshProUGUI UpgradeEpicPriceText;
    [SerializeField, Tooltip("ccылка на текст с шансом дропа экика")]
    private TextMeshProUGUI EpicChanse;

    [SerializeField, Tooltip("ccылка на текст с ценой повышеня шанса дропа редкой")]
    private TextMeshProUGUI UpgradeRarPriceText;
    [SerializeField, Tooltip("ccылка на текст с шансом дропа редкой")]
    private TextMeshProUGUI RarChanse;

    [SerializeField, Tooltip("ccылка на префаб pop up текста")]
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
