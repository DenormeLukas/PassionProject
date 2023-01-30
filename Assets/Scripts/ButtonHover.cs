using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.1f);
    private Vector3 originalScale;
    private RectTransform buttonRectTransform;

    void Start()
    {
        buttonRectTransform = GetComponent<RectTransform>();
        originalScale = buttonRectTransform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonRectTransform.localScale = hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonRectTransform.localScale = originalScale;
    }
}
