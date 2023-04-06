using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthShopView : ShopView
{
    [SerializeField] private List<HealthItem> _shopItems;
    [SerializeField] private HealthItemView _template;  
    public List<HealthItem> ShopItems => _shopItems;
    public List<HealthItemView> SpawnedItem { get; private set; } = new List<HealthItemView>();

    public event Action<int, int, int> OnSellButtonClick;
    public event Action<int> OnRequsetCurrentHealth;
    public event Action<int, int> OnRequestOpenItem;
    public event Action<int, int> OnRequestCloseItem;
    public event Action<int, int> OnRequsetLockItem;
    public event Action<int, int> OnRequsetUnlockItem;

    private void OnEnable()
    {
        for (int i = 0; i < _shopItems.Count; i++)
            SpawnedItem[i].SetName(_shopItems[i]);
    }

    private void Awake()
    {
        for (int i = 0; i < _shopItems.Count; i++)
            AddItem(_shopItems[i], i);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void AddItem(HealthItem shopItem, int index)
    {
        var item = Instantiate(_template, _container.transform);

        item.OnHealthSellButton += TrySellHealthItem;
        item.TryGetCurentHealth += TryRequestCurrentHealth;
        item.TryOpenItem += TryRequestOpenItem;
        item.TryCloseItem += TryRequestCloseItem;
        item.TryLockItem += TryRequestLockItem;
        item.TryUnlockItem += TryRequestUnlockItem;

        item.Render(shopItem, index);

        SpawnedItem.Add(item);
    }

    public void UpdateCurrentHealth(int currentHealth)
    {
        CurrentValue.text = $"{FormatNumberExtension.FormatNumber(currentHealth)}";
    }

    public void SetCurrentHealth(int currentHealth)
    {
        CurrentValue.text = $"{FormatNumberExtension.FormatNumber(currentHealth)}";
    }

    private void TrySellHealthItem(int index, int price, int addHealth)
    {        
        OnSellButtonClick?.Invoke(index, price, addHealth);
    }

    private void TryRequestCurrentHealth(int index)
    {
        OnRequsetCurrentHealth?.Invoke(index);
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
