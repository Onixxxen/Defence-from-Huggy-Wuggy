using System;
using UnityEngine;
using YG.Example;

public class NeuronCollector
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
