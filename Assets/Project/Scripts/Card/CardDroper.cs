using System;
using UnityEngine;

public class CardDroper : MonoBehaviour
{

    public static event Action<float?, int?> ChangeChance;

    [Tooltip("Коллекция карт"), SerializeField]
    private GameObject[] Carts;

    [Tooltip("UI зона для карт"), SerializeField]
    private GameObject[] CartsSlot;

    private int RarChanse = 20;
    private float EpicChanse = 0;

    private int[][] indexMassive = new int[3][] 
    {
            new int[3]
            {
                3,4,5
            },
            new int[3]
            {
                6,7,8
            },
            new int[3]
            {
                9,10,11
            }
    }; //индекси карт с усилениями 

    private void OnEnable()
    {
        RoadWalker.DropCard += CardDrop;
    }

    private void OnDisable()
    {
        RoadWalker.DropCard -= CardDrop;
    }

    public void CardDrop()
    {
        DropMonster();
        DropBonus();
        DropExtraGold();

    }

    private void DropMonster()
    {
        int r = UnityEngine.Random.Range(0, 101);
        int i = r <= (100 - (RarChanse + EpicChanse)) ? 0 :
            r <= (RarChanse + (100 - (RarChanse + EpicChanse))) ? 1 : 2;


        Instantiate(Carts[i],
            new Vector3(CartsSlot[0].transform.position.x,
                CartsSlot[0].transform.position.y, 0),
                CartsSlot[0].transform.rotation,
                CartsSlot[0].transform);
    }

    private void DropBonus()
    {
        int bonusType = UnityEngine.Random.Range(0, 3);
        int r = UnityEngine.Random.Range(0, 101);
        int i = r <= (100 - (RarChanse + EpicChanse)) ? 0 :
            r <= (RarChanse + (100 - (RarChanse + EpicChanse))) ? 1 : 2;


        Instantiate(Carts[indexMassive[bonusType][i]],
            new Vector3(CartsSlot[1].transform.position.x,
                CartsSlot[1].transform.position.y, 0),
                CartsSlot[1].transform.rotation,
                CartsSlot[1].transform);
    }

    private void DropExtraGold()
    {
        int r = UnityEngine.Random.Range(0, 101);
        int i = r <= (100 - (RarChanse + EpicChanse)) ? 12 :
            r <= (RarChanse + (100 - (RarChanse + EpicChanse))) ? 13 : 14;


        Instantiate(Carts[i],
            new Vector3(CartsSlot[2].transform.position.x,
                CartsSlot[2].transform.position.y, 0),
                CartsSlot[2].transform.rotation,
                CartsSlot[2].transform);
    }

    public void UpgradeRarChans()
    {
        if (RarChanse != 60)
        {
            RarChanse += 1;
            ChangeChance.Invoke(null, RarChanse);
        }
    }
    public void UpgradeEpicChans()
    {
        if (EpicChanse != 40)
        {
            EpicChanse += 0.5f;
            ChangeChance.Invoke(EpicChanse, null);
        }
    }
}
