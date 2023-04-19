using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _explosionInEnemy;
    [SerializeField] private GameObject _explosionInGround;

    private BrainAttackView _brainAttackView;

    public void Init(BrainAttackView brainAttackView)
    {
        _brainAttackView = brainAttackView;
    }

    private void OnTriggerEnter(Collider other)
    {     
        if (other.TryGetComponent(out EnemyView enemy))
        {
            _brainAttackView.Boom.Play();            
            enemy.ActivateDieSound();
            Instantiate(_explosionInEnemy, transform.position, Quaternion.identity);
            _brainAttackView.ChangeBuletStatus(false);
            _brainAttackView.BrainView.TryShowSupportingText(2);
            enemy.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out BrainView brain) && !collision.gameObject.TryGetComponent(out EnemyView enemy))
        {
            _brainAttackView.Boom.Play();
            Instantiate(_explosionInGround, transform.position, Quaternion.identity);
            _brainAttackView.ChangeBuletStatus(false);
            Destroy(gameObject);
        }
    }
}
