using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPoolView : MonoBehaviour
{
    [SerializeField] private BrainView _brain;
    [SerializeField] private GameObject _container;

    private List<EnemyView> _pool = new List<EnemyView>();

    public void Initialize(Enemies enemy, EnemyView prefab)
    {
        var spawned = Instantiate(prefab, _container.transform);
        spawned.gameObject.SetActive(false);
        spawned.SetCharacteristics(enemy);
        spawned.Init(_brain);

        _pool.Add(spawned);
    }

    public bool TryGetObject(out EnemyView result)
    {
        result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return result != null;
    }
}
