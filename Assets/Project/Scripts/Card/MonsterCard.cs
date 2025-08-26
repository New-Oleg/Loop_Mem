using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterCard : ACard
{
    [Tooltip("Префабы монстров"), SerializeField]
    private GameObject[] Monsters;

    private static List<GameObject> Tails = new List<GameObject>();

    private void Start()
    {
        if (Tails == null || Tails.Count == 0)
        {
            GetTails();
        }
    }

    private static void GetTails()
    {
        Tails.Clear();
        Tails = GameObject.FindGameObjectsWithTag("Tail").ToList<GameObject>();
        for (int i = 0; i < Tails.Count; i++)
        {
            if (Tails[i].transform.localPosition == new Vector3(0, 0, 1))
            {
                Tails.RemoveAt(i);
            }
        }
    }

    public override void CardSelected()
    {
        base.CardSelected();
        try
        {
            if (Tails.Count != 1)
            {
                int r = Random.Range(0, Tails.Count);
                Tail t = Tails[r].GetComponent<Tail>();
                t.TakeAMob(Monsters[Random.Range(0, Monsters.Length)]);
                Tails.RemoveAt(r);
            } 
        }
        catch 
        {
            GetTails();

            CardSelected();
        }
    }


}
