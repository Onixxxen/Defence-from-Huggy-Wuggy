using UnityEngine;
using UnityEngine.UI;
using YG;

public class SettingLanguageView : MonoBehaviour
{
    [Header("Choise Language Button")]
    [SerializeField] private Button _ruLanguageButton;
    [SerializeField] private Button _enLanguageButton;
    [SerializeField] private Button _trLanguageButton;
    [SerializeField] private Button _ukLanguageButton;

    [Header("Current Language Panel")]
    [SerializeField] private GameObject _currentLanguagePanel;

    [Header("Setting Panel")]
    [SerializeField] private GameObject _settingPanel;

    [Header("Setting Panel")]
    [SerializeField] private TutorialView _tutorialView;


    private TranslateView[] _translateView;
    private string _currentLanguage = "ru";

    public string CurrentLanguage => _currentLanguage;
    public GameObject SettingPanel => _settingPanel;

    private void Start()
    {
        _translateView = FindObjectsOfType<TranslateView>(true);

        //CheckYandexLanguage();
        //_settingPanel.SetActive(false);

        _ruLanguageButton.onClick.AddListener(SetRussianLanguage);
        _enLanguageButton.onClick.AddListener(SetEnglishLanguage);
        _trLanguageButton.onClick.AddListener(SetTurkeyLanguage);
        _ukLanguageButton.onClick.AddListener(SetUkrainianLanguage);
    }

    private void SetRussianLanguage()
    {
        foreach (TranslateView translateView in _translateView)
            translateView.SetRussianLanguage();

        ChangeCurrentLanguage(_ruLanguageButton.transform);
        _currentLanguage = "ru";
        SaveLanguage();
        SetTutorualLanguage();
    }

    private void SetEnglishLanguage()
    {
        foreach (TranslateView translateView in _translateView)
            translateView.SetEnglishLanguage();

        ChangeCurrentLanguage(_enLanguageButton.transform);
        _currentLanguage = "en";
        SaveLanguage();
        SetTutorualLanguage();
    }

    private void SetTurkeyLanguage()
    {
        foreach (TranslateView translateView in _translateView)
            translateView.SetTurkeyLanguage();

        ChangeCurrentLanguage(_trLanguageButton.transform);
        _currentLanguage = "tr";
        SaveLanguage();
        SetTutorualLanguage();
    }

    private void SetUkrainianLanguage()
    {
        foreach (TranslateView translateView in _translateView)
            translateView.SetUkrainianLanguage();

        ChangeCurrentLanguage(_ukLanguageButton.transform);
        _currentLanguage = "uk";
        SaveLanguage();
        SetTutorualLanguage();
    }

    private void ChangeCurrentLanguage(Transform button)
    {
        _currentLanguagePanel.transform.SetParent(button);
        _currentLanguagePanel.transform.position = button.position;
    }

    private void SaveLanguage()
    {
        if (YandexGame.savesData.IsLanguageLoaded)
            YandexGame.savesData.SavedLanguage = _currentLanguage;
    }

    private void SetTutorualLanguage()
    {
        _tutorialView.ChangeCurrentDialog(_currentLanguage);
    }

    public void CheckYandexLanguage()
    {
        if (YandexGame.SDKEnabled)
        {
            if (YandexGame.EnvironmentData.language == "ru")
                SetRussianLanguage();
            else if (YandexGame.EnvironmentData.language == "en")
                SetEnglishLanguage();
            else if (YandexGame.EnvironmentData.language == "tr")
                SetTurkeyLanguage();
            else if (YandexGame.EnvironmentData.language == "uk")
                SetUkrainianLanguage();

            Debug.Log("СДК успел");
        }
        else
        {
            Debug.Log("СДК не успел и язык тоже");
        }
    }    

    public void LoadLanguageData()
    {
        YandexGame.savesData.IsLanguageLoaded = false;

        if (YandexGame.savesData.SavedLanguage == "ru")
            SetRussianLanguage();
        else if (YandexGame.savesData.SavedLanguage == "en")
            SetEnglishLanguage();
        else if (YandexGame.savesData.SavedLanguage == "tr")
            SetTurkeyLanguage();
        else if (YandexGame.savesData.SavedLanguage == "uk")
            SetUkrainianLanguage();

        YandexGame.savesData.IsLanguageLoaded = true;
    }
}
