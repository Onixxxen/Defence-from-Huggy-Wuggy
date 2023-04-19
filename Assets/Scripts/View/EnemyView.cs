using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System;
using YG;
using YG.Example;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _damage;
    [SerializeField] private int _speed;

    private NavMeshAgent _agent;
    private TargetPoint _targetPoint;

    private RewardButtonView _rewardButtonView;
    private SaverData _saverData;

    private Enemies _enemy;
    private PauseView _pauseView;

    private int _normalSpeed => _speed;
    private float _floatDamage;

    private EnemySound _huggySound;
    private EnemySound _kissySound;

    public int Damage => _damage;
    public int Speed => _speed;
    public int NormalSpeed => _normalSpeed;

    public event Action<int> TryRequestAttack;

    public void Init(RewardButtonView rewardButtonView, SaverData saverData, Enemies enemy, PauseView pauseView)
    {
        _rewardButtonView = rewardButtonView;
        _saverData = saverData;
        _enemy = enemy;
        _pauseView = pauseView;
    }

    public void InitSound(EnemySound huggySound, EnemySound kissySound)
    {
        _huggySound = huggySound;
        _kissySound = kissySound;
    }

    private void Awake()
    {
        _targetPoint = FindObjectOfType<TargetPoint>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        int endPointNumber = UnityEngine.Random.Range(0, _targetPoint.TargetPoints.Count);
        _agent.destination = _targetPoint.TargetPoints[endPointNumber].position;
        TryActivateSpawnSound();

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out BrainView brain) && !_pauseView.IsPause)
        {
            Debug.Log(_pauseView.IsPause);
            _agent.isStopped = true;
            StartCoroutine(Attack());
            TryActiveBonusButton();
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Run");
            StopCoroutine(Attack());
        }
    }

    public void SetCharacteristics(Enemies enemy)
    {
        _damage = enemy.Damage;
        _floatDamage = enemy.Damage;
        _speed = enemy.Speed;
    }

    public void ChangeSpeed(int newSpeed)
    {
        _agent.speed = newSpeed;
    }

    public void ChangeDamage(float newDamage)
    {
        _floatDamage *= newDamage;
        _damage = (int)_floatDamage;

        if (YandexGame.savesData.IsTowerDefenceLoaded)
            _saverData.SaveEnemyDamage(_damage);
    }

    public void ResetDamage()
    {
        _damage = 1;

        if (YandexGame.savesData.IsTowerDefenceLoaded)
            _saverData.SaveEnemyDamage(_damage);
    }

    public IEnumerator Attack()
    {
        GetComponent<Animator>().SetTrigger("Attack");
        ActivateAttackSound();
        TryRequestAttack?.Invoke(_damage);
        yield return new WaitForSeconds(1);
        //StartCoroutine(Attack());
    }

    private void TryActiveBonusButton()
    {
        if (!_rewardButtonView.SlowDownButttonIsSpawned)
        {
            int randomButton = UnityEngine.Random.Range(1, 20); // желательно проверить

            if (randomButton == 1)
                for (int i = 0; i < _rewardButtonView.RewardButtons.Count; i++)
                    if (_rewardButtonView.RewardButtons[i].Name == "SlowDownButton")
                        _rewardButtonView.ActivateRewardButton(_rewardButtonView.RewardButtons[i]);
        }

        if (!_rewardButtonView.RecoveryBrainButtonIsSpawned)
        {
            int randomButton = UnityEngine.Random.Range(1, 10);

            if (randomButton == 1)
                for (int i = 0; i < _rewardButtonView.RewardButtons.Count; i++)
                    if (_rewardButtonView.RewardButtons[i].Name == "RecoveryBrainButton")
                        _rewardButtonView.ActivateRewardButton(_rewardButtonView.RewardButtons[i]);
        }
    }

    private void TryActivateSpawnSound()
    {
        if (_enemy != null)
        {
            int random = UnityEngine.Random.Range(0, 3);

            if (random == 1)
            {
                if (_name == "HuggyWuggy" || _name == "KillyWilly")
                    _huggySound.SpawnSound.Play();
                else if (_name == "KissyMissy")
                    _kissySound.SpawnSound.Play();
            }
        }
    }

    private void ActivateAttackSound()
    {
        if (_name == "HuggyWuggy" || _name == "KillyWilly")
            _huggySound.AttackSound.Play();
        else if (_name == "KissyMissy")
            _kissySound.AttackSound.Play();
    }

    public void ActivateDieSound()
    {
        if (_name == "HuggyWuggy" || _name == "KillyWilly")
            _huggySound.DieSound.Play();
        else if (_name == "KissyMissy")
            _kissySound.DieSound.Play();
    }    

    public void LoadEnemyData()
    {
        _damage = YandexGame.savesData.SavedEnemyDamage;
    }
}
