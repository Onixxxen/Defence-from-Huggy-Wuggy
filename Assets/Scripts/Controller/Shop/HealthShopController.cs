using System.Collections.Generic;
using UnityEngine;

public class HealthShopController : ShopController
{
    private HealthShopPresenter _healthPresenter;
    private List<HealthItemView> _healthItemView = new List<HealthItemView>();

    public List<HealthItemView> HealthItemView => _healthItemView;

    private void OnEnable()
    {
        if (_healthPresenter != null)
            _healthPresenter.Enable();
    }

    private void OnDisable()
    {
        if (_healthPresenter != null)
            _healthPresenter.Disable();
    }

    private void Start()
    {
        GameController = FindObjectOfType<GameController>();
        NeuronCollectorView = FindObjectOfType<NeuronCollectorView>();

        _healthPresenter = new HealthShopPresenter();

        var healthShopView = FindObjectOfType<HealthShopView>(true);

        for (int i = 0; i < healthShopView.SpawnedItem.Count; i++)
            _healthItemView.Add(healthShopView.SpawnedItem[i]);

        _healthPresenter.Init(GameController.HealthShop, NeuronCollectorView, healthShopView, _healthItemView);
    }
}
