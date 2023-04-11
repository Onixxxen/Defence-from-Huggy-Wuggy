using System.Collections;
using TMPro;
using UnityEngine;

public class RewardTextView : MonoBehaviour
{
    [SerializeField] private TMP_Text _rewardText;
    [SerializeField] private SettingLanguageView _settingLanguageView;

    public SettingLanguageView SettingLanguageView => _settingLanguageView;

    public void ActivateRewardText(string newText)
    {
        _rewardText.text = newText;
        _rewardText.gameObject.SetActive(true);
        StartCoroutine(ShowRewardText());
    }

    private IEnumerator ShowRewardText()
    {
        GetComponent<Animator>().SetTrigger("ShowText");

        yield return new WaitForSeconds(4);
        GetComponent<Animator>().SetTrigger("DestroyText");
        yield return new WaitUntil(AnimationIsEnd);

        _rewardText.gameObject.SetActive(false);
    }

    private bool AnimationIsEnd()
    {
        return _rewardText.transform.localScale.x == 0;
    }
}
