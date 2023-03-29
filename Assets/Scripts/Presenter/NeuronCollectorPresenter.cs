
using UnityEngine;

public class NeuronCollectorPresenter
{
    private NeuronCollector _neuronCollector;
    private NeuronCollectorView _neuronCollectorView;
    private Neuron _neuron;
    private BrainView _brainView;

    public void Init(NeuronCollectorView neuronView, BrainView brainView, NeuronCollector neuronModel, Neuron neuron)
    {
        _neuronCollectorView = neuronView;
        _brainView = brainView;
        _neuronCollector = neuronModel;   
        _neuron = neuron;
    }

    public void Enable()
    {
        _brainView.ChangeNeuronCount += _neuronCollector.CollectNeuron;
        _neuronCollectorView.TryGetNeuronCount += RequestNeuronCount;

        _neuronCollector.NeuronCountChanged += OnChangeNeuron;
        _neuron.GetNeuronCount += OnGetNeuronCount;
    }

    public void Disable()
    {
        _brainView.ChangeNeuronCount -= _neuronCollector.CollectNeuron;
        _neuronCollectorView.TryGetNeuronCount -= RequestNeuronCount;

        _neuronCollector.NeuronCountChanged -= OnChangeNeuron;
    }

    private void OnChangeNeuron(int count)
    {
        _neuronCollectorView.ChangeNeuronView(count);
    }

    private void RequestNeuronCount()
    {
        _neuron.NeuronCountRequest();
    }

    private void OnGetNeuronCount(int count)
    {
        _neuronCollectorView.ChangeNeuronView(count);
    }
}
