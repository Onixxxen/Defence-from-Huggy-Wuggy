using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;  

public class RewardButton : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _reward;
    [SerializeField] private Slider _progressSlider;

    private RewardButtonView _view;

    public string Name => _name;
    public int Reward => _reward;
    public Slider ProgressSlider => _progressSlider;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => ActivateReward(_reward));
    }

    private void Start()
    {
        float normalScale = transform.localScale.x;
        transform.localScale = new Vector3(0, 0, 0);
        transform.DOScale(normalScale, 0.5f);
        _progressSlider.value = _progressSlider.minValue;
    }

    public void Init(RewardButtonView view)
    {
        _view = view;
    }
    public void ActivateSlider(int duration)
    {
        StartCoroutine(TryDestroyButton(duration));
    }

    private void ActivateReward(int rewardID)
    {
        YandexGame.RewVideoShow(rewardID);
    }

    private IEnumerator TryDestroyButton(int duration)
    {
        _progressSlider.DOValue(_progressSlider.maxValue, duration);

        yield return new WaitUntil(SliderIsFull);

        transform.DOScale(0, 0.5f);

        yield return new WaitUntil(ButtonIsScaled);

        Destroy(gameObject);

        for (int i = 0; i < _view.SpawnedButton.Count; i++)
            if (_view.SpawnedButton[i].Name == _name)
                _view.SpawnedButton.Remove(this);

    }

    private bool SliderIsFull()
    {
        return _progressSlider.value == _progressSlider.maxValue;
    }

    private bool ButtonIsScaled()
    {
        return transform.localScale.x == 0;
    }
}
