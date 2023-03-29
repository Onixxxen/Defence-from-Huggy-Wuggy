using UnityEngine;
using YG;

public class ResetSavesView : MonoBehaviour
{
    [SerializeField] private DayChangerView _dayChangerView;

    public void ResetSaves()
    {
        YandexGame.ResetSaveProgress();
        _dayChangerView.ChangeTime(0.57f);
    }
}
