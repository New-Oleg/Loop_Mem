using UnityEngine;

public class Character : Unit
{
    private int RegenCoef = 0;
    protected override void OnEnable()
    {
        base.OnEnable();
        UpgradeCard.upgradeHp += UpradeHP;
        UpgradeCard.upradeAttackSpeed += UpradeAttackSpeed;
        UpgradeCard.upradeDamage += UpradeDamage;
        UpgradeCard.upradeRagenHealts += UpgradeRegen;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        UpgradeCard.upgradeHp -= UpradeHP;
        UpgradeCard.upradeAttackSpeed -= UpradeAttackSpeed;
        UpgradeCard.upradeDamage -= UpradeDamage;
        UpgradeCard.upradeRagenHealts -= UpgradeRegen;
    }


    private void Start()
    {
        InvokeRepeating(nameof(InvockeRegenHealts), 1, 1);
    }

    private void UpgradeRegen(int R)
    {
        RegenCoef += R;
    }

    private void InvockeRegenHealts()
    {
        RegenHealts(RegenCoef);
    }

}
