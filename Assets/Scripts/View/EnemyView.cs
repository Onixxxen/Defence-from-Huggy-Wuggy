using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System;
using YG;
using YG.Example;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _speed;

    private NavMeshAgent _agent; 
    private TargetPoint _targetPoint;

    private RewardButtonView _rewardButtonView;
    private SaverData _saverData;

    private int _normalSpeed => _speed;

    public int Damage => _damage;
    public int Speed => _speed;
    public int NormalSpeed => _normalSpeed;

    public event Action<int> TryRequestAttack;

    public void Init(RewardButtonView rewardButtonView, SaverData saverData)
    {
        _rewardButtonView = rewardButtonView;
        _saverData = saverData;
    }

    private void Awake()
    {
        _targetPoint = FindObjectOfType<TargetPoint>();
        _agent = GetComponent<NavMeshAgent>();        
    }

    private void OnEnable()
    {       
        int endPointNumber = UnityEngine.Random.Range(0, _targetPoint.TargetPoints.Count);
        _agent.destination = _targetPoint.TargetPoints[endPointNumber].position;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out BrainView brain))
        {
            _agent.isStopped = true;
            StartCoroutine(Attack());
            TryActiveBonusButton();
        }
        else
        {
            StopCoroutine(Attack());
        }
    }

    public void SetCharacteristics(Enemies enemy)
    {
        _damage = enemy.Damage;
        _speed = enemy.Speed;
    }

    public void ChangeSpeed(int newSpeed)
    {
        _agent.speed = newSpeed;
    }

    public void ChangeDamage(int newDamage)
    {
        _damage *= newDamage;

        if (YandexGame.savesData.TowerDefenceLoaded)
            _saverData.SaveEnemyDamage(_damage);
    }

    public void ResetDamage()
    {
        _damage = 1;

        if (YandexGame.savesData.TowerDefenceLoaded)
            _saverData.SaveEnemyDamage(_damage);
    }

    private IEnumerator Attack()
    {
        TryRequestAttack?.Invoke(_damage);
        yield return new WaitForSeconds(1);
        StartCoroutine(Attack());
    }   

    private void TryActiveBonusButton()
    {
        if (!_rewardButtonView.SlowDownButttonIsSpawned)
        {
            int randomButton = UnityEngine.Random.Range(1, 20); // желательно проверить

            if (randomButton == 1)
                for (int i = 0; i < _rewardButtonView.RewardButtons.Count; i++)
                    if (_rewardButtonView.RewardButtons[i].Name == "SlowDownButton")
                        _rewardButtonView.ActivateRewardButton(_rewardButtonView.RewardButtons[i]);
        }  

        if (!_rewardButtonView.RecoveryBrainButtonIsSpawned)
        {
            int randomButton = UnityEngine.Random.Range(1, 20);

            if (randomButton == 1)
                for (int i = 0; i < _rewardButtonView.RewardButtons.Count; i++)
                    if (_rewardButtonView.RewardButtons[i].Name == "RecoveryBrainButton")
                        _rewardButtonView.ActivateRewardButton(_rewardButtonView.RewardButtons[i]);
        }        
    }

    public void LoadEnemyData()
    {
        _damage = YandexGame.savesData.SavedEnemyDamage;
    }
}
