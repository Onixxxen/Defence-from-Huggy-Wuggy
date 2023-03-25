using System;
using System.Collections.Generic;
using UnityEngine;

public class ArmorShopView : ShopView
{
    [SerializeField] private List<ArmorItem> _shopItems;
    [SerializeField] protected ArmorItemView _template;

    public List<ArmorItem> ShopItems => _shopItems;
    public List<ArmorItemView> SpawnedItem { get; private set; } = new List<ArmorItemView>();

    public event Action<int, int, int> OnSellButtonClick;
    public event Action<int> OnRequsetCurrentArmor;
    public event Action<int, int> OnRequestOpenItem;
    public event Action<int, int> OnRequestCloseItem;
    public event Action<int, int> OnRequsetLockItem;
    public event Action<int, int> OnRequsetUnlockItem;

    private void Awake()
    {
        for (int i = 0; i < _shopItems.Count; i++)
            AddItem(_shopItems[i], i);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void AddItem(ArmorItem shopItem, int index)
    {
        var item = Instantiate(_template, _container.transform);

        item.OnArmorSellButton += TrySellArmorItem;
        item.TryGetCurentArmor += TryRequestCurrentArmor;
        item.TryOpenItem += TryRequestOpenItem;
        item.TryCloseItem += TryRequestCloseItem;
        item.TryLockItem += TryRequestLockItem;
        item.TryUnlockItem += TryRequestUnlockItem;

        item.Render(shopItem, index);

        SpawnedItem.Add(item);
    }

    public void UpdateCurrentArmor(int currentArmor)
    {
        CurrentValue.text = $"{FormatNumberExtension.FormatNumber(currentArmor)}";
    }

    public void SetCurrentArmor(int currentArmor)
    {
        CurrentValue.text = $"{FormatNumberExtension.FormatNumber(currentArmor)}";
    }

    private void TrySellArmorItem(int index, int price, int addArmor)
    {
        OnSellButtonClick?.Invoke(index, price, addArmor);
    }

    private void TryRequestCurrentArmor(int index)
    {
        OnRequsetCurrentArmor?.Invoke(index);
    }

    private void TryRequestOpenItem(int index, int price)
    {
        OnRequestOpenItem?.Invoke(index, price);
    }

    private void TryRequestCloseItem(int index, int price)
    {
        OnRequestCloseItem?.Invoke(index, price);
    }

    private void TryRequestLockItem(int index, int price)
    {
        OnRequsetLockItem?.Invoke(index, price);
    }

    private void TryRequestUnlockItem(int index, int price)
    {
        OnRequsetUnlockItem?.Invoke(index, price);
    }
}
