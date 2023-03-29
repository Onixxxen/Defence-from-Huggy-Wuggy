using System.Collections.Generic;

public class ArmorShopController : ShopController
{
    private ArmorShopPresenter _armorPresenter;
    private List<ArmorItemView> _armorItemView = new List<ArmorItemView>();

    public List<ArmorItemView> ArmorItemView => _armorItemView;

    private void OnEnable()
    {
        if (_armorPresenter != null)
            _armorPresenter.Enable();
    }

    private void OnDisable()
    {
        if (_armorPresenter != null)
            _armorPresenter.Disable();
    }

    private void Start()
    {
        GameController = FindObjectOfType<GameController>();
        NeuronCollectorView = FindObjectOfType<NeuronCollectorView>();

        _armorPresenter = new ArmorShopPresenter();

        var amorShopView = FindObjectOfType<ArmorShopView>(true);

        for (int i = 0; i < amorShopView.SpawnedItem.Count; i++)
            _armorItemView.Add(amorShopView.SpawnedItem[i]);

        _armorPresenter.Init(GameController.ArmorShop, NeuronCollectorView, amorShopView, _armorItemView); ;
    }
}
