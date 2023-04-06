using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SupportingTextView : MonoBehaviour
{
    [SerializeField] private List<ClickerSupportingText> _clickerSupportingTexts;
    [SerializeField] private List<DefenceSupportingText> _defenceSupportingTexts;
    [SerializeField] private TMP_Text _supportingText;
    [SerializeField] private SettingLanguageView _settingLanguage;

    private const int _clickerMode = 1;
    private const int _towerDefenceMode = 2;

    public void ShowSupportingText(int mode)
    {
        if (_supportingText.gameObject.activeInHierarchy)
            _supportingText.gameObject.SetActive(false);

        if (mode == _clickerMode)
        {
            int textNumber = Random.Range(0, _clickerSupportingTexts.Count);

            if (_settingLanguage.CurrentLanguage == "ru")
                _supportingText.text = _clickerSupportingTexts[textNumber].RuText;
            else if (_settingLanguage.CurrentLanguage == "en")
                _supportingText.text = _clickerSupportingTexts[textNumber].EnText;
            else if (_settingLanguage.CurrentLanguage == "tr")
                _supportingText.text = _clickerSupportingTexts[textNumber].TrText;
            else if (_settingLanguage.CurrentLanguage == "uk")
                _supportingText.text = _clickerSupportingTexts[textNumber].UkText;
        }
        else if (mode == _towerDefenceMode)
        {
            int textNumber = Random.Range(0, _defenceSupportingTexts.Count);

            if (_settingLanguage.CurrentLanguage == "ru")
                _supportingText.text = _defenceSupportingTexts[textNumber].RuText;
            else if (_settingLanguage.CurrentLanguage == "en")
                _supportingText.text = _defenceSupportingTexts[textNumber].EnText;
            else if (_settingLanguage.CurrentLanguage == "tr")
                _supportingText.text = _defenceSupportingTexts[textNumber].TrText;
            else if (_settingLanguage.CurrentLanguage == "uk")
                _supportingText.text = _defenceSupportingTexts[textNumber].UkText;
        }

        _supportingText.gameObject.SetActive(true);        
        StartCoroutine(SupportingTextAnimation());
    }

    private IEnumerator SupportingTextAnimation()
    {
        _supportingText.GetComponent<Animator>().SetTrigger("TextIsActive");

        yield return new WaitUntil(AnimationIsEnd);

        _supportingText.gameObject.SetActive(false);
    }

    private bool AnimationIsEnd()
    {
        return _supportingText.transform.localScale.x == 0;
    }
}
