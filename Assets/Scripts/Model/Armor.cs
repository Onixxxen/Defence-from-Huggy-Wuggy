using System;

public class Armor : Brain
{
    private int _maxCount = 10;
    private int _count;

    public int MaxCount => _maxCount;
    public int Count => _count;

    public event Action<int> GiveArmorValue;

    public void AddArmor(int count)
    {
        _maxCount += count;
    }

    public void RestoreArmor()
    {
        _count = _maxCount;
    }

    public void TakeDamage(int damage)
    {
        _count -= damage;
    }

    public void ArmorValueRequest()
    {
        GiveArmorValue?.Invoke(_maxCount);
    }
}
