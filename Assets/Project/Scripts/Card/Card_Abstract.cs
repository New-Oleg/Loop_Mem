using System;
using UnityEngine;

public abstract class ACard : MonoBehaviour
{
    public static event Action CardSelect;

    protected virtual void OnEnable()
    {
        CardSelect += CardDestroy;
    }

    protected virtual void OnDisable()
    {
        CardSelect -= CardDestroy;
    }

    public virtual void CardSelected()
    {
        CardSelect.Invoke();
    }

    private void CardDestroy()
    {
        Destroy(gameObject);
    }
}