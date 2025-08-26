using System;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public static event Action<int, Transform> OnTakeDamage;

    [SerializeField]
    protected int Damage;

    public float AttackSpeed;

    [SerializeField]
    protected int MaxHealts;

    public int Healts;

    private void Awake()
    {
        Healts = MaxHealts;
    }
    protected virtual void OnEnable()
    {
        RoadWalker.DropCard += RegenHealtsOnEndTurn;
    }
    protected virtual void OnDisable()
    {
        RoadWalker.DropCard -= RegenHealtsOnEndTurn;
    }

    private void RegenHealtsOnEndTurn()
    {
        Healts = MaxHealts;
    }
    
    public int Hit()
    {
        return Damage;
    }
    public void TakeDamage(int D)
    {
        Healts -= D;
        OnTakeDamage.Invoke(D, transform);
    }
    public void RegenHealts(int H)
    {
        if(Healts + H <= MaxHealts)
            Healts += H;
        else Healts = MaxHealts;
    }
    public void UpradeHP(int bonus)
    {
        MaxHealts += bonus;
        Healts = MaxHealts;
    }
    public void UpradeDamage(int bonus)
    {
        Damage += bonus;
    }
    public void UpradeAttackSpeed(float bonus)
    {
        if (AttackSpeed - bonus >= 0.1f)
            AttackSpeed -= bonus;
    }

    public virtual bool IsAlive() => Healts > 0;
}
