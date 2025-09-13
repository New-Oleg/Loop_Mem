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


    [SerializeField, Tooltip("ccылка на текст с хп героя (запись в формате Max/Now)")]
    private TextMeshProUGUI HealtsText;
    [SerializeField, Tooltip("ccылка на текст с уроном героя")]
    private TextMeshProUGUI CharacterDamageText;
    [SerializeField, Tooltip("ccылка на текст со скоростю атаки")]
    private TextMeshProUGUI CharacterAtackSpeedText;


    [SerializeField, Tooltip("ccылка на панкль с инфой о тайле")]
    private GameObject RoadInfoPanel;
    [SerializeField, Tooltip("ccылка на текст с именем")]
    private TextMeshProUGUI RoadNameText;
    [SerializeField, Tooltip("ccылка на текст с уровнем монстра")]
    private TextMeshProUGUI MonserInRoadLevelText;
    [SerializeField, Tooltip("ccылка на текст с хп монстра (запись в формате Max/Now)")]
    private TextMeshProUGUI MonsterInRoadHpText;
    [SerializeField, Tooltip("ccылка на текст со уроном атаки монстра")]
    private TextMeshProUGUI MonsterInRoadDamageText;
    [SerializeField, Tooltip("ccылка на текст со скоростю атаки монстра")]
    private TextMeshProUGUI MonsterInRoadAtackSpeedText;

    [SerializeField, Tooltip("ccылка на текст с счетчиком уровней")]
    private TextMeshProUGUI LevelCountText;

    [SerializeField, Tooltip("ccылка на префаб pop up текста")]
    private TextMeshProUGUI DamageText;

    private void OnEnable()
    {
        ShopController.OnCoinChannget += UpdateUICoin;
        ShopController.OnPriceChannget += UpdatePriceText;
        CardDroper.ChangeChance += UpdateChanceText;
        Unit.OnTakeDamage += PopUpDamage;
        Character.OnStatChanget += UpdateCharacterStat;
        Tail.GetRoadInfo += ShowRoadInnfo;
        Tail.UnshowInfo += UnshowRoadInnfo;
        ShopController.GenerateNewLevel += LevelCounter;
    }

    private void OnDisable()
    {
        ShopController.OnCoinChannget -= UpdateUICoin;
        ShopController.OnPriceChannget -= UpdatePriceText;
        CardDroper.ChangeChance -= UpdateChanceText;
        Unit.OnTakeDamage -= PopUpDamage;
        Character.OnStatChanget -= UpdateCharacterStat;
        Tail.GetRoadInfo -= ShowRoadInnfo;
        Tail.UnshowInfo -= UnshowRoadInnfo;
        ShopController.GenerateNewLevel -= LevelCounter;
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

    private void UpdateCharacterStat(int? maxHp, int? nowHp,int? Damage, float? AtackSpeed)
    {

        if (maxHp != null) HealtsText.text = (nowHp > 0) ? maxHp + " / " + nowHp : "Death";
        if (Damage != null) CharacterDamageText.text = Damage + "";
        if (AtackSpeed != null) CharacterAtackSpeedText.text = AtackSpeed + "";
    }



    private void ShowRoadInnfo(Monster m)
    {

        RoadInfoPanel.SetActive(true);
        RoadNameText.text = m.gameObject.name.Replace("(Clone)", "").Trim();
        MonserInRoadLevelText.text = "Level: " +( Monster.Level + m.Rare); 
        MonsterInRoadHpText.text = (m.Healts > 0) ? "MaxHp/MinHp:\n" + m.MaxHealts + "/" + m.Healts : "Death";
        MonsterInRoadDamageText.text = "Damage: " + m.Damage;
        MonsterInRoadAtackSpeedText.text = "Atack Speed: " + m.AttackSpeed;
    }


    private void UnshowRoadInnfo() 
    {
        CancelInvoke(nameof(ShowRoadInnfo));
        RoadInfoPanel.SetActive(false);
    }

    private void LevelCounter()
    {
        LevelCountText.text = (int.Parse(LevelCountText.text) + 1).ToString();
    }
}
