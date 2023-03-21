using UnityEngine;
using TMPro;
using System.Diagnostics;

public class NeuronCollectorView : MonoBehaviour
{
    [SerializeField] private TMP_Text _neuronCount;

    public TMP_Text NeuronCount => _neuronCount;

    public void ChangeNeuronView(int count)
    {
        _neuronCount.text = $"{FormatNumberExtension.FormatNumber(count)}";
    }    
}
