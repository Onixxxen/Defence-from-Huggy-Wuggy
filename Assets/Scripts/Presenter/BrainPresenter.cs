using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainPresenter : MonoBehaviour
{
    private Health _health;
    private Armor _armor;
    private BrainView _brainView;
    private DayChanger _dayChanger;

    public void Init(Health health, Armor armor, BrainView brainView, DayChanger dayChanger)
    {
        _health = health;
        _armor = armor;
        _brainView = brainView;
        _dayChanger = dayChanger;
    }

    public void Enable()
    {
        _brainView.OnRequestHealthValue += RequestHealthValue;
        _brainView.OnRequestArmorValue += RequestArmorValue;

        _armor.GiveArmorValue += OnGiveArmorValue;
        _health.GiveHealthValue += OnGiveHealthValue;
        _dayChanger.RestoreBrain += OnRestoreBrain;
    }
    public void Disable()
    {
        _brainView.OnRequestHealthValue -= RequestHealthValue;
        _brainView.OnRequestArmorValue -= RequestArmorValue;

        _armor.GiveArmorValue -= OnGiveArmorValue;
        _health.GiveHealthValue -= OnGiveHealthValue;
        _dayChanger.RestoreBrain -= OnRestoreBrain;
    }

    private void RequestArmorValue()
    {
        _armor.ArmorValueRequest();
    }

    private void RequestHealthValue()
    {
        _health.HealthValueRequest();
    }

    private void OnGiveArmorValue(int value)
    {
        _brainView.SetArmorValue(value);
    }

    private void OnGiveHealthValue(int value)
    {
        _brainView.SetHealthValue(value);
    }

    private void OnRestoreBrain(int healthValue, int armorValue)
    {
        _brainView.SetHealthValue(healthValue);
        _brainView.SetArmorValue(armorValue);
    }
}
