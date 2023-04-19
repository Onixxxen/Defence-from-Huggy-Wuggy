using System.Collections;
using TMPro;
using UnityEngine;

public class RewardTextView : MonoBehaviour
{
    [SerializeField] private TMP_Text _rewardText;
    [SerializeField] private SettingLanguageView _settingLanguageView;
    [SerializeField] private GameObject _effect;

    public SettingLanguageView SettingLanguageView => _settingLanguageView;

    public void ActivateRewardText(string newText)
    {
        _rewardText.text = newText;
        _rewardText.gameObject.SetActive(true);
        Instantiate(_effect, transform.position, Quaternion.identity);
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
