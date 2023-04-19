using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ArmorItemView : ShopItemView
{
    [SerializeField] private ArmorSellButton _tutorialSellButton;

    private int _price;
    private ArmorItemView[] _items;
    private SettingLanguageView _settingLanguage;

    public ArmorSellButton TutorialSellButton => _tutorialSellButton;

    public event Action<int> TryGetCurentArmor;
    public event Action<int, int, int> OnArmorSellButton;

    public event Action<int, int> TryOpenItem;
    public event Action<int, int> TryCloseItem;
    public event Action<int, int> TryLockItem;
    public event Action<int, int> TryUnlockItem;

    private void OnEnable()
    {
        _price = PriceValue;

        TryGetCurentArmor?.Invoke(Index);

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
        _items = FindObjectsOfType<ArmorItemView>(true);

        TryGetCurentArmor?.Invoke(Index);
        TryOpenItem?.Invoke(Index, _price);

        SellButton.onClick.AddListener(SellItem);
        _tutorialSellButton.GetComponent<Button>().onClick.AddListener(SellItem);
    }

    public void SellItem()
    {
        _price = PriceValue;

        int addArmor = ImprovementValue;

        OnArmorSellButton?.Invoke(Index, _price, addArmor);
        Instantiate(BuyEffect, SellButton.transform.position, Quaternion.identity);

        for (int i = 0; i < _items.Length; i++)
            _items[i].TryLockItem?.Invoke(_items[i].Index, _items[i]._price);
    }

    public void Render(ArmorItem item, int index)
    {
        SetName(item);
        Index = index;
        Icon.sprite = item.Icon;
        Improvement.text = $"+{FormatNumberExtension.FormatNumber(item.AddArmor)}";
        Price.text = $"{FormatNumberExtension.FormatNumber(item.Price)}";
        ClosePrice.text = $"{FormatNumberExtension.FormatNumber(item.Price)}";
        PriceValue = item.Price;
        ImprovementValue = item.AddArmor;
    }

    public void SetName(ArmorItem item)
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

    public void LoadArmorItemPriceData()
    {
        PriceValue = YandexGame.savesData.SavedArmorItemPrices[Index];
        ClosePanel.gameObject.SetActive(!YandexGame.savesData.ArmorItemOpenStatus[Index]);
        UpdateValues(PriceValue);
    }
}
