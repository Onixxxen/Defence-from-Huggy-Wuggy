using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using System;

public class EnemyView : MonoBehaviour
{
    private NavMeshAgent _agent; 
    private TargetPoint _targetPoint;

    private RewardButtonView _rewardButtonView;

    private int _speed;
    private int _normalSpeed;

    public int Speed => _speed;
    public int NormalSpeed => _normalSpeed;

    public event Action TryRequestAttack;
    public event Action<int, int> TryGiveCharacteristics;

    public void Init(RewardButtonView rewardButtonView)
    {
        _rewardButtonView = rewardButtonView;
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
        TryGiveCharacteristics?.Invoke(enemy.Damage, enemy.Speed);
        _speed = enemy.Speed;
        _normalSpeed = _speed;
    }

    public void ChangeSpeed(int newSpeed)
    {
        _agent.speed = newSpeed;
    }

    private IEnumerator Attack()
    {
        TryRequestAttack?.Invoke();
        yield return new WaitForSeconds(1);
        StartCoroutine(Attack());
    }   

    private void TryActiveBonusButton()
    {
        if (!_rewardButtonView.SlowDownButttonIsSpawned)
        {
            int randomButton = UnityEngine.Random.Range(1, 3);

            Debug.Log(randomButton);

            if (randomButton == 1)
                for (int i = 0; i < _rewardButtonView.RewardButtons.Count; i++)
                    if (_rewardButtonView.RewardButtons[i].Name == "SlowDownButton")
                        _rewardButtonView.ActivateRewardButton(_rewardButtonView.RewardButtons[i]);
        }        
    }
}
