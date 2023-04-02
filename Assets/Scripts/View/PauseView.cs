using UnityEngine;

public class PauseView : MonoBehaviour
{
    private ObjectPoolView _objectPool;
    private DayChangerView _dayChangerView;
    private RecoveryArmorView _recoveryArmorView;
    private RecoveryHealthView _recoveryHealthView;
    private RewardButtonView _rewardButtonView;

    public void Init(ObjectPoolView objectPoolView, DayChangerView dayChangerView, RecoveryArmorView recoveryArmorView, RecoveryHealthView recoveryHealthView, RewardButtonView rewardButtonView)
    {
        _objectPool = objectPoolView;
        _dayChangerView = dayChangerView;
        _recoveryArmorView = recoveryArmorView;
        _recoveryHealthView = recoveryHealthView;
        _rewardButtonView = rewardButtonView;
    }

    public void Pause(bool isActive)
    {
        if (isActive)
        {
            _dayChangerView.ChangeDayTimeInSecond(1000000);
            _recoveryArmorView.TryPauseCooldown();
            _recoveryHealthView.TryPauseCooldown();

            for (int i = 0; i < _objectPool.Pool.Count; i++)
                _objectPool.Pool[i].ChangeSpeed(0);

            if (_rewardButtonView.SpawnedButton.Count > 0)
                for (int i = 0; i < _rewardButtonView.SpawnedButton.Count; i++)
                    _rewardButtonView.SpawnedButton[i].TryPauseLifetime();
        }
        else
        {
            _dayChangerView.ChangeDayTimeInSecond(_dayChangerView.PreviousDayTimeInSecond);
            _recoveryArmorView.TryContinueCooldown();
            _recoveryHealthView.TryContinueCooldown();

            for (int i = 0; i < _objectPool.Pool.Count; i++)
                _objectPool.Pool[i].ChangeSpeed(_objectPool.Pool[i].NormalSpeed);

            if (_rewardButtonView.SpawnedButton.Count > 0)
                for (int i = 0; i < _rewardButtonView.SpawnedButton.Count; i++)
                    _rewardButtonView.SpawnedButton[i].TryContinueLifetime();
        }
    }
}
