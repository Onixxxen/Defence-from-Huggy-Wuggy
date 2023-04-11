using System;
using UnityEngine;
using YG;

public class DayChangerPresenter
{
    private DayChanger _dayChanger;
    private DayChangerView _dayChangerView;
    private ObjectPoolView _objectPoolView;

    public void Init(DayChanger dayChanger, DayChangerView dayChangerView, ObjectPoolView objectPoolView)
    {
        _dayChanger = dayChanger;
        _dayChangerView = dayChangerView;
        _objectPoolView = objectPoolView;
    }

    public void Enable()
    {
        _dayChangerView.TryChangeMode += RequestChangeDay;

        _dayChanger.ActivateClickerMode += OnActivateClickerMode;
        _dayChanger.ActivateTowerDefenceMode += OnActivateTowerDefenceMode;
        _dayChanger.ChangeEnemyDamage += OnChangeEnemyDamage;
    }    

    public void Disable()
    {
        _dayChangerView.TryChangeMode -= RequestChangeDay;

        _dayChanger.ActivateClickerMode -= OnActivateClickerMode;
        _dayChanger.ActivateTowerDefenceMode -= OnActivateTowerDefenceMode;
        _dayChanger.ChangeEnemyDamage += OnChangeEnemyDamage;
    }

    private void RequestChangeDay(int modeIndex)
    {
        _dayChanger.ChangeDayRequest(modeIndex);
    }

    private void OnActivateClickerMode(int day, int modeIndex)
    {
        _dayChangerView.ActivateClickerMode(day, modeIndex);
    }

    private void OnActivateTowerDefenceMode(int day, int modeIndex)
    {
        _dayChangerView.ActivateTowerDefenceMode(day, modeIndex);
    }

    private void OnChangeEnemyDamage(float factor)
    {
        for (int i = 0; i < _objectPoolView.Pool.Count; i++)
            _objectPoolView.Pool[i].ChangeDamage(factor);
    }
}
