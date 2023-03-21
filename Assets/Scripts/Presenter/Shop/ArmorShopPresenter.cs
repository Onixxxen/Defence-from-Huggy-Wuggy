using System.Collections.Generic;
using UnityEngine;

public class ArmorShopPresenter : ShopPresenter
{
    private ArmorShop _armorShop;
    private ArmorShopView _armorShopView;
    private List<ArmorItemView> _armorItemView = new List<ArmorItemView>();

    public void Init(ArmorShop armorShop, NeuronCollectorView neuronCollectorView, ArmorShopView armorShopView, List<ArmorItemView> armorItemView)
    {
        _armorShop = armorShop;
        NeuronCollectorView = neuronCollectorView;
        _armorShopView = armorShopView;

        for (int i = 0; i < armorItemView.Count; i++)
            _armorItemView.Add(armorItemView[i]);

    }

    public void Enable()
    {
        _armorShopView.OnSellButtonClick += TrySell;
        _armorShopView.OnRequsetCurrentArmor += RequestCurrentArmor;
        _armorShopView.OnRequestOpenItem += RequestOpenItem;
        _armorShopView.OnRequsetLockItem += RequestLockItem;
        _armorShopView.OnRequsetUnlockItem += RequestUnlockItem;

        _armorShop.SellArmorItem += OnBuying;
        _armorShop.GiveCurrentArmor += OnGiveCurrentArmor;
        _armorShop.OpenItem += OnOpenItem;
        _armorShop.LockItem += OnLockItem;
        _armorShop.UnlockItem += OnUnlockItem;
    }

    public void Disable()
    {
        _armorShopView.OnSellButtonClick -= TrySell;
        _armorShopView.OnRequsetCurrentArmor -= RequestCurrentArmor;
        _armorShopView.OnRequestOpenItem -= RequestOpenItem;
        _armorShopView.OnRequsetLockItem -= RequestLockItem;
        _armorShopView.OnRequsetUnlockItem -= RequestUnlockItem;

        _armorShop.SellArmorItem -= OnBuying;
        _armorShop.GiveCurrentArmor -= OnGiveCurrentArmor;
        _armorShop.OpenItem -= OnOpenItem;
        _armorShop.LockItem += OnLockItem;
        _armorShop.UnlockItem += OnUnlockItem;
    }

    public void TrySell(int index, int price, int addArmor)
    {
        _armorShop.TrySellItem(index, price, addArmor);
    }
    public void OnBuying(int newNeuron, int newPrice, int newArmor, int index)
    {
        NeuronCollectorView.ChangeNeuronView(newNeuron);
        _armorItemView[index].UpdateValues(newPrice);
        _armorShopView.UpdateCurrentArmor(newArmor);
    }

    public void RequestCurrentArmor(int index)
    {
        _armorShop.CurrentArmorRequest(index);
    }

    public void OnGiveCurrentArmor(int index, int armor)
    {
        _armorShopView.SetCurrentArmor(armor);
    }

    public void RequestOpenItem(int index, int price)
    {
        _armorShop.OpenItemRequest(index, price);
    }

    public void OnOpenItem(int index)
    {
        _armorItemView[index].OpenItem();
    }

    public void RequestLockItem(int index, int price)
    {
        _armorShop.LockItemRequest(index, price);
    }

    public void OnLockItem(int index)
    {
        _armorItemView[index].LockItem();
    }

    public void RequestUnlockItem(int index, int price)
    {
        _armorShop.UnlockItemRequest(index, price);
    }

    public void OnUnlockItem(int index)
    {
        _armorItemView[index].UnlockItem();
    }
}
