
using UnityEngine;

public class NeuronCollectorPresenter
{
    private NeuronCollector _neuronCollector;
    private NeuronCollectorView _neuronCollectorView;
    private BrainView _brainView;

    public void Init(NeuronCollectorView neuronView, BrainView brainView, NeuronCollector neuronModel)
    {
        _neuronCollectorView = neuronView;
        _brainView = brainView;
        _neuronCollector = neuronModel;        
    }

    public void Enable()
    {
        _neuronCollector.NeuronCountChanged += OnChangeNeuron;
        _brainView.ChangeNeuronCount += _neuronCollector.CollectNeuron;
    }

    public void Disable()
    {
        _neuronCollector.NeuronCountChanged -= OnChangeNeuron;
        _brainView.ChangeNeuronCount -= _neuronCollector.CollectNeuron;
    }

    public void OnChangeNeuron(int count)
    {
        _neuronCollectorView.ChangeNeuronView(count);
    }
}
