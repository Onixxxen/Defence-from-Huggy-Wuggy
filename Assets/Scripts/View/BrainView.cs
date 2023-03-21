using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BrainView : MonoBehaviour
{
    [SerializeField] private Slider _health;
    [SerializeField] private Slider _armor;

    private DayChangerView _dayChangerView;

    private float _normalScale;

    private const int _clickerMode = 1;
    private const int _towerDefenceMode = 2;

    public event Action ChangeNeuronCount;

    public void Init(DayChangerView dayChangerView)
    {
        _dayChangerView = dayChangerView;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (_dayChangerView.CurrentMode == _clickerMode)
            OnBraintClick();
    }

    private void Start()
    {
        _normalScale = transform.localScale.x;
    }

    private void OnBraintClick()
    {
        ChangeNeuronCount?.Invoke();
        StartCoroutine(ChangeBrainScale());
    }

    private IEnumerator ChangeBrainScale()
    {
        transform.DOScale(_normalScale - 1, 0.5f);
        yield return new WaitForSeconds(0.1f);
        transform.DOScale(_normalScale, 0.5f);
    }
}
