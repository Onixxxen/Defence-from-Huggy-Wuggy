using UnityEngine;

public class Bullet : MonoBehaviour
{
    private BrainAttackView _brainAttackView;

    public void Init(BrainAttackView brainAttackView)
    {
        _brainAttackView = brainAttackView;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyView enemy))
        {
            enemy.gameObject.SetActive(false);
            _brainAttackView.ChangeBuletStatus(false);
            _brainAttackView.BrainView.TryShowSupportingText(2);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out BrainView brain) && !collision.gameObject.TryGetComponent(out EnemyView enemy))
        {
            _brainAttackView.ChangeBuletStatus(false);
            Destroy(gameObject);
        }
    }
}
