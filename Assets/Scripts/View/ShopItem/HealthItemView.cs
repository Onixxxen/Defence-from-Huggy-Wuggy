using System;
using UnityEngine;
using YG;

public class HealthItemView : ShopItemView
{
    private int _price;
    private HealthItemView[] _items;
    private SettingLanguageView _settingLanguage;

    public event Action<int> TryGetCurentHealth;    
    public event Action<int, int, int> OnHealthSellButton;

    public event Action<int, int> TryOpenItem;
    public event Action<int, int> TryCloseItem;
    public event Action<int, int> TryLockItem;
    public event Action<int, int> TryUnlockItem;

    private void OnEnable()
    {
        _price = PriceValue;

        TryGetCurentHealth?.Invoke(Index);

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
        _items = FindObjectsOfType<HealthItemView>(true);        

        TryGetCurentHealth?.Invoke(Index);
        TryOpenItem?.Invoke(Index, _price);

        SellButton.onClick.AddListener(SellItem);
    }

    public void SellItem()
    {
        _price = PriceValue;

        int addHealth = ImprovementValue;

        OnHealthSellButton?.Invoke(Index, _price, addHealth);

        for (int i = 0; i < _items.Length; i++)
            _items[i].TryLockItem?.Invoke(_items[i].Index, _items[i]._price);
    }

    public void Render(HealthItem item, int index)
    {
        SetName(item);
        Index = index;
        Icon.sprite = item.Icon;
        Improvement.text = $"+{FormatNumberExtension.FormatNumber(item.AddHealth)}";
        Price.text = $"{FormatNumberExtension.FormatNumber(item.Price)}";
        PriceValue = item.Price;
        ImprovementValue = item.AddHealth;
    }

    public void SetName(HealthItem item)
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

    public void LoadHealthItemPriceData()
    {
        PriceValue = YandexGame.savesData.SavedHealthItemPrices[Index];
        ClosePanel.gameObject.SetActive(!YandexGame.savesData.HealthItemOpenStatus[Index]);
        UpdateValues(PriceValue);
    }
}
