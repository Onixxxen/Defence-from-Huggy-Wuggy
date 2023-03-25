using System;

public class Health : Brain
{
    private int _maxCount = 10;
    private int _count;

    public int MaxCount => _maxCount;
    public int Count => _count;

    public event Action<int> GiveHealthValue;
    public event Action<int> RecoveryHealth;

    public void AddMaxHealth(int count)
    {
        _maxCount += count;
    }

    public void AddHealth(int count)
    {
        _count += count;
    }

    public void RecoveryHealthRequest()
    {
        _count += _maxCount / 5;
        RecoveryHealth?.Invoke(_count);
    }

    public void RestoreHealth()
    {
        _count = _maxCount;
    }

    public void TakeDamage(int damage)
    {
        _count -= damage;
    }

    public void HealthValueRequest()
    {
        GiveHealthValue?.Invoke(_maxCount);
    }

    public void Reset()
    {
        _maxCount = 10;
        _count = _maxCount;
    }
}
