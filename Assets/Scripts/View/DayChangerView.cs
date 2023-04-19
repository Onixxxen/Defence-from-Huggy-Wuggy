using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG; // Яндекс SDK
using YG.Example;

public class DayChangerView : MonoBehaviour
{
    [Header("Changing Time")]
    [SerializeField] private Gradient _directionalLightGradient;
    [SerializeField] private Gradient _ambientLightGradient;

    [SerializeField, Range(0, 3600)] private float _dayTimeInSecond;
    [SerializeField, Range(0, 1f)] private float _timeProgress;

    [SerializeField] private Light _directionalLight;

    [Header("Changing Mode")]
    [SerializeField] private Camera _camera;
    [SerializeField] private Canvas _clickerCanvas;
    [SerializeField] private Canvas _towerDefenceCanvas;
    [SerializeField] private Scrollbar _timeProgressBar;
    [SerializeField] private ChangeDayTextView _changeDayTextView;
    [SerializeField] private CurrentDayView _currentDayView;
    [SerializeField] private ObjectPoolView _objectPool;
    [SerializeField] private SpawnerView _spawnerView;
    [SerializeField] private StartScreenView _startScreenView;

    [Header("Recovery Button")]
    [SerializeField] private RecoveryHealthView _recoveryHealthView;
    [SerializeField] private RecoveryArmorView _recoveryArmorView;

    [Header("RewardButton")]
    [SerializeField] private RewardButtonView _rewardButtonView;

    [Header("Sound")]
    [SerializeField] private AudioSource _clickerMusic;
    [SerializeField] private AudioSource _defenceMusic;

    [Header("Saving")]
    [SerializeField] private SaverData _saverData;

    private Vector3 _defaultAngles;

    private const int _clickerMode = 1;
    private const int _towerDefenceMode = 2;

    private float _randomTime;
    private float _randomButton;
    private bool _isSpawned;

    public int CurrentMode { get; private set; }
    public float PreviousDayTimeInSecond { get; private set; }
    public float TimeProgress => _timeProgress;
    public float DayTimeInSecond => _dayTimeInSecond;

    public event Action<int> TryChangeMode;

    private void Awake()
    {
        PreviousDayTimeInSecond = _dayTimeInSecond;
    }

    private void Start()
    {
        _defaultAngles = _directionalLight.transform.localEulerAngles;
    }

