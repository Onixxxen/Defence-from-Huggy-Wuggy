using UnityEngine;

public class PauseView : MonoBehaviour
{
    private ObjectPoolView _objectPool;
    private SpawnerView _spawnerView;
    private DayChangerView _dayChangerView;
    private RecoveryArmorView _recoveryArmorView;
    private RecoveryHealthView _recoveryHealthView;
    private RewardButtonView _rewardButtonView;
    private SoundSettingsView _soundSettingsView;

    private bool isPause;

    public bool IsPause => isPause;

    public SoundSettingsView SoundSettingsView => _soundSettingsView;

    public void Init(ObjectPoolView objectPoolView, SpawnerView spawnerView, DayChangerView dayChangerView, RecoveryArmorView recoveryArmorView, RecoveryHealthView recoveryHealthView, RewardButtonView rewardButtonView, SoundSettingsView soundSettingsView)
    {
        _objectPool = objectPoolView;
        _spawnerView = spawnerView;
        _dayChangerView = dayChangerView;
        _recoveryArmorView = recoveryArmorView;
        _recoveryHealthView = recoveryHealthView;
        _rewardButtonView = rewardButtonView;
        _soundSettingsView = soundSettingsView;
    }

    public void Pause(bool isActive)
    {
        if (isActive)
        {
            _dayChangerView.ChangeDayTimeInSecond(1000000);
            _spawnerView.ChangeSecondBetweenSpawn(1000000);
            _soundSettingsView.PauseSoundVolume();
            _recoveryArmorView.TryPauseCooldown();
            _recoveryHealthView.TryPauseCooldown();

            for (int i = 0; i < _objectPool.Pool.Count; i++)
                _objectPool.Pool[i].ChangeSpeed(0);

            for (int i = 0; i < _objectPool.Pool.Count; i++)
                StopCoroutine(_objectPool.Pool[i].Attack());

            if (_rewardButtonView.SpawnedButton.Count > 0)
                for (int i = 0; i < _rewardButtonView.SpawnedButton.Count; i++)
                    _rewardButtonView.SpawnedButton[i].TryPauseLifetime();

            isPause = true;
        }
        else
        {
            _dayChangerView.ChangeDayTimeInSecond(_dayChangerView.PreviousDayTimeInSecond);
            _spawnerView.BackSecondBetweenSpawn();
            _soundSettingsView.BackSoundVolume();
            _recoveryArmorView.TryContinueCooldown();
            _recoveryHealthView.TryContinueCooldown();

            for (int i = 0; i < _objectPool.Pool.Count; i++)
                _objectPool.Pool[i].ChangeSpeed(_objectPool.Pool[i].NormalSpeed);

            if (_rewardButtonView.SpawnedButton.Count > 0)
                for (int i = 0; i < _rewardButtonView.SpawnedButton.Count; i++)
                    _rewardButtonView.SpawnedButton[i].TryContinueLifetime();

            isPause = false;
        }
    }
}
