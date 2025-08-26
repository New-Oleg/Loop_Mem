using System;
using UnityEngine;

public class Monster : Unit
{

    public static int Level = 1;

    [Tooltip("Уровень редкости моба, от 1")]
    public int Rare;

    [Tooltip("Колличество монет за моба")]
    public int Reward;

    protected override void OnEnable()
    {
        base.OnEnable();

        RoadWalker.DropCard += LevelUpgrade;

        ShopController.GenerateNewLevel += NewGameLevel;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        RoadWalker.DropCard -= LevelUpgrade;

        ShopController.GenerateNewLevel -= NewGameLevel;
    }

    private void Start()
    {
        Reward = Reward * Rare * ((Level / 2) + 1);
        for (int i = 0; i < Level; i++)
        {
            MonsterUpgrade();
        }
    }

    private void LevelUpgrade()
    {
        Level++;
        MonsterUpgrade();
    }
    private void MonsterUpgrade()
    {
        int i = UnityEngine.Random.Range(0, 3);
        switch (i){
            case 0:
                UpradeHP(MaxHealts / 5);
                break;
            case 1:
                UpradeAttackSpeed(AttackSpeed / 10);
                break;
            case 2:
                UpradeDamage(Damage / 5);
                break;

        }
    }


    private void NewGameLevel()
    {
        if (Level > 2) 
            Level -= 2;
    }
}