    private void Update()
    {
        _timeProgress += Time.deltaTime / _dayTimeInSecond;
        _timeProgressBar.value = _timeProgress;

        if (_timeProgress >= 1f)
            _timeProgress = 0f;

        if (_isSpawned == false)
            if (CurrentMode == _clickerMode)
                TryShowClickerRewardButton(); // не смотри этот метод

        if (_isSpawned == false)
            if (CurrentMode == _towerDefenceMode)
                TryShowTowerDefenceRewardButton();

        TryCallChangeMode();

        _directionalLight.color = _directionalLightGradient.Evaluate(_timeProgress);
        RenderSettings.ambientLight = _ambientLightGradient.Evaluate(_timeProgress);

        _directionalLight.transform.localEulerAngles = new Vector3(x: 360f * _timeProgress + 20, y: _defaultAngles.x, _defaultAngles.z);
    }


    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            _saverData.SaveTime(_timeProgress);
    }

    private void TryCallChangeMode()
    {
        if (_timeProgress >= 0.57 && _timeProgress < 0.571f)
        {
            TryChangeMode?.Invoke(_clickerMode);
            _timeProgress = 0.572f;
        }

        if (_timeProgress >= 0f && _timeProgress < 0.001f)
        {
            TryChangeMode?.Invoke(_towerDefenceMode);
            _timeProgress = 0.002f;
        }
    }

    public void ActivateClickerMode(int day, int modeIndex)
    {
        CurrentMode = modeIndex;

        _defenceMusic.Stop();
        _clickerMusic.Play();

        BackDayTimeInSecond();
        RandomizeInClicker();

        _isSpawned = false;

        _startScreenView.ChangeCameraNormalSie(5);

        for (int i = 0; i < _objectPool.Pool.Count; i++)
            _objectPool.Pool[i].gameObject.SetActive(false);

        _currentDayView.UpdateDayText(day);

        _towerDefenceCanvas.gameObject.SetActive(false);
        DOTween.To(x => _camera.orthographicSize = x, _camera.orthographicSize, 5, 2);
        _clickerCanvas.gameObject.SetActive(true);
    }

    public void ActivateTowerDefenceMode(int day, int modeIndex)
    {
        CurrentMode = modeIndex;

        _clickerMusic.Stop();
        _defenceMusic.Play();

        if (_spawnerView.SecondsBetweenSpawn >= 9000)
            _spawnerView.ChangeSecondBetweenSpawn(2.3f);

        _spawnerView.ChangeSecondBetweenSpawn(_spawnerView.SecondsBetweenSpawn - 0.1f);


        if (YandexGame.savesData.IsTowerDefenceLoaded)
            _saverData.SaveSecondBetweebSpawn(_spawnerView.SecondsBetweenSpawn);

        ReloadRecoveryButton();
        BackDayTimeInSecond();
        RandomizeInTowerDefence();

        _isSpawned = false;

        _clickerCanvas.gameObject.SetActive(false);

        if (YandexGame.EnvironmentData.isMobile)
            _startScreenView.ChangeCameraNormalSie(20);
        else
            _startScreenView.ChangeCameraNormalSie(15);

        if (YandexGame.EnvironmentData.isMobile) // Проверка на устройство. Если телефон, то камера отдаляется на большее расстояние
            DOTween.To(x => _camera.orthographicSize = x, _camera.orthographicSize, 20, 2);
        else
            DOTween.To(x => _camera.orthographicSize = x, _camera.orthographicSize, 15, 2);

        _towerDefenceCanvas.gameObject.SetActive(true);

        _changeDayTextView.ChangeDayText(day);
        _currentDayView.UpdateDayText(day);
    }

    private void ReloadRecoveryButton()
    {
        _recoveryHealthView.CooldownSlider.gameObject.SetActive(false);
        _recoveryHealthView.GetComponent<Button>().interactable = true;

        _recoveryArmorView.CooldownSlider.gameObject.SetActive(false);
        _recoveryArmorView.GetComponent<Button>().interactable = true;
    }

    public void ChangeTime(float newTime)
    {
        _timeProgress = newTime;
        _saverData.SaveTime(_timeProgress);
    }

    public void ChangeDayTimeInSecond(float newValue)
    {
        _dayTimeInSecond = newValue;
    }

    public void BackDayTimeInSecond()
    {
        _dayTimeInSecond = PreviousDayTimeInSecond;
    }

    public void UpdatePreviousDayTimeInSecond()
    {
        PreviousDayTimeInSecond = _dayTimeInSecond;
    }

    private void RandomizeInClicker()
    {
        int randomButton = UnityEngine.Random.Range(1, 6);
        float randomTime = UnityEngine.Random.Range(0.6f, 0.8f);

        _randomButton = randomButton;
        _randomTime = randomTime;
    }

    private void RandomizeInTowerDefence()
    {
        int randomBonusView = UnityEngine.Random.Range(1, 6);

        if (randomBonusView == 1)
        {
            float randomTime = UnityEngine.Random.Range(0.1f, 0.3f);
            _randomTime = randomTime;
        }
    }

    private void TryShowClickerRewardButton() // ну типо...
    {
        if (_timeProgress >= _randomTime && _timeProgress < _randomTime + 0.001f)
        {
            if (_randomButton == 1)
            {
                for (int i = 0; i < _rewardButtonView.RewardButtons.Count; i++)
                {
                    if (_rewardButtonView.RewardButtons[i].Name == "NeuronBonusButton")
                    {
                        _rewardButtonView.ActivateRewardButton(_rewardButtonView.RewardButtons[i]);
                        _isSpawned = true;
                    }
                }
            }
            else if (_randomButton == 2)
            {
                for (int i = 0; i < _rewardButtonView.RewardButtons.Count; i++)
                {
                    if (_rewardButtonView.RewardButtons[i].Name == "SlowTimeButton")
                    {
                        _rewardButtonView.ActivateRewardButton(_rewardButtonView.RewardButtons[i]);
                        _isSpawned = true;
                    }
                }
            }
        }
    }

    private void TryShowTowerDefenceRewardButton()
    {
        if (_timeProgress >= _randomTime && _timeProgress < _randomTime + 0.001f)
        {
            for (int i = 0; i < _rewardButtonView.RewardButtons.Count; i++)
            {
                if (_rewardButtonView.RewardButtons[i].Name == "SpeedTimeButton")
                {
                    _rewardButtonView.ActivateRewardButton(_rewardButtonView.RewardButtons[i]);
                    _isSpawned = true;
                }
            }
        }
    }

    public bool IsClickerMode()
    {
        return _timeProgress >= 0.57 && _timeProgress < 0.999f;
    }

    public bool IsDefenceMode()
    {
        return _timeProgress >= 0 && _timeProgress < 0.57f;
    }

    public void LoadTimeData()
    {
        _timeProgress = YandexGame.savesData.SavedTime;

        if (IsClickerMode())
        {
            YandexGame.savesData.IsClickerLoaded = false;
            TryChangeMode?.Invoke(_clickerMode);
        }

        if (IsDefenceMode())
        {
            if (YandexGame.EnvironmentData.isMobile)
                _startScreenView.ChangeCameraNormalSie(20);
            else
                _startScreenView.ChangeCameraNormalSie(15);

            YandexGame.savesData.IsTowerDefenceLoaded = false;
            TryChangeMode?.Invoke(_towerDefenceMode);
        }

        YandexGame.savesData.IsTowerDefenceLoaded = true;
        YandexGame.savesData.IsClickerLoaded = true;
    }
}
