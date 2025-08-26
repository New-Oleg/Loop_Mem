using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] 
    private float offset = 50f;   // насколько сдвигаем влево

    [SerializeField] 
    private float duration = 0.25f; // время анимации

    private RectTransform rectTransform;
    private Vector2 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.DOAnchorPos(originalPosition + Vector2.left * offset, duration)
            .SetEase(Ease.OutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.DOAnchorPos(originalPosition, duration)
            .SetEase(Ease.OutQuad);
    }
}
