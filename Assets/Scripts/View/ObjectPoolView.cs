using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ObjectPoolView : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private RewardButtonView _rewardButtonView;

    private List<EnemyView> _pool = new List<EnemyView>();

    public List<EnemyView> Pool => _pool;

    public event Action OnRequestAttack;
    public event Action<int, int> OnGiveCharacteristics;

    public void Initialize(Enemies enemy, EnemyView prefab)
    {
        var spawned = Instantiate(prefab, _container.transform);
        spawned.gameObject.SetActive(false);

        spawned.TryRequestAttack += TryAttack;
        spawned.TryGiveCharacteristics += GiveCharacteristics;

        spawned.SetCharacteristics(enemy);
        spawned.Init(_rewardButtonView);       

        _pool.Add(spawned);
    }

    public bool TryGetObject(out EnemyView result)
    {
        result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return result != null;
    }

    public void TryAttack()
    {
        OnRequestAttack?.Invoke();
    }

    private void GiveCharacteristics(int damage, int speed)
    {
        OnGiveCharacteristics?.Invoke(damage, speed);
    }
}
