using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ShopController : MonoBehaviour
{

    public static event Action GenerateNewLevel;
    public static event Action<int> OnCoinChannget;
    public static event Action<int?, int?, int?> OnPriceChannget;

    public UnityEvent UpgradeRarChanse;
    public UnityEvent UpgradeEpicChanse;


    private int _coin;

    private int _priceLevelUp = 100;
    private int _priceUpgradeRarCanse = 50;
    private int _priceUpgradeEpicCanse = 65;

    private void OnEnable()
    {
        RoadWalker.DropCard += AddCoin;
        FightController.GetRevard += TackeRevardCoin;
        UpgradeCard.AddCoin += TackeRevardCoin;
    }
    private void OnDisable()
    {
        RoadWalker.DropCard -= AddCoin;
        FightController.GetRevard -= TackeRevardCoin;
        UpgradeCard.AddCoin -= TackeRevardCoin;
    }

    private void AddCoin()
    {
        _coin += 25; // заредачить
        OnCoinChannget.Invoke(_coin);
    }

    private void TackeRevardCoin(int c)
    {
        _coin += c;
        OnCoinChannget.Invoke(_coin);
    }

    [ContextMenu("NewLevel")]
    public void NewLevel()
    {
        if (_coin >= _priceLevelUp)
        {
            _coin -= _priceLevelUp;
            _priceLevelUp += _priceLevelUp / 4;
            GenerateNewLevelInvoke();
            OnPriceChannget.Invoke(null, null, _priceLevelUp);
            OnCoinChannget.Invoke(_coin);
        }
    }

    [ContextMenu("BuyUpgradeRarChanse")]
    public void BuyUpgradeRarChanse()
    {
        if (_coin >= _priceUpgradeRarCanse)
        {
            _coin -= _priceUpgradeRarCanse;
            _priceUpgradeRarCanse += _priceUpgradeRarCanse / 2;
            UpgradeRarChanse.Invoke();
            OnPriceChannget.Invoke(null, _priceUpgradeRarCanse, null);
            OnCoinChannget.Invoke(_coin);
        }
    }

    [ContextMenu("BuyUpgradeEpicChanse")]
    public void BuyUpgradeEpicChanse()
    {
        if (_coin >= _priceUpgradeEpicCanse)
        {
            _coin -= _priceUpgradeEpicCanse;
            _priceUpgradeEpicCanse += _priceUpgradeEpicCanse / 2;
            UpgradeEpicChanse.Invoke();
            OnPriceChannget.Invoke(_priceUpgradeEpicCanse, null, null);
            OnCoinChannget.Invoke(_coin);
        }
    }

    private void GenerateNewLevelInvoke()
    {
        for (int i = GenerateNewLevel.GetInvocationList().Length - 1; i >= 0; i--)
        {
            ((Action)GenerateNewLevel.GetInvocationList()[i])();
        }
    }
}
