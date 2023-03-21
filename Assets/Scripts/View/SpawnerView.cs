using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerView : ObjectPoolView
{
    [SerializeField] private List<Enemies> _enemies = new List<Enemies>();
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _secondsBetweenSpawn;

    private float _elapsedTime;

    private void Start()
    {
        for (int i = 0; i < _enemies.Count; i++)
            Initialize(_enemies[i], _enemies[i].Template);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        //добавить условие если мод равен тауэр дефенс
        if (_elapsedTime >= _secondsBetweenSpawn)
        {
            if (TryGetObject(out EnemyView enemy))
            {
                _elapsedTime = 0;

                int spawnPointNumber = Random.Range(0, _spawnPoints.Length);

                SetEnemy(enemy, _spawnPoints[spawnPointNumber].position);
            }
        }
    }

    private void SetEnemy(EnemyView enemy, Vector3 spawnPoint)
    {
        enemy.gameObject.SetActive(true);
        enemy.transform.position = spawnPoint;
    }
}
