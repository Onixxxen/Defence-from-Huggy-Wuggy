using System;
using UnityEngine;
using YG; // яндекс SDK
using YG.Example;

public class DayChanger
{
    private int _day;
    private int _maxDay;

    private Health _health;
    private Armor _armor;

    private Enemy _enemy;

    private SaverData _saverData;

    private const int _clickerMode = 1;
    private const int _towerDefenceMode = 2;

    public DayChanger(Enemy enemy, Health health, Armor armor, SaverData saverData)
    {
        _enemy = enemy;
        _health = health;
        _armor = armor;
        _saverData = saverData;
    }

    public int Day => _day;

    public event Action<int> ActivateClickerMode;
    public event Action<int, int> ActivateTowerDefenceMode;
    public event Action<int, int> RestoreBrain;
    public event Action<int> ChangeEnemyDamage;

    public void ChangeDayRequest(int modeIndex)
    {
        if (modeIndex == _clickerMode)
        {
            ActivateClickerMode?.Invoke(modeIndex);

            if (_maxDay <= _day)
            {
                _maxDay = _day;

                if (YandexGame.savesData.ClickerLoaded)
                    _saverData.SaveMaxDayCount(_maxDay);

                YandexGame.NewLeaderboardScores("DayCount", _maxDay); // яндекс SDK
            }

            if (_day % 2 == 0)
                YandexGame.FullscreenShow(); // яндекс SDK
        }

        if (modeIndex == _towerDefenceMode)
        {
            if (YandexGame.savesData.TowerDefenceLoaded)
            {
                _day++;                

                _saverData.SaveDayCount(_day);
                _health.RestoreHealth();
                _armor.RestoreArmor();
                RestoreBrain?.Invoke(_health.Count, _armor.Count);
            }

            ActivateTowerDefenceMode?.Invoke(_day, modeIndex);
            ChangeEnemyDamage?.Invoke(2);            
        }        
    }

    public void Reset()
    {
        _day = 0;
        _saverData.SaveDayCount(_day);
    }

    public void LoadDayData()
    {
        _day = YandexGame.savesData.SavedDay;
        _maxDay = YandexGame.savesData.SavedMaxDay;
    }
}
