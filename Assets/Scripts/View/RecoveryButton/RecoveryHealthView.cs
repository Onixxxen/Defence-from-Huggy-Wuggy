using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RecoveryHealthView : MonoBehaviour
{
    [SerializeField] private Slider _cooldownSlider;
    [SerializeField] private BrainEffectView _brainEffectView;
    [SerializeField] private float _recoveryTime;

    [Header("RewardButton")]
    [SerializeField] private RewardButtonView _rewardButtonView;

    public Slider CooldownSlider => _cooldownSlider;

    public event Action TryRecoveryHealth;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(RecoveryHealth);
    }

    private void RecoveryHealth()
    {
        TryRecoveryHealth?.Invoke();
        _brainEffectView.StartRecoveryHealthEffect();
    }

    public void BlockRecoveryButton()
    {
        StartCoroutine(RecoveryHealthCooldown());
        //TryActiveBonusButton();
    }

    private IEnumerator RecoveryHealthCooldown()
    {
        if (_cooldownSlider.value == _cooldownSlider.minValue)
            _cooldownSlider.value = _cooldownSlider.maxValue;

        _cooldownSlider.gameObject.SetActive(true);
        GetComponent<Button>().interactable = false;

        _cooldownSlider.DOValue(_cooldownSlider.minValue, _recoveryTime);

        yield return new WaitUntil(IsCooldownPassed);

        _cooldownSlider.gameObject.SetActive(false);
        GetComponent<Button>().interactable = true;
    }

    private bool IsCooldownPassed()
    {
        return _cooldownSlider.value == _cooldownSlider.minValue;
    }

    public void TryPauseCooldown()
    {
        if (_cooldownSlider.gameObject.activeInHierarchy)
            _cooldownSlider.DOPause();
    }

    public void TryContinueCooldown()
    {
        if (_cooldownSlider.gameObject.activeInHierarchy)
            StartCoroutine(RecoveryHealthCooldown());
    }

    private void TryActiveBonusButton()
    {
        int randomButton = UnityEngine.Random.Range(1, 10);

        if (randomButton == 1)
            for (int i = 0; i < _rewardButtonView.RewardButtons.Count; i++)
                if (_rewardButtonView.RewardButtons[i].Name == "SkipHealthCooldownButton")
                    _rewardButtonView.ActivateRewardButton(_rewardButtonView.RewardButtons[i]);
    }
}
