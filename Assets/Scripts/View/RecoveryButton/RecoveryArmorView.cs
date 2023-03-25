using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RecoveryArmorView : MonoBehaviour
{
    [SerializeField] private Slider _cooldownSlider;
    [SerializeField] private int _recoveryTime;

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
    }

    private IEnumerator RecoveryArmorCooldown()
    {
        _cooldownSlider.gameObject.SetActive(true);
        GetComponent<Button>().interactable = false;
        _cooldownSlider.value = _cooldownSlider.maxValue;

        _cooldownSlider.DOValue(_cooldownSlider.minValue, _recoveryTime);

        yield return new WaitForSeconds(_recoveryTime);

        _cooldownSlider.gameObject.SetActive(false);
        GetComponent<Button>().interactable = true;
    }
}
