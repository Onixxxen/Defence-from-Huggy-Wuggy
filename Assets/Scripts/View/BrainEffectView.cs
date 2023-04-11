using UnityEngine;

public class BrainEffectView : MonoBehaviour
{
    [SerializeField] private GameObject _neuronEffect;
    [SerializeField] private GameObject _recoveryHealthEffect;
    [SerializeField] private GameObject _recoveryArmorEffect;

    public void StartNeuronEffect()
    {
        Instantiate(_neuronEffect, transform.position, Quaternion.identity);
    }

    public void StartRecoveryHealthEffect()
    {
        Instantiate(_recoveryHealthEffect, transform.position, Quaternion.Euler(x: 90, y: 0, z: 0));
    }

    public void StartRecoveryArmorEffect()
    {
        Instantiate(_recoveryArmorEffect, transform.position, Quaternion.Euler(x: 90, y: 0, z: 0));
    }
}
