using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthShopPresenter : ShopPresenter
{
    private HealthShop _healthShop;
    private HealthShopView _healthShopView;
    private List<HealthItemView> _healthItemView = new List<HealthItemView>();

    public void Init(HealthShop healthShop, NeuronCollectorView neuronCollectorView, HealthShopView healthShopView, List<HealthItemView> healthItemView)
    {
        _healthShop = healthShop;
        NeuronCollectorView = neuronCollectorView;
        _healthShopView = healthShopView;

        for (int i = 0; i < healthItemView.Count; i++)
            _healthItemView.Add(healthItemView[i]);

    }

    public void Enable()
    {
        _healthShopView.OnSellButtonClick += TrySell;
        _healthShopView.OnRequsetCurrentHealth += RequestCurrentHealth;
        _healthShopView.OnRequestOpenItem += RequestOpenItem;
        _healthShopView.OnRequsetLockItem += RequestLockItem;
        _healthShopView.OnRequsetUnlockItem += RequestUnlockItem;

        _healthShop.SellHealthItem += OnBuying;
        _healthShop.GiveCurrentHealth += OnGiveCurrentHealth;
        _healthShop.OpenItem += OnOpenItem;
        _healthShop.LockItem += OnLockItem;
        _healthShop.UnlockItem += OnUnlockItem;
    }

    public void Disable()
    {
        _healthShopView.OnSellButtonClick -= TrySell;
        _healthShopView.OnRequsetCurrentHealth -= RequestCurrentHealth;
        _healthShopView.OnRequestOpenItem -= RequestOpenItem;
        _healthShopView.OnRequsetLockItem += RequestLockItem;
        _healthShopView.OnRequsetUnlockItem += RequestUnlockItem;

        _healthShop.SellHealthItem -= OnBuying;
        _healthShop.GiveCurrentHealth -= OnGiveCurrentHealth;
        _healthShop.OpenItem -= OnOpenItem;
        _healthShop.LockItem -= OnLockItem;
        _healthShop.UnlockItem -= OnUnlockItem;
    }

    public void TrySell(int index, int price, int addHealth)
    {        
        _healthShop.TrySellItem(index, price, addHealth);
    }
    public void OnBuying(int newNeuron, int newPrice, int newHealth, int index)
    {
        NeuronCollectorView.ChangeNeuronView(newNeuron);
        _healthItemView[index].UpdateValues(newPrice);
        _healthShopView.UpdateCurrentHealth(newHealth);
    }

    public void RequestCurrentHealth(int index)
    {
        _healthShop.CurrentHealthRequest(index);
    }

    public void OnGiveCurrentHealth(int index, int health)
    {
        _healthShopView.SetCurrentHealth(health);
    }

    public void RequestOpenItem(int index, int price)
    {
        _healthShop.OpenItemRequest(index, price);
    }

    public void OnOpenItem(int index)
    {
        _healthItemView[index].OpenItem();
    }

    public void RequestLockItem(int index, int price)
    {
        _healthShop.LockItemRequest(index, price);
    }

    public void OnLockItem(int index)
    {
        _healthItemView[index].LockItem();
    }

    public void RequestUnlockItem(int index, int price)
    {
        _healthShop.UnlockItemRequest(index, price);
    }

    public void OnUnlockItem(int index)
    {
        _healthItemView[index].UnlockItem();
    }
}
