using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainPresenter : MonoBehaviour
{
    private Health _health;
    private Armor _armor;
    private BrainView _brainView;

    public void Init(Health health, Armor armor, BrainView brainView)
    {
        _health = health;
        _armor = armor;
        _brainView = brainView;
    }

    public void Enable()
    {
        _brainView.OnRequestHealthValue += RequestHealthValue;
        _brainView.OnRequestArmorValue += RequestArmorValue;

        _armor.GiveArmorValue += OnGiveArmorValue;
        _health.GiveHealthValue += OnGiveHealthValue;
    }
    public void Disable()
    {
        _brainView.OnRequestHealthValue -= RequestHealthValue;
        _brainView.OnRequestArmorValue -= RequestArmorValue;

        _armor.GiveArmorValue -= OnGiveArmorValue;
        _health.GiveHealthValue -= OnGiveHealthValue;
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
}
