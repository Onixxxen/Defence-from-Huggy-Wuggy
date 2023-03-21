using System;
using Unity.VisualScripting;
using UnityEngine;

public class DayChangerPresenter
{
    private DayChanger _dayChanger;
    private DayChangerView _dayChangerView;

    public void Init(DayChanger dayChanger, DayChangerView dayChangerView)
    {
        _dayChanger = dayChanger;
        _dayChangerView = dayChangerView;
    }

    public void Enable()
    {
        _dayChangerView.TryChangeMode += RequestChangeDay;

        _dayChanger.ActivateClickerMode += OnActivateClickerMode;
        _dayChanger.ActivateTowerDefenceMode += OnActivateTowerDefenceMode;
    }    

    public void Disable()
    {
        _dayChangerView.TryChangeMode -= RequestChangeDay;

        _dayChanger.ActivateClickerMode -= OnActivateClickerMode;
        _dayChanger.ActivateTowerDefenceMode -= OnActivateTowerDefenceMode;
    }

    private void RequestChangeDay(int modeIndex)
    {
        _dayChanger.ChangeDayRequest(modeIndex);
    }

    private void OnActivateClickerMode(int modeIndex)
    {
        _dayChangerView.ActivateClickerMode(modeIndex);
    }

    private void OnActivateTowerDefenceMode(int day, int modeIndex)
    {
        _dayChangerView.ActivateTowerDefenceMode(day, modeIndex);
    }
}
