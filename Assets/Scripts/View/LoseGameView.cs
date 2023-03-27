using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseGameView : MonoBehaviour
{
    [SerializeField] private DayChangerView _dayChangerView;
    [SerializeField] private TMP_Text _totalDay;
    [SerializeField] private Button _resetButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private GameObject _mainUI;
    [SerializeField] private ObjectPoolView _objectPool;

    [Header("Shops")]
    [SerializeField] private DevelopmentShopView _developmentShopView;
    [SerializeField] private HealthShopView _healthShopView;
    [SerializeField] private ArmorShopView _armorShopView;

    public DayChangerView DayChangerView => _dayChangerView;

    public event Action TryActiveLoseGame;
    public event Action TryGetDayCount;

    private void OnEnable()
    {
        TryGetDayCount?.Invoke();
        _mainUI.SetActive(false);
    }

    private void OnDisable()
    {
        _mainUI.SetActive(true);
    }

    private void Start()
    {
        _resetButton.onClick.AddListener(RequestLoseGame);
    }

    public void RequestLoseGame()
    {
        gameObject.SetActive(true);
        TryActiveLoseGame?.Invoke();
    }

    public void SetDayCount(int day)
    {
        _totalDay.text = day.ToString();
    }

    public void LoseGame()
    {
        for (int i = 0; i < _developmentShopView.SpawnedItem.Count; i++)
        {
            _developmentShopView.SpawnedItem[i].UpdateValues(_developmentShopView.ShopItems[i].Price);
            _developmentShopView.SpawnedItem[i].RequestCloseItem();
        }

        for (int i = 0; i < _healthShopView.SpawnedItem.Count; i++)
        {
            _healthShopView.SpawnedItem[i].UpdateValues(_healthShopView.ShopItems[i].Price);
            _healthShopView.SpawnedItem[i].RequestCloseItem();
        }

        for (int i = 0; i < _armorShopView.SpawnedItem.Count; i++)
        {
            _armorShopView.SpawnedItem[i].UpdateValues(_armorShopView.ShopItems[i].Price);
            _armorShopView.SpawnedItem[i].RequestCloseItem();
        }

        for (int i = 0; i < _objectPool.Pool.Count; i++)
            _objectPool.Pool[i].gameObject.SetActive(false);

        _dayChangerView.ChangeTime(0.57f);
        gameObject.SetActive(false);
    }
}
