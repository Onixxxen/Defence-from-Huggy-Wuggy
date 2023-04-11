using TMPro;
using UnityEngine;
using YG;

public class CurrentDayView : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentDayText;
    [SerializeField] private TMP_Text _dayCount;
    [SerializeField] private TMP_Text _dayText;

    public void UpdateDayText(int dayCount)
    {
        _dayCount.text = $"{dayCount}";
        _currentDayText.text = $"{_dayText.text} {_dayCount.text}";
    }
}
