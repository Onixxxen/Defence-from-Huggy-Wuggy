using System;
using UnityEngine;

public class Neuron : Brain
{
    private int _count;
    private int _perClick = 1;

    public int Count => _count;
    public int PerClick => _perClick;

    public void AddNeuron()
    {
        _count += _perClick;
    }

    public void RemoveNeuron(int count)
    {
        _count -= count;
    }

    public void ChangeNeuronPerClick(int count)
    {
        _perClick += count;
    }

    public void Reset()
    {
        _count = 0;
        _perClick = 1;
    }

    public void MultiplyNeuronPerClick(int factor)
    {
        _perClick *= factor;
    }

    public void BackNeuronPerClick(int factor)
    {
        double convertedPerClick = (double)_perClick / factor;
        _perClick = (int)Math.Ceiling(convertedPerClick);
    }
}
