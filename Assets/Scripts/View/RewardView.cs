using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using YG;

public class RewardView : MonoBehaviour
{
    [SerializeField] private LoseGameView _loseGameView;
    [SerializeField] private DayChangerView _dayChangerView;
    [SerializeField] private ObjectPoolView _objectPool;
    [SerializeField] private RecoveryArmorView _recoveryArmorView;
    [SerializeField] private RecoveryHealthView _recoveryHealthView;
    [SerializeField] private RewardButtonView _rewardButtonView;
    [SerializeField] private BrainView _brainView;

    [Header("Settings")]
    [SerializeField] private int _bonusTime = 30;

    private const int _clickerMode = 1;
    private const int _towerDefenceMode = 2;

    private void OnEnable() => YandexGame.RewardVideoEvent += OnRewardEvent;
    private void OnDisable() => YandexGame.RewardVideoEvent -= OnRewardEvent;

    public event Action<int> TryMultiplyNeuronPerClick;
    public event Action<int> TryBackNeuronPerClick;

    private void OnRewardEvent(int id)
    {
        if (id == 1)
            ContinueGame();
        else if (id == 2)
            StartCoroutine(MultiplyNeuronPerClick(2));
        else if (id == 3)
            StartCoroutine(ChangeDayTimeInSecond());
        else if (id == 4)
            StartCoroutine(ChangeEnemySpeed());
        else if (id == 5)
            SkipHealthCooldown();
        else if (id == 6)
            SkipArmorCooldown();
        else if (id == 7)
            RecoveryBrain();

        for (int i = 0; i < _objectPool.Pool.Count; i++)
            _objectPool.Pool[i].StopCoroutine(_objectPool.Pool[i].Attack());
    }

    private IEnumerator MultiplyNeuronPerClick(int factor)
    {
        TryMultiplyNeuronPerClick?.Invoke(factor);

        ActivateSlider("NeuronBonusButton", _bonusTime);

        yield return new WaitForSeconds(_bonusTime);

        TryBackNeuronPerClick?.Invoke(factor);
    }

    private void ContinueGame()
    {
        _loseGameView.DayChangerView.ChangeTime(0.57f);
        _loseGameView.DayChangerView.BackDayTimeInSecond();
        _loseGameView.gameObject.SetActive(false);
    }

    private IEnumerator ChangeDayTimeInSecond()
    {
        if (_dayChangerView.CurrentMode == _clickerMode)
        {
            _dayChangerView.ChangeDayTimeInSecond(_dayChangerView.DayTimeInSecond * 4);
            ActivateSlider("SlowTimeButton", _bonusTime);

            yield return new WaitForSeconds(_bonusTime);

            _dayChangerView.BackDayTimeInSecond();
        }
        else if (_dayChangerView.CurrentMode == _towerDefenceMode)
        {
            _dayChangerView.ChangeDayTimeInSecond(_dayChangerView.DayTimeInSecond / 4);
            ActivateSlider("SpeedTimeButton", _bonusTime);

            yield return new WaitForSeconds(_bonusTime);

            _dayChangerView.BackDayTimeInSecond();
        }
    }

    private IEnumerator ChangeEnemySpeed()
    {
        for (int i = 0; i < _objectPool.Pool.Count; i++)
            _objectPool.Pool[i].ChangeSpeed(_objectPool.Pool[i].Speed / 3);

        ActivateSlider("SlowDownButton", _bonusTime);

        yield return new WaitForSeconds(_bonusTime);

        for (int i = 0; i < _objectPool.Pool.Count; i++)
            _objectPool.Pool[i].ChangeSpeed(_objectPool.Pool[i].NormalSpeed);

        _rewardButtonView.ChangeSlowDownButtonStatus(false);
    }

    private void SkipHealthCooldown()
    {
        _recoveryHealthView.CooldownSlider.DOPause();
        ActivateSlider("SkipHealthCooldownButton", 1);
        _recoveryHealthView.CooldownSlider.DOValue(_recoveryHealthView.CooldownSlider.minValue, 1);
    }

    private void SkipArmorCooldown()
    {
        _recoveryHealthView.CooldownSlider.DOPause();
        ActivateSlider("SkipArmorCooldownButton", 1);
        _recoveryArmorView.CooldownSlider.DOValue(_recoveryArmorView.CooldownSlider.minValue, 1);
    }

    private void RecoveryBrain()
    {
        ActivateSlider("RecoveryBrainButton", 1);
        _rewardButtonView.ChangeRecoveryBrainButtonStatus(false);
        _brainView.RestoreBrain();
    }

    private void ActivateSlider(string nameButton, int duration)
    {
        for (int i = 0; i < _rewardButtonView.SpawnedButton.Count; i++)
            if (_rewardButtonView.SpawnedButton[i].Name == nameButton)
                _rewardButtonView.SpawnedButton[i].ActivateSlider(duration);
    }
}
