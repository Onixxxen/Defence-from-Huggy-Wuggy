using System;
using UnityEngine;
using YG;

public class DevelopmentItemView : ShopItemView
{
    private int _price;
    private DevelopmentItemView[] _items;
    private SettingLanguageView _settingLanguage;

    public event Action<int> TryGetNeuronPerClick;    
    public event Action<int, int, int> OnDevelopmentSellButton;

    public event Action<int, int> TryOpenItem;
    public event Action<int, int> TryCloseItem;
    public event Action<int, int> TryLockItem;
    public event Action<int, int> TryUnlockItem;

    private void OnEnable()
    {
        _price = PriceValue;

        TryGetNeuronPerClick?.Invoke(Index);

        if (ClosePanel.activeSelf == true)
            TryOpenItem?.Invoke(Index, _price);

        if (LockPanel.activeSelf == true)
            TryUnlockItem?.Invoke(Index, _price);
    }

    private void Awake()
    {
        _settingLanguage = FindObjectOfType<SettingLanguageView>(true);
    }

    private void Start()
    {
        _price = PriceValue;
        _items = FindObjectsOfType<DevelopmentItemView>(true);

        TryGetNeuronPerClick?.Invoke(Index);
        TryOpenItem?.Invoke(Index, _price);

        SellButton.onClick.AddListener(SellItem);
    }

    public void SellItem()
    {
        _price = PriceValue;

        int addNeuronPerClick = ImprovementValue;

        OnDevelopmentSellButton?.Invoke(Index, _price, addNeuronPerClick);
        Instantiate(BuyEffect, SellButton.transform.position, Quaternion.identity);

        for (int i = 0; i < _items.Length; i++)
            _items[i].TryLockItem?.Invoke(_items[i].Index, _items[i]._price);
    }

    public void Render(DevelopmentItem item, int index)
    {
        SetName(item);
        Index = index;
        Icon.sprite = item.Icon;        
        Improvement.text = $"+{FormatNumberExtension.FormatNumber(item.AddNeuronPerClick)}";
        Price.text = $"{FormatNumberExtension.FormatNumber(item.Price)}";
        ClosePrice.text = $"{FormatNumberExtension.FormatNumber(item.Price)}"; 
        PriceValue = item.Price;
        ImprovementValue = item.AddNeuronPerClick;          
    }

    public void SetName(DevelopmentItem item)
    {
        if (_settingLanguage.CurrentLanguage == "ru")
            Name.text = item.RuName;
        else if (_settingLanguage.CurrentLanguage == "en")
            Name.text = item.EnName;
        else if (_settingLanguage.CurrentLanguage == "tr")
            Name.text = item.TrName;
        else if (_settingLanguage.CurrentLanguage == "uk")
            Name.text = item.UkName;
    }

    public void UpdateValues(int price)
    {
        PriceValue = price;
        Price.text = $"{FormatNumberExtension.FormatNumber(price)}";
        ClosePrice.text = $"{FormatNumberExtension.FormatNumber(price)}";
    }

    public void OpenItem()
    {
        ClosePanel.gameObject.SetActive(false);
    }

    public void RequestCloseItem()
    {
        if (ClosePanel.activeSelf == false)
            TryCloseItem?.Invoke(Index, _price);
    }

    public void CloseItem()
    {
        ClosePanel.gameObject.SetActive(true);
    }

    public void LockItem()
    {
        LockPanel.gameObject.SetActive(true);
    }

    public void UnlockItem()
    {
        LockPanel.gameObject.SetActive(false);
    }

    public void LoadDevelopmentItemPriceData()
    {
        PriceValue = YandexGame.savesData.SavedDevelopmentItemPrices[Index];
        ClosePanel.gameObject.SetActive(!YandexGame.savesData.DevelopmentItemOpenStatus[Index]);
        UpdateValues(PriceValue);
    }
}
