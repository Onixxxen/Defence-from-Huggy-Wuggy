using UnityEngine;

public class GameController : MonoBehaviour
{
    private NeuronCollector _neuronCollector;
    private NeuronCollectorPresenter _neuronPresenter;

    private DayChanger _dayChanger;
    private DayChangerPresenter _dayChangerPresenter;

    public Neuron Neuron { get; private set; }
    public Health Health { get; private set; }
    public Armor Armor { get; private set; }

    public DevelopmentShop DevelopmentShop { get; private set; }
    public HealthShop HealthShop { get; private set; }
    public ArmorShop ArmorShop { get; private set; }

    private void OnEnable()
    {
        _neuronPresenter.Enable();
        _dayChangerPresenter.Enable();
    }

    private void OnDisable()
    {
        _neuronPresenter.Disable();
        _dayChangerPresenter.Disable();
    }

    private void Awake()
    {
        Neuron = new Neuron();
        Health = new Health();
        Armor = new Armor();

        _neuronCollector = new NeuronCollector(Neuron);        
        _neuronPresenter = new NeuronCollectorPresenter();

        _dayChanger = new DayChanger();
        _dayChangerPresenter = new DayChangerPresenter();

        DevelopmentShop = new DevelopmentShop(Neuron);
        HealthShop = new HealthShop(Health, Neuron);
        ArmorShop = new ArmorShop(Armor, Neuron);

        var neuronCollectorView = FindObjectOfType<NeuronCollectorView>(true);
        var brainView = FindObjectOfType<BrainView>(true);
        var dayChangerView = FindObjectOfType<DayChangerView>(true);

        _neuronPresenter.Init(neuronCollectorView, brainView, _neuronCollector);
        _dayChangerPresenter.Init(_dayChanger, dayChangerView);
        brainView.Init(dayChangerView);
    }
}
