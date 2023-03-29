using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using YG.Example;

public class ObjectPoolView : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private RewardButtonView _rewardButtonView;
    [SerializeField] private SaverData _saverData;

    private List<EnemyView> _pool = new List<EnemyView>();

    public List<EnemyView> Pool => _pool;

    public event Action<int> OnRequestAttack;

    public void Initialize(Enemies enemy, EnemyView prefab)
    {
        var spawned = Instantiate(prefab, _container.transform);
        spawned.gameObject.SetActive(false);

        spawned.TryRequestAttack += TryAttack;

        spawned.SetCharacteristics(enemy);
        spawned.Init(_rewardButtonView, _saverData);       

        _pool.Add(spawned);
    }

    public bool TryGetObject(out EnemyView result)
    {
        result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return result != null;
    }

    public void TryAttack(int damage)
    {
        OnRequestAttack?.Invoke(damage);
    }
}
