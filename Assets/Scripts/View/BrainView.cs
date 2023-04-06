using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BrainView : MonoBehaviour
{
    private Slider _healthSlider;
    private Slider _armorSlider;

    private TMP_Text _healthText;
    private TMP_Text _armorText;

    private DayChangerView _dayChangerView;
    private LoseGameView _loseGameView;

    private SupportingTextView _supportingTextView;

    private BrainAttackView _brainAttackView;

    private float _normalScale;

    private const int _clickerMode = 1;
    private const int _towerDefenceMode = 2;

    private Camera _camera;

    public event Action ChangeNeuronCount;
    public event Action OnRestoreBrain;

    public void Init(DayChangerView dayChangerView, LoseGameView loseGameView, SupportingTextView supportingTextView, Slider healthSlider, Slider armorSlider, TMP_Text healthText, TMP_Text armorText)
    {
        _dayChangerView = dayChangerView;
        _loseGameView = loseGameView;
        _supportingTextView = supportingTextView;
        _healthSlider = healthSlider;
        _armorSlider = armorSlider;
        _healthText = healthText;
        _armorText = armorText;
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
        _camera = Camera.main;
        _brainAttackView = GetComponentInChildren<BrainAttackView>();
    }

    private void Update()
    {
        _healthText.text = FormatNumberExtension.FormatNumber((int)_healthSlider.value);
        _armorText.text = FormatNumberExtension.FormatNumber((int)_armorSlider.value);

        if (_dayChangerView.CurrentMode == _towerDefenceMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!_brainAttackView.BulletIsCreated)
                {
                    _brainAttackView.Shot();
                }
            }
        }
    }

    private void OnBraintClick()
    {
        ChangeNeuronCount?.Invoke();
        TryShowSupportingText(_clickerMode);
        StartCoroutine(ChangeBrainScale());
    }

    private IEnumerator ChangeBrainScale()
    {
        transform.DOScale(_normalScale - 1, 0.5f);
        yield return new WaitForSeconds(0.1f);
        transform.DOScale(_normalScale, 0.5f);
    }

    public void TryShowSupportingText(int mode)
    {
        if (mode == _clickerMode)
        {
            int showText = UnityEngine.Random.Range(1, 20);
            if (showText == 1)
                _supportingTextView.ShowSupportingText(_clickerMode);
        }
        else if (mode == _towerDefenceMode)
        {
            int showText = UnityEngine.Random.Range(1, 5);
            if (showText == 1)
                _supportingTextView.ShowSupportingText(_towerDefenceMode);
        }
    }

    public void SetArmorValue(int value) // Вместо этого делать ChangeArmorCount(различие - тот будет делать плавно)
    {
        _armorSlider.maxValue = value;
        _armorSlider.value = _armorSlider.maxValue;
    }

    public void SetHealthValue(int value) // Вместо этого делать ChangeHealthCount(различие - тот будет делать плавно)
    {
        _healthSlider.maxValue = value;
        _healthSlider.value = _healthSlider.maxValue;
    }

    public void ChangeArmorValue(int armorValue)
    {
        _armorSlider.DOValue(armorValue, 1);
    }

    public void ChangeHealthValue(int healthValue)
    {
        _healthSlider.DOValue(healthValue, 1);
    }

    public void ChangeArmorCount(int armorValue, int maxValue)
    {
        _armorSlider.maxValue = maxValue;
        _armorSlider.DOValue(armorValue, 1);
    }

    public void ChangeHealthCount(int healthValue, int maxValue)
    {
        _healthSlider.maxValue = maxValue;
        _healthSlider.DOValue(healthValue, 1);
    }

    public void RestoreBrain()
    {
        _healthSlider.DOValue(_healthSlider.maxValue, 1);
        _armorSlider.DOValue(_armorSlider.maxValue, 1);
        OnRestoreBrain?.Invoke();
    }

    public void BrainDie()
    {
        _loseGameView.gameObject.SetActive(true);
    }
}
