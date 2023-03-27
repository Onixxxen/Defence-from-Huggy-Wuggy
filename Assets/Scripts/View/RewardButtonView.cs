using System.Collections.Generic;
using UnityEngine;

public class RewardButtonView : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private List<RewardButton> _rewardButtons;
    [SerializeField] private DayChangerView _dayChangerView;

    private bool _slowDownButttonIsSpawned;

    private List<RewardButton> _spawnedButton = new List<RewardButton>();

    public List<RewardButton> SpawnedButton => _spawnedButton;
    public List<RewardButton> RewardButtons => _rewardButtons;
    public bool SlowDownButttonIsSpawned => _slowDownButttonIsSpawned;

    public void ActivateRewardButton(RewardButton rewardButton)
    {
        var button = Instantiate(rewardButton, _container.transform);

        _spawnedButton.Add(button);

        button.Init(this);

        if (rewardButton.Name == "SlowDownButton")
            _slowDownButttonIsSpawned = true;
    }

    public void ChangeSlowDownButtonStatus(bool isActive)
    {
        _slowDownButttonIsSpawned = isActive;
    }
}
