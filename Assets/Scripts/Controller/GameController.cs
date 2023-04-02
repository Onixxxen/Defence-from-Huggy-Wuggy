using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Example;

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
    private DayChangerView _dayChangerView;

    private Enemy _enemy;
    private EnemyPresenter _enemyPresenter;

    private BrainPresenter _brainPresenter;
    private RecoveryPresenter _recoveryPresenter;

    private LoseGame _loseGame;
    private LoseGamePresenter _loseGamePresenter;

    private StartScreenView _startScreenView;
    private PauseView _pauseView;

    private RewardPresenter _rewardButtonPresenter;

    private const int _clickerMode = 1;
    private const int _towerDefenceMode = 2;

    public DayChanger DayChanger => _dayChanger;

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
        _rewardButtonPresenter.Enable();
    }

    private void OnDisable()
    {
        _neuronPresenter.Disable();
        _dayChangerPresenter.Disable();
        _enemyPresenter.Disable();
        _brainPresenter.Disable();
        _recoveryPresenter.Disable();
        _loseGamePresenter.Disable();
        _rewardButtonPresenter.Disable();
    }

    private void Awake()
    {
        var saverData = FindObjectOfType<SaverData>();

        Neuron = new Neuron(saverData);
        Health = new Health(saverData);
        Armor = new Armor(saverData);

        _neuronCollector = new NeuronCollector(Neuron);
        _neuronPresenter = new NeuronCollectorPresenter();

        DevelopmentShop = new DevelopmentShop(Neuron, saverData);
        HealthShop = new HealthShop(Health, Neuron, saverData);
        ArmorShop = new ArmorShop(Armor, Neuron, saverData);

        _enemy = new Enemy(Health, Armor, saverData);
        _enemyPresenter = new EnemyPresenter();

        _dayChanger = new DayChanger(_enemy, Health, Armor, saverData);
        _dayChangerPresenter = new DayChangerPresenter();

        _brainPresenter = new BrainPresenter();
        _recoveryPresenter = new RecoveryPresenter();

        _loseGame = new LoseGame(Neuron, Health, Armor, _enemy, _dayChanger);
        _loseGamePresenter = new LoseGamePresenter();

        _rewardButtonPresenter = new RewardPresenter();

        var neuronCollectorView = FindObjectOfType<NeuronCollectorView>(true);
        var brainView = FindObjectOfType<BrainView>(true);
        _dayChangerView = FindObjectOfType<DayChangerView>(true);
        var objectPool = FindObjectOfType<ObjectPoolView>(true);
        var recoveryHealthView = FindObjectOfType<RecoveryHealthView>(true);
        var recoveryArmorView = FindObjectOfType<RecoveryArmorView>(true);
        var loseGameView = FindObjectOfType<LoseGameView>(true);
        _startScreenView = FindObjectOfType<StartScreenView>(true);
        _pauseView = FindObjectOfType<PauseView>(true);
        var pauseButtonView = FindObjectOfType<PauseButtonView>(true);
        var rewardView = FindObjectOfType<RewardView>(true);
        var rewardButtonView = FindObjectOfType<RewardButtonView>(true);

        _neuronPresenter.Init(neuronCollectorView, brainView, _neuronCollector, Neuron);
        _dayChangerPresenter.Init(_dayChanger, _dayChangerView, objectPool);
        brainView.Init(_dayChangerView, loseGameView, _healthSlider, _armorSlider, _healthText, _armorText); ;
        _enemyPresenter.Init(_enemy, objectPool, brainView);
        _brainPresenter.Init(Health, Armor, brainView, _dayChanger);
        _recoveryPresenter.Init(recoveryHealthView, recoveryArmorView, Health, Armor, brainView);
        _loseGamePresenter.Init(loseGameView, _loseGame, neuronCollectorView);
        _startScreenView.Init(_dayChangerView, _pauseView);
        _pauseView.Init(objectPool, _dayChangerView, recoveryArmorView, recoveryHealthView, rewardButtonView);
        pauseButtonView.Init(_startScreenView);
        _rewardButtonPresenter.Init(Neuron, rewardView);

        _startScreenView.ActivateStartScreen();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            _pauseView.Pause(true);
        else
            _pauseView.Pause(false);
    }    
}
