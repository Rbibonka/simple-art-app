using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimationController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private RectTransform button;

    public void OnPointerEnter(PointerEventData eventData)
    {
        button.DOScale(1.1f, 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.DOScale(1f, 0.1f);
    }
}
