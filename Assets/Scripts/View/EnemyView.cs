using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyView : MonoBehaviour
{
    private float _damage;
    private float _speed;

    private BrainView _brain;

    private void OnMouseDown()
    {
        gameObject.SetActive(false);
    }

    public void Init(BrainView brain)
    {
        _brain = brain;
    }

    public void SetCharacteristics(Enemies enemy)
    {
        _damage = enemy.Damage;
        _speed = enemy.Speed;
    }
}
