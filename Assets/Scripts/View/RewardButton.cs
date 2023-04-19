using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;  

public class RewardButton : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _reward;
    [SerializeField] private TMP_Text _lifetimeText;
    [SerializeField] private Slider _progressSlider;
    [SerializeField] private Slider _lifetime;

    [Header("Reward Text")]
    [SerializeField] private string RuText;
    [SerializeField] private string EnText;
    [SerializeField] private string TrText;
    [SerializeField] private string UkText;

    private RewardButtonView _view;
    private DayChangerView _dayChangerView;
    private SpawnerView _spawnerView;
    private SoundSettingsView _soundSettingsView;

    public string Name => _name;
    public int Reward => _reward;
    public Slider ProgressSlider => _progressSlider;

    public void Init(RewardButtonView view, DayChangerView dayChangerView, SpawnerView spawnerView, SoundSettingsView soundSettingsView)
    {
        _view = view;
        _dayChangerView = dayChangerView;
        _spawnerView = spawnerView;
        _soundSettingsView = soundSettingsView;
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => ActivateReward(_reward));
    }

    private void Start()
    {
        float normalScale = transform.localScale.x;
        transform.localScale = new Vector3(0, 0, 0);
        transform.DOScale(normalScale, 0.5f);
        _progressSlider.value = _progressSlider.minValue;
        StartCoroutine(ButtonLifetime());
    }

    private void ActivateReward(int rewardID)
    {
        _dayChangerView.UpdatePreviousDayTimeInSecond();
        _spawnerView.UpdatePreviousSecondBetweenSpawn();
        _soundSettingsView.UpdateSoundVolume();
        YandexGame.RewVideoShow(rewardID);
    }

    public void ActivateSlider(int duration)
    {
        ActivateRewardText();
        StartCoroutine(TryDestroyButton(duration));
    }

    private IEnumerator TryDestroyButton(int duration)
    {
        _lifetimeText.gameObject.SetActive(false);
        _lifetime.DOPause();

        GetComponent<Button>().interactable = false;
        _progressSlider.DOValue(_progressSlider.maxValue, duration);

        yield return new WaitUntil(SliderIsFull);

        transform.DOScale(0, 0.5f);

        yield return new WaitUntil(ButtonIsScaled);        

        for (int i = 0; i < _view.SpawnedButton.Count; i++)
            if (_view.SpawnedButton[i].Name == _name)
                _view.SpawnedButton.Remove(this);

        Destroy(gameObject);
    }

    private IEnumerator ButtonLifetime()
    {
        if (_lifetime.value == _lifetime.minValue)
            _lifetime.value = _lifetime.maxValue;

        _lifetime.DOValue(_lifetime.minValue, _lifetime.maxValue * 1.5f);        

        yield return new WaitUntil(lifetimeIsEnd);

        transform.DOScale(0, 0.5f);

        yield return new WaitUntil(() => transform.localScale.x == 0);

        for (int i = 0; i < _view.SpawnedButton.Count; i++)
            if (_view.SpawnedButton[i].Name == _name)
                _view.SpawnedButton.Remove(this);

        Destroy(gameObject);
    }

    private void ActivateRewardText()
    {
        if (_view.RewardTextView.SettingLanguageView.CurrentLanguage == "ru")
            _view.RewardTextView.ActivateRewardText(RuText);
        else if (_view.RewardTextView.SettingLanguageView.CurrentLanguage == "en")
            _view.RewardTextView.ActivateRewardText(EnText);
        else if (_view.RewardTextView.SettingLanguageView.CurrentLanguage == "tr")
            _view.RewardTextView.ActivateRewardText(TrText);
        else if (_view.RewardTextView.SettingLanguageView.CurrentLanguage == "uk")
            _view.RewardTextView.ActivateRewardText(UkText);
    }

    public void TryPauseLifetime()
    {
        if (_lifetime.gameObject.activeInHierarchy)
            _lifetime.DOPause();
    }

    public void TryContinueLifetime()
    {
        if (_lifetime.gameObject.activeInHierarchy)
            StartCoroutine(ButtonLifetime());
    }

    private bool SliderIsFull()
    {
        return _progressSlider.value == _progressSlider.maxValue;
    }

    private bool lifetimeIsEnd()
    {
        int lifeTime = (int)_lifetime.value;
        _lifetimeText.text = lifeTime.ToString();
        return _lifetime.value == _lifetime.minValue;
    }

    private bool ButtonIsScaled()
    {
        return transform.localScale.x == 0;
    }
}
