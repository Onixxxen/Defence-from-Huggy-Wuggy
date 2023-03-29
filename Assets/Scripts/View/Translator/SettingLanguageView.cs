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

    private TranslateView[] _translateView;
    private string _currentLanguage = "ru";

    public string CurrentLanguage => _currentLanguage;

    private void Start()
    {
        _translateView = FindObjectsOfType<TranslateView>(true);

        CheckYandexLanguage();

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
    }

    private void SetEnglishLanguage()
    {
        foreach (TranslateView translateView in _translateView)
            translateView.SetEnglishLanguage();

        ChangeCurrentLanguage(_enLanguageButton.transform);
        _currentLanguage = "en";
    }

    private void SetTurkeyLanguage()
    {
        foreach (TranslateView translateView in _translateView)
            translateView.SetTurkeyLanguage();

        ChangeCurrentLanguage(_trLanguageButton.transform);
        _currentLanguage = "tr";
    }

    private void SetUkrainianLanguage()
    {
        foreach (TranslateView translateView in _translateView)
            translateView.SetUkrainianLanguage();

        ChangeCurrentLanguage(_ukLanguageButton.transform);
        _currentLanguage = "uk";
    }

    private void ChangeCurrentLanguage(Transform button)
    {
        _currentLanguagePanel.transform.SetParent(button);
        _currentLanguagePanel.transform.position = button.position;
    }

    private void CheckYandexLanguage()
    {
        if (YandexGame.EnvironmentData.language == "ru")
            SetRussianLanguage();
        else if (YandexGame.EnvironmentData.language == "en")
            SetEnglishLanguage();
        else if (YandexGame.EnvironmentData.language == "tr")
            SetTurkeyLanguage();
        else if (YandexGame.EnvironmentData.language == "uk")
            SetUkrainianLanguage();
    }
}
