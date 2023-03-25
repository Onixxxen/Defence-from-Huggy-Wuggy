using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private TMP_Text _changeDayText;
    [SerializeField] private ObjectPoolView _objectPool;

    [Header("Recovery Button")]
    [SerializeField] private RecoveryHealthView _recoveryHealthView;
    [SerializeField] private RecoveryArmorView _recoveryArmorView;

    private Vector3 _defaultAngles;

    private const int _clickerMode = 1;
    private const int _towerDefenceMode = 2;

    public int CurrentMode { get; private set; }
    public float NormalDayTimeInSecond { get; private set; }
    public TMP_Text ChangeDayText => _changeDayText;
    public float TimeProgress => _timeProgress;
    public float DayTimeInSecond => _dayTimeInSecond;

    public event Action<int> TryChangeMode;

    private void Awake()
    {
        NormalDayTimeInSecond = _dayTimeInSecond;
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

        TryCallChangeMode();

        _directionalLight.color = _directionalLightGradient.Evaluate(_timeProgress);
        RenderSettings.ambientLight = _ambientLightGradient.Evaluate(_timeProgress);

        _directionalLight.transform.localEulerAngles = new Vector3(x: 360f * _timeProgress + 20, y: _defaultAngles.x, _defaultAngles.z);
    }

    private void TryCallChangeMode()
    {
        if (_timeProgress >= 0.57 && _timeProgress < 0.571f)
        {
            TryChangeMode?.Invoke(_clickerMode);
            _timeProgress = 0.572f;
        }

        if (_timeProgress >= 0.99f && _timeProgress < 0.991f)
        {
            TryChangeMode?.Invoke(_towerDefenceMode);
            _timeProgress = 0.992f;
        }
    }

    public void ActivateClickerMode(int modeIndex)
    {
        CurrentMode = modeIndex;

        for (int i = 0; i < _objectPool.Pool.Count; i++)
            _objectPool.Pool[i].gameObject.SetActive(false); ;

        _towerDefenceCanvas.gameObject.SetActive(false);      
        DOTween.To(x => _camera.orthographicSize = x, _camera.orthographicSize, 5, 2);        
        _clickerCanvas.gameObject.SetActive(true);
    }

    public void ActivateTowerDefenceMode(int day, int modeIndex)
    {
        CurrentMode = modeIndex;

        ReloadRecoveryButton();
        
        _clickerCanvas.gameObject.SetActive(false);
        DOTween.To(x => _camera.orthographicSize = x, _camera.orthographicSize, 15, 2);             
        _towerDefenceCanvas.gameObject.SetActive(true); 
        
        _changeDayText.text = $"демэ {day}";

        StartCoroutine(SetActiveDayText());
    }

    private IEnumerator SetActiveDayText()
    {
        _changeDayText.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        _changeDayText.gameObject.SetActive(false);
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
    }

    public void ChangeDayTimeInSecond(float newValue)
    {
        _dayTimeInSecond = newValue;
    }
}
