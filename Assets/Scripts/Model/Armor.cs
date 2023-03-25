using System;

public class Armor : Brain
{
    private int _maxCount = 10;
    private int _count;

    public int MaxCount => _maxCount;
    public int Count => _count;

    public event Action<int> GiveArmorValue;
    public event Action<int> RecoveryArmor;

    public void AddMaxArmor(int count)
    {
        _maxCount += count;
    }

    public void AddArmor(int count)
    {
        _count += count;
    }

    public void RestoreArmor()
    {
        _count = _maxCount;
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
    }

    public void ArmorValueRequest()
    {
        GiveArmorValue?.Invoke(_maxCount);
    }

    public void Reset()
    {
        _maxCount = 10;
        _count = _maxCount;
    }
}
