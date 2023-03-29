using System;
using UnityEngine;
using YG;
using YG.Example;

public class Neuron : Brain
{
    private int _count;
    private int _perClick = 1;

    private SaverData _saverData;

    public Neuron(SaverData saverData)
    {
        _saverData = saverData;
    }

    public int Count => _count;
    public int PerClick => _perClick;

    public event Action<int> GetNeuronCount;

    public void AddNeuron()
    {
        _count += _perClick;
        _saverData.SaveNeuronCount(_count);
    }

    public void RemoveNeuron(int count)
    {
        _count -= count;
        _saverData.SaveNeuronCount(_count);
    }

    public void NeuronCountRequest()
    {
        GetNeuronCount?.Invoke(_count);
    }

    public void ChangeNeuronPerClick(int count)
    {
        _perClick += count;
        _saverData.SaveNeuronPerClick(_perClick);
    }

    public void Reset()
    {
        _count = 0;
        _perClick = 1;
        _saverData.SaveNeuronCount(_count);
        _saverData.SaveNeuronPerClick(_perClick);
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

    public void LoadNeuronData()
    {
        _count = YandexGame.savesData.SavedNeuron;
        _perClick = YandexGame.savesData.SavedNeuronPerClick;
    }
}
