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
}
