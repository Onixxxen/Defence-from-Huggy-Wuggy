using System;
using UnityEngine;
using YG;
using YG.Example;

public class Enemy
{
    private int _damage;
    private int _speed;

    private Health _health;
    private Armor _armor;

    private SaverData _saverData;

    public int Damage => _damage;

    public event Action<int> ChangeArmorValue;
    public event Action<int> ChangeHealthValue;
    public event Action ResetDamageValue;
    public event Action BrainDie;

    public Enemy(Health health, Armor armor, SaverData saverData)
    {
        _health = health;
        _armor = armor;
        _saverData = saverData;
    }

    public void AttackRequest(int damage)
    {        
        _damage = damage;

        if (_armor.Count > 0 && _health.Count > 0)
        {
            _armor.TakeDamage(damage);
            ChangeArmorValue?.Invoke(_armor.Count);
        }
        else if (_armor.Count <= 0 && _health.Count > 0)
        {
            _health.TakeDamage(damage);
            ChangeHealthValue?.Invoke(_health.Count);
        }

        if (_armor.Count <= 0 && _health.Count <= 0)
        {
            BrainDie?.Invoke();
        }
    }   
    
    public void Reset()
    {
        ResetDamageValue?.Invoke();
    }
}
