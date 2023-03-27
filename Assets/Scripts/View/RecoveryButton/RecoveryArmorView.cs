using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RecoveryArmorView : MonoBehaviour
{
    [SerializeField] private Slider _cooldownSlider;
    [SerializeField] private int _recoveryTime;

    [Header("RewardButton")]
    [SerializeField] private RewardButtonView _rewardButtonView;

    public Slider CooldownSlider => _cooldownSlider;

    public event Action TryRecoveryArmor;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(RecoveryArmor);
    }

    private void RecoveryArmor()
    {
        TryRecoveryArmor?.Invoke();
    }

    public void BlockRecoveryButton()
    {
        StartCoroutine(RecoveryArmorCooldown());
        TryActiveBonusButton();
    }

    private IEnumerator RecoveryArmorCooldown()
    {
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

    private void TryActiveBonusButton()
    {
        int randomButton = UnityEngine.Random.Range(1, 5);

        Debug.Log(randomButton);

        if (randomButton == 1)
            for (int i = 0; i < _rewardButtonView.RewardButtons.Count; i++)
                if (_rewardButtonView.RewardButtons[i].Name == "SkipArmorCooldownButton")
                    _rewardButtonView.ActivateRewardButton(_rewardButtonView.RewardButtons[i]);
    }
}
