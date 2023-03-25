using System;
using UnityEngine;

public class LoseGame
{
    private Neuron _neuron;
    private Health _health;
    private Armor _armor;
    private DayChanger _dayChanger;

    public event Action<int> GiveLoseGame;
    public event Action<int> GiveDayCount;

    public LoseGame(Neuron neuron, Health healthm, Armor armor, DayChanger dayChanger)
    {
        _neuron = neuron;
        _health = healthm;
        _armor = armor;
        _dayChanger = dayChanger;
    }

    public void LoseGameRequest()
    {
        _neuron.Reset();
        _health.Reset();
        _armor.Reset();
        _dayChanger.Reset();
        GiveLoseGame?.Invoke(_neuron.Count);
    }

    public void DayCountRequest()
    {
        GiveDayCount?.Invoke(_dayChanger.Day);
    }
}
