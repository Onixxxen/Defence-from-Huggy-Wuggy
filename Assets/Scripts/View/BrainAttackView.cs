using UnityEngine;

public class BrainAttackView : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private float _angleInDegrees;
    [SerializeField] private BrainView _brainView;

    private float g = Physics.gravity.y;
    private bool _bulletIsCreated;

    public bool BulletIsCreated => _bulletIsCreated;
    public BrainView BrainView => _brainView;

    private void Update()
    {
        _spawnTransform.localEulerAngles = new Vector3(-_angleInDegrees, 0f, 0f);
    }

    public void Shot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 fromTo = hit.point - transform.position;
            Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

            transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

            float x = fromToXZ.magnitude;
            float y = fromTo.y;

            float angleInRadians = _angleInDegrees * Mathf.PI / 180;

            float v2 = (g * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
            float v = Mathf.Sqrt(Mathf.Abs(v2));

            Bullet newBullet = Instantiate(_bullet, _spawnTransform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody>().velocity = _spawnTransform.forward * v;
            newBullet.Init(this);
            ChangeBuletStatus(true);
        }

    }

    public void ChangeBuletStatus(bool status)
    {
        _bulletIsCreated = status;
    }
}
