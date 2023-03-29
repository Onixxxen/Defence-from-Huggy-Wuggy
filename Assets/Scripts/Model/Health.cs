using System;
using YG;
using YG.Example;

public class Health : Brain
{
    private int _maxCount = 10;
    private int _count;

    private SaverData _saverData;

    public Health(SaverData saverData)
    {
        _saverData = saverData;
    }

    public int MaxCount => _maxCount;
    public int Count => _count;

    public event Action<int> GiveHealthValue;
    public event Action<int, int> GiveHealthCount;
    public event Action<int> RecoveryHealth;

    public void AddMaxHealth(int count)
    {
        _maxCount += count;
        _saverData.SaveHealthMaxCount(_maxCount);
    }

    public void RecoveryHealthRequest()
    {
        _count += _maxCount / 5;
        RecoveryHealth?.Invoke(_count);
    }

    public void RestoreHealth()
    {
        _count = _maxCount;
        _saverData.SaveHealthCount(_count);
    }

    public void TakeDamage(int damage)
    {
        _count -= damage;
        _saverData.SaveHealthCount(_count);
    }

    public void HealthValueRequest()
    {
        GiveHealthValue?.Invoke(_maxCount);
    }

    public void Reset()
    {
        _maxCount = 10;
        _count = _maxCount;
        _saverData.SaveHealthMaxCount(_maxCount);
        _saverData.SaveHealthCount(_count);
    }

    public void LoadHealthData()
    {
        _maxCount = YandexGame.savesData.SavedMaxHealth;
        _count = YandexGame.savesData.SavedHealthCount;
        GiveHealthCount?.Invoke(_count, _maxCount);
    }
}
