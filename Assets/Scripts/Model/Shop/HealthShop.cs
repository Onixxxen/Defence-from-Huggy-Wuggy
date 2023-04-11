using System;
using UnityEngine;
using YG;
using YG.Example;

public class HealthShop : Shop
{
    private Health _health;
    private Neuron _neuron;

    private SaverData _saverData;

    public HealthShop(Health health, Neuron neuron, SaverData saverData)
    {
        _health = health;
        _neuron = neuron;
        _saverData = saverData;
    }

    public event Action<int, int, int, int> SellHealthItem;
    public event Action<int, int> GiveCurrentHealth;
    public event Action<int> OpenItem;
    public event Action<int> CloseItem;
    public event Action<int> LockItem;
    public event Action<int> UnlockItem;

    public void TrySellItem(int index, int price, int addHealth)
    {        
        Price = price;
        Improvement = addHealth;
        int newPrice = (int)(Price * 1.2f);
        //int newPrice = Price;

        if (_neuron.Count >= Price)
        {
            _neuron.RemoveNeuron(Price);
            _health.AddMaxHealth(Improvement);

            _saverData.SaveHealthItemPrices(index, newPrice);

            SellHealthItem?.Invoke(_neuron.Count, newPrice, _health.MaxCount, index);
        }
    }

    public void CurrentHealthRequest(int index)
    {
        GiveCurrentHealth?.Invoke(index, _health.MaxCount);
    }

    public void OpenItemRequest(int index, int price)
    {
        _saverData.SaveHealthOpenStatus(index, _neuron.Count >= price);

        if (_neuron.Count >= price)
            OpenItem?.Invoke(index);
    }

    public void CloseItemRequest(int index, int price)
    {
        if (_neuron.Count < price)
            CloseItem?.Invoke(index);
    }

    public void LockItemRequest(int index, int price)
    {
        int newPrice = (int)(price * 1.5f);

        if (_neuron.Count < newPrice)
            LockItem?.Invoke(index);
    }

    public void UnlockItemRequest(int index, int price)
    {
        if (_neuron.Count >= price)
            UnlockItem?.Invoke(index);
    }
}
