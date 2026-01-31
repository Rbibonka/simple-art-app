using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TabBarController : MonoBehaviour
{
    [SerializeField]
    private Button btn_All;

    [SerializeField]
    private Button btn_Odd;

    [SerializeField]
    private Button btn_Even;

    [SerializeField]
    private RectTransform selectPanel;

    private TabBarView tabBarView;

    public event Action ButtonAllClicked;
    public event Action ButtonOddClicked;
    public event Action ButtonEvenClicked;

    public void Initialize()
    {
        tabBarView = new(selectPanel);

        btn_All.onClick.AddListener(OnButtonAllClicked);
        btn_Odd.onClick.AddListener(OnButtonOddClicked);
        btn_Even.onClick.AddListener(OnButtonEvenClicked);
    }

    public void Deinitialize()
    {
        btn_All.onClick.RemoveListener(OnButtonAllClicked);
        btn_Odd.onClick.RemoveListener(OnButtonOddClicked);
        btn_Even.onClick.RemoveListener(OnButtonEvenClicked);
    }

    private void OnButtonAllClicked()
    {
        ButtonAllClicked?.Invoke();

        tabBarView.MoveSelectorTo(btn_All);
    }

    private void OnButtonOddClicked()
    {
        ButtonOddClicked?.Invoke();

        tabBarView.MoveSelectorTo(btn_Odd);
    }

    private void OnButtonEvenClicked()
    {
        ButtonEvenClicked?.Invoke();

        tabBarView.MoveSelectorTo(btn_Even);
    }
}