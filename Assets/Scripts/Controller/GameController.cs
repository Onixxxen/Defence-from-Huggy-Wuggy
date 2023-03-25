using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _armorSlider;

    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _armorText;

    private NeuronCollector _neuronCollector;
    private NeuronCollectorPresenter _neuronPresenter;

    private DayChanger _dayChanger;
    private DayChangerPresenter _dayChangerPresenter;

    private Enemy _enemy;
    private EnemyPresenter _enemyPresenter;

    private BrainPresenter _brainPresenter;
    private RecoveryPresenter _recoveryPresenter;

    private LoseGame _loseGame;
    private LoseGamePresenter _loseGamePresenter;

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
        _enemyPresenter.Enable();
        _brainPresenter.Enable();
        _recoveryPresenter.Enable();
        _loseGamePresenter.Enable();
    }

    private void OnDisable()
    {
        _neuronPresenter.Disable();
        _dayChangerPresenter.Disable();
        _enemyPresenter.Disable();
        _brainPresenter.Disable();
        _recoveryPresenter.Disable();
        _loseGamePresenter.Disable();
    }

    private void Awake()
    {
        Neuron = new Neuron();
        Health = new Health();
        Armor = new Armor();

        _neuronCollector = new NeuronCollector(Neuron);        
        _neuronPresenter = new NeuronCollectorPresenter();        

        DevelopmentShop = new DevelopmentShop(Neuron);
        HealthShop = new HealthShop(Health, Neuron);
        ArmorShop = new ArmorShop(Armor, Neuron);

        _enemy = new Enemy(Health, Armor);
        _enemyPresenter = new EnemyPresenter();

        _dayChanger = new DayChanger(_enemy, Health, Armor);
        _dayChangerPresenter = new DayChangerPresenter();

        _brainPresenter = new BrainPresenter();
        _recoveryPresenter = new RecoveryPresenter();

        _loseGame = new LoseGame(Neuron, Health, Armor, _dayChanger);
        _loseGamePresenter = new LoseGamePresenter();

        var neuronCollectorView = FindObjectOfType<NeuronCollectorView>(true);
        var brainView = FindObjectOfType<BrainView>(true);
        var dayChangerView = FindObjectOfType<DayChangerView>(true);
        var objectPool = FindObjectOfType<ObjectPoolView>(true);
        var recoveryHealthView = FindObjectOfType<RecoveryHealthView>(true);
        var recoveryArmorView = FindObjectOfType<RecoveryArmorView>(true);
        var loseGameView = FindObjectOfType<LoseGameView>(true);

        _neuronPresenter.Init(neuronCollectorView, brainView, _neuronCollector);
        _dayChangerPresenter.Init(_dayChanger, dayChangerView);
        brainView.Init(dayChangerView, loseGameView, _healthSlider, _armorSlider, _healthText, _armorText); ;
        _enemyPresenter.Init(_enemy, objectPool, brainView);
        _brainPresenter.Init(Health, Armor, brainView, _dayChanger);
        _recoveryPresenter.Init(recoveryHealthView, recoveryArmorView, Health, Armor, brainView);
        _loseGamePresenter.Init(loseGameView, _loseGame, neuronCollectorView);
    }
}
