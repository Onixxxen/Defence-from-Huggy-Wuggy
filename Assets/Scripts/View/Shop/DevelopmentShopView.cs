using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;
using YG.Example;

public class DevelopmentShopView : ShopView
{
    [SerializeField] private List<DevelopmentItem> _shopItems;
    [SerializeField] private DevelopmentItemView _template;
    [SerializeField] private TMP_Text _count;
    [SerializeField] private TMP_Text _perClickText;
    [SerializeField] private SaverData _saverData;

    public List<DevelopmentItem> ShopItems => _shopItems;
    public List<DevelopmentItemView> SpawnedItem { get; private set; } = new List<DevelopmentItemView>();

    public event Action<int, int, int> OnSellButtonClick;
    public event Action<int> OnRequsetNeuronPerClick;
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

    private void AddItem(DevelopmentItem shopItem, int index)
    {
        var item = Instantiate(_template, _container.transform);

        item.OnDevelopmentSellButton += TrySellDevelopmentItem;
        item.TryGetNeuronPerClick += TryRequestNeuronPerClick;
        item.TryOpenItem += TryRequestOpenItem;
        item.TryCloseItem += TryRequestCloseItem;
        item.TryLockItem += TryRequestLockItem;
        item.TryUnlockItem += TryRequestUnlockItem;

        item.Render(shopItem, index);

        SpawnedItem.Add(item);
    }

    public void UpdateNeuronPerClick(int neuronPerClick)
    {
        _count.text = $"{neuronPerClick}";
        CurrentValue.text = $"{FormatNumberExtension.FormatNumber(neuronPerClick)}{_perClickText.text}";
    }

    public void SetNeuronPerClick(int neuronPerClick)
    {
        _count.text = $"{neuronPerClick}";
        CurrentValue.text = $"{FormatNumberExtension.FormatNumber(neuronPerClick)}{_perClickText.text}";
    }

    private void TrySellDevelopmentItem(int index, int price, int addNeuronPerClick)
    {
        OnSellButtonClick?.Invoke(index, price, addNeuronPerClick);
        //item.OnDevelopmentSellButton -= TrySellDevelopmentItem; // не знаю почему, но если тут отписываться, то все ломается
    }

    private void TryRequestNeuronPerClick(int index)
    {
        OnRequsetNeuronPerClick?.Invoke(index);
        //item.TryGetNeuronPerClick -= TryRequestNeuronPerClick; // не знаю почему, но если тут отписываться, то все ломается
    }

    private void TryRequestOpenItem(int index, int price)
    {
        OnRequestOpenItem?.Invoke(index, price);
        //item.TryOpenItem -= TryRequestOpenItem; // не знаю почему, но если тут отписываться, то все ломается
    }

    private void TryRequestCloseItem(int index, int price)
    {
        OnRequestCloseItem?.Invoke(index, price);
        //item.TryCloseItem -= TryRequestCloseItem; // не знаю почему, но если тут отписываться, то все ломается
    }

    private void TryRequestLockItem(int index, int price)
    {
        OnRequsetLockItem?.Invoke(index, price);
        //item.TryLockItem -= TryRequestLockItem; // не знаю почему, но если тут отписываться, то все ломается
    }

    private void TryRequestUnlockItem(int index, int price)
    {
        OnRequsetUnlockItem?.Invoke(index, price);
        //item.TryUnlockItem -= TryRequestUnlockItem; // не знаю почему, но если тут отписываться, то все ломается
    }
}
