using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class FightController : MonoBehaviour
{
    public static event Action EndFight;
    public static event Action<int> GetRevard;

    private Character Character;
    private Monster monster;

    private void OnEnable()
    {
        Tail.StartFight += Fight;
    }

    private void OnDisable()
    {
        Tail.StartFight -= Fight;
    }
    void Start()
    {
        Character = GameObject.FindWithTag("Player").GetComponent<Character>();
    }
    private void Fight()
    {
        monster = FindMonster("Monnster", Character.transform.position);
        StartCoroutine(nameof(FightCoroutine));
    }

    private IEnumerator FightCoroutine()
    {
        float timerChar = 0f;
        float timerMon = 0f;

        yield return null;

        if(!monster.IsAlive()) {
            EndFight.Invoke();
            StopCoroutine(nameof(FightCoroutine));
        }

        yield return null;

        while (Character.IsAlive() && monster.IsAlive())
        {
            timerChar += Time.deltaTime;
            timerMon += Time.deltaTime;

            if (timerChar >= Character.AttackSpeed)
            {
                monster.TakeDamage(Character.Hit());
                timerChar = 0f;
            }

            if (monster.IsAlive() && timerMon >= monster.AttackSpeed)
            {
                Character.TakeDamage(monster.Hit());
                timerMon = 0f;
            }

            yield return null;
        }

        if (!Character.IsAlive())
        {
            Debug.Log("Персонаж проиграл");
        }
        else
        {
            Debug.Log("Монстр побеждён");
            GetRevard.Invoke(monster.Reward);
            EndFight.Invoke();
        }
    }

    private Monster FindMonster(string tag, Vector2 position)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject obj in objects)
        {
            float distance = Vector2.Distance(position, obj.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = obj;
            }
        }

        return closest.GetComponent<Monster>();
    }
}
