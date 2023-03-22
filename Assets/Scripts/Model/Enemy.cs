using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    private int _damage;
    private int _speed;

    private Health _health;
    private Armor _armor;

    public int Damage => _damage;

    public event Action<int> ChangeArmorValue;
    public event Action<int> ChangeHealthValue;
    public event Action BrainDie;

    public Enemy(Health health, Armor armor)
    {
        _health = health;
        _armor = armor;
    }

    public void GetCharacteristics(int damage, int speed)
    {        
        _damage = damage;
        _speed = speed;
    }

    public void AttackRequest()
    {
        if (_armor.Count > 0 && _health.Count > 0)
        {            
            _armor.TakeDamage(_damage);
            ChangeArmorValue?.Invoke(_armor.Count);
        }
        else if (_armor.Count <= 0 && _health.Count > 0)
        {
            _health.TakeDamage(_damage);
            ChangeHealthValue?.Invoke(_health.Count);
        }
        else if (_armor.Count <= 0 && _health.Count <= 0)
        {
            BrainDie?.Invoke();
        }
    }   
    
    public void ChangeEnemyDamage(int newValue)
    {
        _damage = newValue;
    }
}
