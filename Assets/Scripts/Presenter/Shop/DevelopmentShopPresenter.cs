using System.Collections.Generic;
using UnityEngine;

public class DevelopmentShopPresenter : ShopPresenter
{
    private DevelopmentShop _developmentShop;
    private DevelopmentShopView _developmentShopView;
    private List<DevelopmentItemView> _developmentItemView = new List<DevelopmentItemView>();

    public void Init(DevelopmentShop developmentShop, NeuronCollectorView neuronCollectorView, DevelopmentShopView developmentShopView, List<DevelopmentItemView> developmentItemView)
    {
        _developmentShop = developmentShop;
        NeuronCollectorView = neuronCollectorView;
        _developmentShopView = developmentShopView;

        for (int i = 0; i < developmentItemView.Count; i++)
            _developmentItemView.Add(developmentItemView[i]);

    }

    public void Enable()
    {
        _developmentShopView.OnSellButtonClick += TrySell;
        _developmentShopView.OnRequsetNeuronPerClick += RequestNeuronPerClick;
        _developmentShopView.OnRequestOpenItem += RequestOpenItem;
        _developmentShopView.OnRequsetLockItem += RequestLockItem;
        _developmentShopView.OnRequsetUnlockItem += RequestUnlockItem;

        _developmentShop.SellDevelopmentItem += OnBuying;
        _developmentShop.GiveNeuronPerClick += OnGiveNeuronPerClick;
        _developmentShop.OpenItem += OnOpenItem;
        _developmentShop.LockItem += OnLockItem;
        _developmentShop.UnlockItem += OnUnlockItem;
    }

    public void Disable()
    {
        _developmentShopView.OnSellButtonClick -= TrySell;
        _developmentShopView.OnRequsetNeuronPerClick -= RequestNeuronPerClick;
        _developmentShopView.OnRequestOpenItem -= RequestOpenItem;
        _developmentShopView.OnRequsetLockItem -= RequestLockItem;
        _developmentShopView.OnRequsetUnlockItem -= RequestUnlockItem;

        _developmentShop.SellDevelopmentItem -= OnBuying;
        _developmentShop.GiveNeuronPerClick -= OnGiveNeuronPerClick;
        _developmentShop.OpenItem -= OnOpenItem;
        _developmentShop.LockItem -= OnLockItem;
        _developmentShop.UnlockItem -= OnUnlockItem;
    }

    public void TrySell(int index, int price, int addNeuronPerClick)
    {
        _developmentShop.TrySellItem(index, price, addNeuronPerClick);
    }

    public void OnBuying(int newNeuron, int newPrice, int newNeuronPerClick, int index)
    {
        NeuronCollectorView.ChangeNeuronView(newNeuron);
        _developmentItemView[index].UpdateValues(newPrice);
        _developmentShopView.UpdateNeuronPerClick(newNeuronPerClick);
    }

    public void RequestNeuronPerClick(int index)
    {
        _developmentShop.NeuronPerClickRequest(index);
    }

    public void OnGiveNeuronPerClick(int index, int neuronPerClick)
    {
        _developmentShopView.SetNeuronPerClick(neuronPerClick);
    }

    public void RequestOpenItem(int index, int price)
    {
        _developmentShop.OpenItemRequest(index, price);
    }

    public void OnOpenItem(int index)
    {
        _developmentItemView[index].OpenItem();
    }

    public void RequestLockItem(int index, int price)
    {
        _developmentShop.LockItemRequest(index, price);
    }

    public void OnLockItem(int index)
    {
        _developmentItemView[index].LockItem();
    }

    public void RequestUnlockItem(int index, int price)
    {
        _developmentShop.UnlockItemRequest(index, price);
    }

    public void OnUnlockItem(int index)
    {
        _developmentItemView[index].UnlockItem();
    }
}
