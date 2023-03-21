using System.Collections.Generic;
using UnityEngine;

public class DevelopmentShopController : ShopController
{
    private DevelopmentShopPresenter _developmentPresenter;
    private List<DevelopmentItemView> _developmentItemView = new List<DevelopmentItemView>();

    private void OnEnable()
    {
        if (_developmentPresenter != null)
            _developmentPresenter.Enable();
    }

    private void OnDisable()
    {
        _developmentPresenter.Disable();
    }

    private void Start()
    {
        GameController = FindObjectOfType<GameController>();
        NeuronCollectorView = FindObjectOfType<NeuronCollectorView>();

        _developmentPresenter = new DevelopmentShopPresenter();

        var developmentShopView = FindObjectOfType<DevelopmentShopView>();

        for (int i = 0; i < developmentShopView.SpawnedItem.Count; i++)
            _developmentItemView.Add(developmentShopView.SpawnedItem[i]);

        _developmentPresenter.Init(GameController.DevelopmentShop, NeuronCollectorView, developmentShopView, _developmentItemView);
    }
}
