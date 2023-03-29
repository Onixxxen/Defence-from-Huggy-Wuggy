using TMPro;
using UnityEngine;

public class ChangeDayTextView : MonoBehaviour
{
    [SerializeField] private TMP_Text _changeDayText;
    [SerializeField] private TMP_Text _dayCount;
    [SerializeField] private TMP_Text _dayText;

    public void ChangeDayText(int dayCount)
    {
        _dayCount.text = $"{dayCount}";
        _changeDayText.text = $"{_dayText.text} {_dayCount.text}";
    }
}
