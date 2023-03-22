using System;

public class Health : Brain
{
    private int _count = 10;

    public int Count => _count;

    public event Action<int> GiveHealthValue;

    public void AddHealth(int count)
    {
        _count += count;
    }

    public void TakeDamage(int damage)
    {
        _count -= damage;
    }

    public void HealthValueRequest()
    {
        GiveHealthValue?.Invoke(_count);
    }
}
