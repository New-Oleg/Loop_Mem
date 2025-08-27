using System;
using UnityEngine;

public class Character : Unit //можно разморозить реген позже
{

    public static event Action<int?, int?, int?, float?> OnStatChanget;


    private int RegenCoef = 0;
    protected override void OnEnable()
    {
        base.OnEnable();
        UpgradeCard.upgradeHp += UpradeHP;
        UpgradeCard.upradeAttackSpeed += UpradeAttackSpeed;
        UpgradeCard.upradeDamage += UpradeDamage;
        //UpgradeCard.upradeRagenHealts += UpgradeRegen;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        UpgradeCard.upgradeHp -= UpradeHP;
        UpgradeCard.upradeAttackSpeed -= UpradeAttackSpeed;
        UpgradeCard.upradeDamage -= UpradeDamage;
        //UpgradeCard.upradeRagenHealts -= UpgradeRegen;
    }


    private void Start()
    {
        //InvokeRepeating(nameof(InvockeRegenHealts), 1, 1); 
        OnStatChanget.Invoke(MaxHealts, Healts, Damage, AttackSpeed);
    }

    //private void UpgradeRegen(int R)
    //{
    //    RegenCoef += R;
    //}

    //private void InvockeRegenHealts()
    //{
    //    RegenHealts(RegenCoef);
    //}


    public override void RegenHealtsOnEndTurn()
    {
        base.RegenHealtsOnEndTurn();
        OnStatChanget.Invoke(MaxHealts, Healts, null, null);
    }

    public override void TakeDamage(int D)
    {
        base.TakeDamage(D);
        OnStatChanget.Invoke(MaxHealts, Healts,null,null);
    }
    public override void UpradeHP(int bonus)
    {
        base.UpradeHP(bonus);
        OnStatChanget.Invoke(MaxHealts, Healts, null, null);
    }
    public override void UpradeDamage(int bonus)
    {
        base.UpradeDamage(bonus);
        OnStatChanget.Invoke(null, null, Damage, null);
    }
    public override void UpradeAttackSpeed(float bonus)
    {
        base.UpradeAttackSpeed(bonus);
        OnStatChanget.Invoke(null, null, null, AttackSpeed);
    }
}
