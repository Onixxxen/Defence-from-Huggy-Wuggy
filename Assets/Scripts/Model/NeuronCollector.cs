using System;
using UnityEngine;

public class NeuronCollector : Neuron
{
    public event Action<int> NeuronCountChanged;

    private Neuron _neuron;

    public NeuronCollector(Neuron neuron)
    {
        _neuron = neuron;
    }

    public void CollectNeuron()
    {
        _neuron.AddNeuron();
        NeuronCountChanged?.Invoke(_neuron.Count);
    }
}
