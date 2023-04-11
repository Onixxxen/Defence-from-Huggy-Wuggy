using System;
using YG;
using UnityEngine;
using YG.Example;

public class DevelopmentShop : Shop
{
    private Neuron _neuron;

    private SaverData _saverData;

    public DevelopmentShop(Neuron neuron, SaverData saverData)
    {
        _neuron = neuron;
        _saverData = saverData;
    }

    public event Action<int, int, int, int> SellDevelopmentItem;
    public event Action<int, int> GiveNeuronPerClick;
    public event Action<int> OpenItem;
    public event Action<int> CloseItem;
    public event Action<int> LockItem;
    public event Action<int> UnlockItem;

    public void TrySellItem(int index, int price, int addNeuronPerClick)
    {
        Price = price;
        Improvement = addNeuronPerClick;
        int newPrice = (int)(Price * 1.2f);
        //int newPrice = Price;

        if (_neuron.Count >= Price)
        {
            _neuron.RemoveNeuron(Price);
            _neuron.ChangeNeuronPerClick(Improvement);

            _saverData.SaveDevelopmentItemPrices(index, newPrice);

            SellDevelopmentItem?.Invoke(_neuron.Count, newPrice, _neuron.PerClick, index);
        }
    }

    public void NeuronPerClickRequest(int index)
    {
        GiveNeuronPerClick?.Invoke(index, _neuron.PerClick);
    }

    public void OpenItemRequest(int index, int price)
    {
        _saverData.SaveDevelopmentOpenStatus(index, _neuron.Count >= price);

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
