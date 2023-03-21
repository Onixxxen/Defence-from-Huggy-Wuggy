using System;

public class ArmorItemView : ShopItemView
{
    private int _price;
    private ArmorItemView[] _items;

    public event Action<int> TryGetCurentArmor;
    public event Action<int, int, int> OnArmorSellButton;

    public event Action<int, int> TryOpenItem;
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

    private void Start()
    {
        _price = PriceValue;
        _items = FindObjectsOfType<ArmorItemView>();

        TryGetCurentArmor?.Invoke(Index);
        TryOpenItem?.Invoke(Index, _price);

        SellButton.onClick.AddListener(SellItem);
    }

    public void SellItem()
    {
        _price = PriceValue;

        int addArmor = ImprovementValue;

        OnArmorSellButton?.Invoke(Index, _price, addArmor);

        for (int i = 0; i < _items.Length; i++)
            _items[i].TryLockItem?.Invoke(_items[i].Index, _items[i]._price);
    }

    public void Render(ArmorItem item, int index)
    {
        Index = index;
        Icon.sprite = item.Icon;
        Name.text = item.Name;
        Improvement.text = $"+{FormatNumberExtension.FormatNumber(item.AddArmor)}";
        Price.text = $"{FormatNumberExtension.FormatNumber(item.Price)}";
        PriceValue = item.Price;
        ImprovementValue = item.AddArmor;
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

    public void LockItem()
    {
        LockPanel.gameObject.SetActive(true);
    }

    public void UnlockItem()
    {
        LockPanel.gameObject.SetActive(false);
    }
}
