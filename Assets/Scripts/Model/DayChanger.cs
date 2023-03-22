using System;
using UnityEngine;

public class DayChanger
{
    private int _day;

    private Enemy _enemy;

    private const int _clickerMode = 1;
    private const int _towerDefenceMode = 2;

    public DayChanger(Enemy enemy)
    {
        _enemy = enemy;
    }

    public event Action<int> ActivateClickerMode;
    public event Action<int, int> ActivateTowerDefenceMode;

    public void ChangeDayRequest(int modeIndex)
    {
        if (modeIndex == _clickerMode)
            ActivateClickerMode?.Invoke(modeIndex);          

        if (modeIndex == _towerDefenceMode)
        {
            _day++;
            ActivateTowerDefenceMode?.Invoke(_day, modeIndex);
            _enemy.ChangeEnemyDamage(_enemy.Damage * 2);
        }
    }
}
