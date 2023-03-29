using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace YG.Example
{
    public class SaverData : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private DevelopmentShopView _developmentShopView;
        [SerializeField] private HealthShopView _healthShopView;
        [SerializeField] private ArmorShopView _armorShopView;
        [SerializeField] private DayChangerView _dayChangerView;
        [SerializeField] private ObjectPoolView _objectPoolView;

        private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
        private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

        private void Start()
        {
            if (YandexGame.SDKEnabled)
                GetLoad();
        }

        public void SaveDayCount(int dayCount)
        {
            YandexGame.savesData.SavedDay = dayCount;
            YandexGame.SaveProgress();
        }

        public void SaveNeuronCount(int neuronCount)
        {
            YandexGame.savesData.SavedNeuron = neuronCount;
            YandexGame.SaveProgress();
        }

        public void SaveNeuronPerClick(int perClick)
        {
            YandexGame.savesData.SavedNeuronPerClick = perClick;
            YandexGame.SaveProgress();
        }

        public void SaveHealthMaxCount(int healthCount)
        {
            YandexGame.savesData.SavedMaxHealth = healthCount;
            YandexGame.SaveProgress();
        }

        public void SaveArmorMaxCount(int armorCount)
        {
            YandexGame.savesData.SavedMaxArmor = armorCount;
            YandexGame.SaveProgress();
        }

        public void SaveHealthCount(int healthCount)
        {
            YandexGame.savesData.SavedHealthCount = healthCount;
            YandexGame.SaveProgress();
        }

        public void SaveArmorCount(int armorCount)
        {
            YandexGame.savesData.SavedArmorCount = armorCount;
            YandexGame.SaveProgress();
        }

        public void SaveEnemyDamage(int newDamage)
        {
            YandexGame.savesData.SavedEnemyDamage = newDamage;
            YandexGame.SaveProgress();
        }

        public void SaveTime(float time)
        {
            YandexGame.savesData.SavedTime = time;
            YandexGame.SaveProgress();
        }

        public void SaveDevelopmentItemPrices(int index, int price)
        {
            YandexGame.savesData.SavedDevelopmentItemPrices[index] = price;
            YandexGame.SaveProgress();            
        }

        public void SaveHealthItemPrices(int index, int price)
        {
            YandexGame.savesData.SavedHealthItemPrices[index] = price;
            YandexGame.SaveProgress();
        }

        public void SaveArmorItemPrices(int index, int price)
        {
            YandexGame.savesData.SavedArmorItemPrices[index] = price;
            YandexGame.SaveProgress();
        }

        public void SaveDevelopmentOpenStatus(int index, bool status)
        {
            YandexGame.savesData.DevelopmentItemOpenStatus[index] = status;
        }

        public void SaveHealthOpenStatus(int index, bool status)
        {
            YandexGame.savesData.HealthItemOpenStatus[index] = status;
        }

        public void SaveArmorOpenStatus(int index, bool status)
        {
            YandexGame.savesData.ArmorItemOpenStatus[index] = status;
        }

        public void Load() => YandexGame.LoadProgress();

        public void GetLoad()
        {
            _gameController.Neuron.LoadNeuronData();
            _gameController.Health.LoadHealthData();
            _gameController.Armor.LoadArmorData();
            _gameController.DayChanger.LoadDayData();
            _dayChangerView.LoadTimeData();

            for (int i = 0; i < _objectPoolView.Pool.Count; i++)
                _objectPoolView.Pool[i].LoadEnemyData();

            for (int i = 0; i < _developmentShopView.SpawnedItem.Count; i++)
                if (YandexGame.savesData.SavedDevelopmentItemPrices[i] != 0)
                    _developmentShopView.SpawnedItem[i].LoadDevelopmentItemPriceData();               

            for (int i = 0; i < _healthShopView.SpawnedItem.Count; i++)
                if (YandexGame.savesData.SavedHealthItemPrices[i] != 0)
                    _healthShopView.SpawnedItem[i].LoadHealthItemPriceData();

            for (int i = 0; i < _armorShopView.SpawnedItem.Count; i++)
                if (YandexGame.savesData.SavedArmorItemPrices[i] != 0)
                    _armorShopView.SpawnedItem[i].LoadArmorItemPriceData();
        }
    }
}