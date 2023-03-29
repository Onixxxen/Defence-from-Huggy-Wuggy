using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerView : ObjectPoolView
{
    [SerializeField] private List<Enemies> _enemies = new List<Enemies>();
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _secondsBetweenSpawn;

    private DayChangerView _dayChangerView;
    private float _elapsedTime;

    public float SecondsBetweenSpawn => _secondsBetweenSpawn;

    private void Start()
    {
        for (int i = 0; i < _enemies.Count; i++)
            Initialize(_enemies[i], _enemies[i].Template);

        _dayChangerView = FindObjectOfType<DayChangerView>();
    }

    private void Update()
    {    
        if (_dayChangerView.TimeProgress > 0.01 && _dayChangerView.TimeProgress < 0.55)
        {
            _elapsedTime += Time.deltaTime;

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
    }

    private void SetEnemy(EnemyView enemy, Vector3 spawnPoint)
    {
        enemy.gameObject.SetActive(true);
        enemy.transform.position = spawnPoint;
    }

    public void ChangeSecondBetweenSpawn(float newValue)
    {
        _secondsBetweenSpawn = newValue;
    }
}
