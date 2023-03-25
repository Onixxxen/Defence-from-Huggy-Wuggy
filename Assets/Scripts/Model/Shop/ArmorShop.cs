using System;

public class ArmorShop : Shop
{
    private Armor _armor = new Armor();
    private Neuron _neuron = new Neuron();

    public ArmorShop(Armor armor, Neuron neuron)
    {
        _armor = armor;
        _neuron = neuron;
    }

    public event Action<int, int, int, int> SellArmorItem;
    public event Action<int, int> GiveCurrentArmor;
    public event Action<int> OpenItem;
    public event Action<int> CloseItem;
    public event Action<int> LockItem;
    public event Action<int> UnlockItem;

    public void TrySellItem(int index, int price, int addArmor)
    {
        Price = price;
        Improvement = addArmor;
        int newPrice = (int)(Price * 1.5f);

        if (_neuron.Count >= Price)
        {
            _neuron.RemoveNeuron(Price);
            _armor.AddMaxArmor(Improvement);
            SellArmorItem?.Invoke(_neuron.Count, newPrice, _armor.MaxCount, index);
        }        
    }

    public void CurrentArmorRequest(int index)
    {
        GiveCurrentArmor?.Invoke(index, _armor.MaxCount);
    }

    public void OpenItemRequest(int index, int price)
    {
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
