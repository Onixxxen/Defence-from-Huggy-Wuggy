using System;
using UnityEngine;

public class HealthShop : Shop
{
    private Health _health = new Health();
    private Neuron _neuron = new Neuron();

    public HealthShop(Health health, Neuron neuron)
    {
        _health = health;
        _neuron = neuron;
    }

    public event Action<int, int, int, int> SellHealthItem;
    public event Action<int, int> GiveCurrentHealth;
    public event Action<int> OpenItem;
    public event Action<int> LockItem;
    public event Action<int> UnlockItem;

    public void TrySellItem(int index, int price, int addHealth)
    {        
        Price = price;
        Improvement = addHealth;
        int newPrice = (int)(Price * 1.5f);

        if (_neuron.Count >= Price)
        {
            _neuron.RemoveNeuron(Price);
            _health.ChangeHealthCount(Improvement);
            SellHealthItem?.Invoke(_neuron.Count, newPrice, _health.Count, index);
        }
    }

    public void CurrentHealthRequest(int index)
    {
        GiveCurrentHealth?.Invoke(index, _health.Count);
    }

    public void OpenItemRequest(int index, int price)
    {
        if (_neuron.Count >= price)
            OpenItem?.Invoke(index);
    }

    public void LockItemRequest(int index, int price)
    {
        if (_neuron.Count < price)
            LockItem?.Invoke(index);
    }

    public void UnlockItemRequest(int index, int price)
    {
        if (_neuron.Count >= price)
            UnlockItem?.Invoke(index);
    }
}
