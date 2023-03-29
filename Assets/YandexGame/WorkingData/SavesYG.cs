using System;
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public int SavedDay = 0;
        public int SavedNeuron = 0;
        public int SavedNeuronPerClick = 1;
        public int SavedMaxHealth = 10;
        public int SavedMaxArmor = 10;
        public int SavedEnemyDamage = 1;
        public float SavedTime = 0.57f;
        public bool TowerDefenceLoaded = false;
        public int SavedHealthCount = 10;
        public int SavedArmorCount = 10;

        public int[] SavedDevelopmentItemPrices = new int[11];
        public int[] SavedHealthItemPrices = new int[10];        
        public int[] SavedArmorItemPrices = new int[10];

        public bool[] DevelopmentItemOpenStatus = new bool[11];
        public bool[] HealthItemOpenStatus = new bool[10];
        public bool[] ArmorItemOpenStatus = new bool[10];
    }
}
