using System.Collections;
using TMPro;
using UnityEngine;

public class ChangeDayTextView : MonoBehaviour
{
    [SerializeField] private TMP_Text _changeDayText;
    [SerializeField] private TMP_Text _dayCount;
    [SerializeField] private TMP_Text _dayText;
    [SerializeField] private TMP_Text _supportingText;

    private void Start()
    {
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);
    }

    public void ChangeDayText(int dayCount)
    {
        _supportingText.gameObject.SetActive(false);

        _dayCount.text = $"{dayCount}";
        _changeDayText.text = $"{_dayText.text} {_dayCount.text}";

        StartCoroutine(SetActiveDayText());
    }

    private IEnumerator SetActiveDayText()
    {
        GetComponent<Animator>().SetTrigger("ShowText");
        yield return new WaitForSeconds(4);
        GetComponent<Animator>().SetTrigger("DestroyText");
    }
}
