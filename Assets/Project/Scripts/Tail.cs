using System;
using UnityEngine;

public class Tail : MonoBehaviour
{
    public static event Action StartFight;

    [HideInInspector]
    public int Number;

    [HideInInspector]
    public bool IsHaveMonster = false;

    private static int tailCount = 0;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x,
            transform.position.y, transform.position.z + 1);
        tailCount++;
        Number = tailCount + 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Tail" &&
            Number > collision.gameObject.GetComponent<Tail>().Number)
        {
            Destroy(collision.gameObject);
        }

        if (IsHaveMonster && collision.collider.tag == "Player")
        {
            StartFight.Invoke();
        }    
    }

    public void TakeAMob(GameObject Mob)
    {
        Instantiate(Mob, transform.position, transform.rotation,transform);
        IsHaveMonster = true;
    }
}
