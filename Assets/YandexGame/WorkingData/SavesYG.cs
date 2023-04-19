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
        public int SavedMaxDay = 0;
        public int SavedNeuron = 0;
        public int SavedNeuronPerClick = 5000;
        public int SavedMaxHealth = 10;
        public int SavedMaxArmor = 10;
        public int SavedEnemyDamage = 1;
        public float SavedTime = 0.57f;
        public float SavedSecondBetweenSpawn = 2.3f;
        public bool IsTowerDefenceLoaded = false;
        public bool IsClickerLoaded = false;
        public bool IsLanguageLoaded = false;
        public bool IsTutorialCompleted = false;
        public int SavedTutorialStage = 1;
        public int SavedHealthCount = 10;
        public int SavedArmorCount = 10;
        public string SavedLanguage = "ru";

        public int[] SavedDevelopmentItemPrices = new int[11];
        public int[] SavedHealthItemPrices = new int[10];        
        public int[] SavedArmorItemPrices = new int[10];

        public bool[] DevelopmentItemOpenStatus = new bool[11];
        public bool[] HealthItemOpenStatus = new bool[10];
        public bool[] ArmorItemOpenStatus = new bool[10];

        public void ResetData()
        {
            SavedDay = 0;
            SavedNeuron = 0;
            SavedNeuronPerClick = 1;
            SavedMaxHealth = 10;
            SavedMaxArmor = 10;
            SavedEnemyDamage = 1;
            SavedTime = 0.57f;
            SavedSecondBetweenSpawn = 2.3f;
            SavedDevelopmentItemPrices = new int[11];
            SavedHealthItemPrices = new int[10];
            SavedArmorItemPrices = new int[10];
            DevelopmentItemOpenStatus = new bool[11];
            HealthItemOpenStatus = new bool[10];
            ArmorItemOpenStatus = new bool[10];
        }
    }
}
