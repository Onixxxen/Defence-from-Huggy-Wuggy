using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SpawnerView : ObjectPoolView
{
    [SerializeField] private List<Enemies> _enemies = new List<Enemies>();
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _secondsBetweenSpawn;

    private DayChangerView _dayChangerView;
    private float _previousSecondBetweenSpawn;
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
        _previousSecondBetweenSpawn = _secondsBetweenSpawn;
        _secondsBetweenSpawn = newValue;
    }

    public void BackSecondBetweenSpawn()
    {
        _secondsBetweenSpawn = _previousSecondBetweenSpawn;
    }

    public void UpdatePreviousSecondBetweenSpawn()
    {
        _previousSecondBetweenSpawn = _secondsBetweenSpawn;
    }

    public void LoadSpawnerData()
    {
        _secondsBetweenSpawn = YandexGame.savesData.SavedSecondBetweenSpawn;
    }
}
