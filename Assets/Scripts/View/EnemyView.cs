using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class EnemyView : MonoBehaviour
{
    private BrainView _brain;

    public event Action TryRequestAttack;
    public event Action<int, int> TryGiveCharacteristics;

    private void OnMouseDown()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Attack();
    }

    public void Init(BrainView brain)
    {
        _brain = brain;
    }

    public void SetCharacteristics(Enemies enemy)
    {        
        TryGiveCharacteristics?.Invoke(enemy.Damage, enemy.Speed);
    }

    private void Attack()
    {
        TryRequestAttack?.Invoke();
    }   
}
