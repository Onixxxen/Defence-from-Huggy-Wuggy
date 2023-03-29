using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPresenter
{
    private Enemy _enemyModel;
    private ObjectPoolView _objectPoolView;
    private BrainView _brainView;

    public void Init(Enemy enemyModel, ObjectPoolView objectPoolView, BrainView brainView)
    {
        _enemyModel = enemyModel;
        _objectPoolView = objectPoolView;
        _brainView = brainView;
    }

    public void Enable() 
    {
        _objectPoolView.OnRequestAttack += RequestAttack;

        _enemyModel.ChangeArmorValue += OnChangeArmorValue;
        _enemyModel.ChangeHealthValue += OnChangeHealthValue;
        _enemyModel.BrainDie += OnBrainDie;
    }

    public void Disable() 
    {
        _objectPoolView.OnRequestAttack -= RequestAttack;

        _enemyModel.ChangeArmorValue -= OnChangeArmorValue;
        _enemyModel.ChangeHealthValue -= OnChangeHealthValue;
        _enemyModel.BrainDie -= OnBrainDie;
    }

    private void RequestAttack(int damage)
    {
        _enemyModel.AttackRequest(damage);
    }

    private void OnChangeArmorValue(int armorValue)
    {
        _brainView?.ChangeArmorValue(armorValue);
    }

    private void OnChangeHealthValue(int healthValue)
    {
        _brainView?.ChangeHealthValue(healthValue);
    }    

    private void OnBrainDie()
    {
        _brainView?.BrainDie();
    }
}
