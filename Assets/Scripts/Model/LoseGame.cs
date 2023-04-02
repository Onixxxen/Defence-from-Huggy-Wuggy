using System;
using UnityEngine;

public class LoseGame
{
    private Neuron _neuron;
    private Health _health;
    private Armor _armor;
    private Enemy _enemy;
    private DayChanger _dayChanger;

    public event Action<int> GiveLoseGame;
    public event Action<int> GiveDayCount;

    public LoseGame(Neuron neuron, Health healthm, Armor armor, Enemy enemy, DayChanger dayChanger)
    {
        _neuron = neuron;
        _health = healthm;
        _armor = armor;
        _enemy = enemy;
        _dayChanger = dayChanger;
    }

    public void LoseGameRequest()
    {
        _neuron.Reset();
        _health.Reset();
        _armor.Reset();
        _enemy.Reset();
        _dayChanger.Reset();
        GiveLoseGame?.Invoke(_neuron.Count);
    }

    public void DayCountRequest()
    {
        GiveDayCount?.Invoke(_dayChanger.Day);
    }
}
