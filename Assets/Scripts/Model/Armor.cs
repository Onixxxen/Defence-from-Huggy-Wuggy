using System;

public class Armor : Brain
{
    private int _count = 10;

    public int Count => _count;

    public event Action<int> GiveArmorValue;

    public void AddArmor(int count)
    {
        _count += count;
    }

    public void TakeDamage(int damage)
    {
        _count -= damage;
    }

    public void ArmorValueRequest()
    {
        GiveArmorValue?.Invoke(_count);
    }
}
