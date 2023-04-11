using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardButtonView : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private List<RewardButton> _rewardButtons;
    [SerializeField] private DayChangerView _dayChangerView;
    [SerializeField] private RewardTextView _rewardTextView;

    private bool _slowDownButttonIsSpawned;
    private bool _recoveryBrainButtonIsSpawned;

    private List<RewardButton> _spawnedButton = new List<RewardButton>();

    public List<RewardButton> SpawnedButton => _spawnedButton;
    public List<RewardButton> RewardButtons => _rewardButtons;
    public RewardTextView RewardTextView => _rewardTextView;
    public bool SlowDownButttonIsSpawned => _slowDownButttonIsSpawned;
    public bool RecoveryBrainButtonIsSpawned => _recoveryBrainButtonIsSpawned;

    public void ActivateRewardButton(RewardButton rewardButton)
    {
        var button = Instantiate(rewardButton, _container.transform);

        _spawnedButton.Add(button);

        button.Init(this, _dayChangerView);

        if (rewardButton.Name == "SlowDownButton")
            _slowDownButttonIsSpawned = true;

        if (rewardButton.Name == "RecoveryBrainButton")
            _recoveryBrainButtonIsSpawned = true;
    }

    public void ChangeSlowDownButtonStatus(bool isActive)
    {
        _slowDownButttonIsSpawned = isActive;
    }

    public void ChangeRecoveryBrainButtonStatus(bool isActive)
    {
        _recoveryBrainButtonIsSpawned = isActive;
    }
}
