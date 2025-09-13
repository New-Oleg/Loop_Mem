using System;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeCard : ACard
{
    public static event Action<int> upgradeHp;
    public static event Action<float> upradeAttackSpeed;
    public static event Action<int> upradeDamage;

    public static event Action<int> AddCoin;

    public void InvoikeUpgradeHp(int HpBonus)
    {
        upgradeHp.Invoke(HpBonus);
        CardSelected();
    }
    public void InvoikeUpgradeAttackSpeed(float AttackSpeedBonus)
    {
        upradeAttackSpeed.Invoke(AttackSpeedBonus);
        CardSelected();
    }
    public void InvoikeUpgradeDamage(int DamageBonus)
    {
        upradeDamage.Invoke(DamageBonus);
        CardSelected();
    }
    public void InvokeAddCoin(int Coin)
    {
        AddCoin.Invoke(Coin);
        CardSelected();
    }
}