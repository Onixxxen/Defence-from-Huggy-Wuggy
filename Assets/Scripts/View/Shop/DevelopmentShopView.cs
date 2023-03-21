using System;
using System.Collections.Generic;
using UnityEngine;

public class DevelopmentShopView : ShopView
{
    [SerializeField] private List<DevelopmentItem> _shopItems;
    [SerializeField] private DevelopmentItemView _template;

    public List<DevelopmentItemView> SpawnedItem { get; private set; } = new List<DevelopmentItemView>();

    public event Action<int, int, int> OnSellButtonClick;
    public event Action<int> OnRequsetNeuronPerClick;
    public event Action<int, int> OnRequestOpenItem;
    public event Action<int, int> OnRequsetLockItem;
    public event Action<int, int> OnRequsetUnlockItem;

    private void Awake()
    {
        for (int i = 0; i < _shopItems.Count; i++)
            AddItem(_shopItems[i], i);
    }

    private void AddItem(DevelopmentItem shopItem, int index)
    {
        var item = Instantiate(_template, _container.transform);

        item.OnDevelopmentSellButton += TrySellDevelopmentItem;
        item.TryGetNeuronPerClick += TryRequestNeuronPerClick;
        item.TryOpenItem += TryRequestOpenItem;
        item.TryLockItem += TryRequestLockItem;
        item.TryUnlockItem += TryRequestUnlockItem;

        item.Render(shopItem, index);

        SpawnedItem.Add(item);
    }

    public void UpdateNeuronPerClick(int neuronPerClick)
    {
        CurrentValue.text = $"{FormatNumberExtension.FormatNumber(neuronPerClick)}/клик";
    }

    public void SetNeuronPerClick(int neuronPerClick)
    {
        CurrentValue.text = $"{FormatNumberExtension.FormatNumber(neuronPerClick)}/клик";
    }

    private void TrySellDevelopmentItem(int index, int price, int addNeuronPerClick)
    {
        OnSellButtonClick?.Invoke(index, price, addNeuronPerClick);
        //item.OnDevelopmentSellButton -= TrySellDevelopmentItem;
    }

    private void TryRequestNeuronPerClick(int index)
    {
        OnRequsetNeuronPerClick?.Invoke(index);
        //item.TryGetNeuronPerClick -= TryRequestNeuronPerClick;
    }

    private void TryRequestOpenItem(int index, int price)
    {
        OnRequestOpenItem?.Invoke(index, price);
        //item.TryOpenItem -= TryRequestOpenItem;
    }   

    private void TryRequestLockItem(int index, int price)
    {
        OnRequsetLockItem?.Invoke(index, price);
        //item.TryLockItem -= TryRequestLockItem;
    }    

    private void TryRequestUnlockItem(int index, int price)
    {
        OnRequsetUnlockItem?.Invoke(index, price);
        //item.TryUnlockItem -= TryRequestUnlockItem;
    }    
}
