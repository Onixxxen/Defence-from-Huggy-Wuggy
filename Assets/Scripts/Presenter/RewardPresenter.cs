using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPresenter
{
    private Neuron _neuron;
    private RewardView _rewardButtonView;

    public void Init(Neuron neuron, RewardView rewardButtonView)
    {
        _neuron = neuron;
        _rewardButtonView = rewardButtonView;
    }

    public void Enable()
    {
        _rewardButtonView.TryMultiplyNeuronPerClick += RequestMultiplyNeuronPerClick;
        _rewardButtonView.TryBackNeuronPerClick += RequestBackNeuronPerClick;
    }

    public void Disable()
    {
        _rewardButtonView.TryMultiplyNeuronPerClick += RequestMultiplyNeuronPerClick;
        _rewardButtonView.TryBackNeuronPerClick += RequestBackNeuronPerClick;
    }

    private void RequestMultiplyNeuronPerClick(int factor)
    {
        _neuron.MultiplyNeuronPerClick(factor);
    }

    private void RequestBackNeuronPerClick(int factor)
    {
        _neuron.BackNeuronPerClick(factor);
    }
}
