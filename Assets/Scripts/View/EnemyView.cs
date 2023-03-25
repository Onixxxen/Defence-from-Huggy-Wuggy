using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using System;

public class EnemyView : MonoBehaviour
{
    private BrainView _brain;
    private NavMeshAgent _agent; 
    private TargetPoint _targetPoint;

    private int _speed;

    public event Action TryRequestAttack;
    public event Action<int, int> TryGiveCharacteristics;

    public void Init(BrainView brain)
    {
        _brain = brain;
    }

    private void Awake()
    {
        _targetPoint = FindObjectOfType<TargetPoint>();
        _agent = GetComponent<NavMeshAgent>();        
    }

    private void OnEnable()
    {
        _agent.speed = _speed;

        int endPointNumber = UnityEngine.Random.Range(0, _targetPoint.TargetPoints.Count);
        _agent.destination = _targetPoint.TargetPoints[endPointNumber].position;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out BrainView brain))
        {
            _agent.isStopped = true;
            StartCoroutine(Attack());        }
        else
        {
            StopCoroutine(Attack());
        }
    }

    public void SetCharacteristics(Enemies enemy)
    {        
        TryGiveCharacteristics?.Invoke(enemy.Damage, enemy.Speed);
        _speed = enemy.Speed;
    }

    private IEnumerator Attack()
    {
        TryRequestAttack?.Invoke();
        yield return new WaitForSeconds(1);
        StartCoroutine(Attack());
    }   
}
