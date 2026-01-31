using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    [SerializeField]
    private Button btn_Back;

    [SerializeField]
    private RectTransform panel;

    [SerializeField]
    private Image content;

    public event Action buttonBackClicked;

    public void Initialize()
    {
        panel.transform.localScale = Vector3.zero;
        btn_Back.onClick.AddListener(OnButtonBackClicked);
    }

    public void Deinitialize()
    {
        btn_Back.onClick.RemoveListener(OnButtonBackClicked);
    }

    public void SetupContent(Sprite sprite)
    {
        content.sprite = sprite;
    }

    public void Show()
    {
        panel.DOScale(1f, 0.4f);
    }

    public void Hide()
    {
        panel.DOScale(0f, 0.4f);
    }

    private void OnButtonBackClicked()
    {
        buttonBackClicked?.Invoke();
    }
}