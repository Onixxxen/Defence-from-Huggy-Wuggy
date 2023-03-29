using System;
using UnityEngine;
using YG;
using YG.Example;

public class Armor : Brain
{
    private int _maxCount = 10;
    private int _count;

    private SaverData _saverData;

    public Armor(SaverData saverData)
    {
        _saverData = saverData;
    }

    public int MaxCount => _maxCount;
    public int Count => _count;

    public event Action<int> GiveArmorValue;
    public event Action<int, int> GiveArmorCount;
    public event Action<int> RecoveryArmor;

    public void AddMaxArmor(int count)
    {
        _maxCount += count;
        _saverData.SaveArmorMaxCount(_maxCount);
    }

    public void RestoreArmor()
    {
        _count = _maxCount;
        _saverData.SaveArmorCount(_count);
    }

    public void RecoveryArmorRequest()
    {
        // Очень странно работает. Добавить дебаги и проверить
        _count += _maxCount / 5;
        RecoveryArmor?.Invoke(_count);
    }

    public void TakeDamage(int damage)
    {
        _count -= damage;
        _saverData.SaveArmorCount(_count);
    }

    public void ArmorValueRequest()
    {
        GiveArmorValue?.Invoke(_maxCount);
    }

    public void Reset()
    {
        _maxCount = 10;
        _count = _maxCount;
        _saverData.SaveArmorMaxCount(_maxCount);
        _saverData.SaveArmorCount(_count);
    }

    public void LoadArmorData()
    {
        _maxCount = YandexGame.savesData.SavedMaxArmor;
        _count = YandexGame.savesData.SavedArmorCount;
        GiveArmorCount?.Invoke(_count, _maxCount);
    }
}
