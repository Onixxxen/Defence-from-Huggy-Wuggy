using System;
using UnityEngine;

public class DayChanger
{
    private int _day;

    private Health _health;
    private Armor _armor;

    private Enemy _enemy;

    private const int _clickerMode = 1;
    private const int _towerDefenceMode = 2;

    public DayChanger(Enemy enemy, Health health, Armor armor)
    {
        _enemy = enemy;
        _health = health;
        _armor = armor;
    }

    public int Day => _day;

    public event Action<int> ActivateClickerMode;
    public event Action<int, int> ActivateTowerDefenceMode;
    public event Action<int, int> RestoreBrain;

    public void ChangeDayRequest(int modeIndex)
    {
        if (modeIndex == _clickerMode)
            ActivateClickerMode?.Invoke(modeIndex);          

        if (modeIndex == _towerDefenceMode)
        {
            _day++;
            ActivateTowerDefenceMode?.Invoke(_day, modeIndex);
            _enemy.ChangeEnemyDamage(_enemy.Damage * 2);
            _health.RestoreHealth();
            _armor.RestoreArmor();
            RestoreBrain?.Invoke(_health.Count, _armor.Count);
        }
    }

    public void Reset()
    {
        _day = 0;
    }
}
