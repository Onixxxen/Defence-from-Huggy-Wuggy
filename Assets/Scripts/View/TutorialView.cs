using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Example;

public class TutorialView : MonoBehaviour
{
    [Header("Tutorial Objects")]
    [SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private GameObject _tutorialWindow;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _avatar;
    [SerializeField] private Button _continueButton;

    [Header("Data")]
    [SerializeField] private NeuronCollectorView _neuron;
    [SerializeField] private DayChangerView _dayChangerView;
    [SerializeField] private DevelopmentShopView _developmentShopView;
    [SerializeField] private ArmorShopView _armorShopView;
    [SerializeField] private DevelopmentShopButton _developmentButton;
    [SerializeField] private ArmorShopButton _armorButton;
    [SerializeField] private PauseView _pauseView;
    [SerializeField] private SaverData _saverData;

    [Header("View")]
    [SerializeField] private Sprite[] _avatars;
    [SerializeField] private TutorialDialogs _ruTutorialDialogs;
    [SerializeField] private TutorialDialogs _enTutorialDialogs;
    [SerializeField] private TutorialDialogs _trTutorialDialogs;
    [SerializeField] private TutorialDialogs _ukTutorialDialogs;

    public TutorialDialogs RuTutorialDialogs => _ruTutorialDialogs;
    public TutorialDialogs EnTutorialDialogs => _enTutorialDialogs;
    public TutorialDialogs TrTutorialDialogs => _trTutorialDialogs;
    public TutorialDialogs UkTutorialDialogs => _ukTutorialDialogs;

    private TutorialDialogs _currentTutorialDialogs;
    private DevelopmentSellButton _developmentSellButton;
    private ArmorSellButton _armorSellButton;

    private bool _isDialogEnd;
    private bool _isTutorialCompleted;
    private int _stage = 1;

    private void Start()
    {
        _continueButton.onClick.AddListener(ContinueDialog);        
    }

    public IEnumerator RunTutorialDialog()
    {
        if (_isTutorialCompleted)
            yield break;

        if (_stage == 1)
        {
            _pauseView.Pause(true);
            _tutorialPanel.SetActive(true);
            for (int i = 0; i < _currentTutorialDialogs.DialogStageOne.Length; i++)
            {
                _isDialogEnd = false;
                _avatar.sprite = _avatars[i];
                //_text.text = _currentTutorialDialogs.DialogStageOne[i];
                StartCoroutine(WriteSentence(_currentTutorialDialogs.DialogStageOne[i]));

                yield return new WaitUntil(() => _isDialogEnd);
            }
            EndDialog();
            _pauseView.Pause(false);

            yield return new WaitUntil(() => _neuron.NeuronIsCollected(50));
            _stage++;
            _saverData.SaveTutorialStage(_stage);
        }        

        if (_stage == 2)
        {
            _pauseView.Pause(true);
            _tutorialPanel.SetActive(true);
            for (int i = 0; i < _currentTutorialDialogs.DialogStageTwo.Length; i++)
            {
                _isDialogEnd = false;
                _avatar.sprite = _avatars[i];
                //_text.text = _currentTutorialDialogs.DialogStageTwo[i];
                StartCoroutine(WriteSentence(_currentTutorialDialogs.DialogStageTwo[i]));

                yield return new WaitUntil(() => _isDialogEnd);
            }

            _tutorialWindow.SetActive(false);
            _developmentButton.gameObject.SetActive(true);

            yield return new WaitUntil(_developmentButton.ButtonIsPressed);

            _developmentButton.gameObject.SetActive(false);
            _tutorialWindow.SetActive(true);
            EndDialog();
            _pauseView.Pause(false);

            ChangeDevelopmentSellButton(true);
            yield return new WaitUntil(_developmentSellButton.ButtonIsPressed);
            ChangeDevelopmentSellButton(false);
            _stage++;
            _saverData.SaveTutorialStage(_stage);
        }        

        if (_stage == 3)
        {
            _pauseView.Pause(true);
            _tutorialPanel.SetActive(true);
            for (int i = 0; i < _currentTutorialDialogs.DialogStageThree.Length; i++)
            {
                _isDialogEnd = false;
                _avatar.sprite = _avatars[i];
                //_text.text = _currentTutorialDialogs.DialogStageThree[i];
                StartCoroutine(WriteSentence(_currentTutorialDialogs.DialogStageThree[i]));

                yield return new WaitUntil(() => _isDialogEnd);
            }
            EndDialog();
            _pauseView.Pause(false);

            yield return new WaitUntil(() => _neuron.NeuronIsCollected(100));
            _stage++;
            _saverData.SaveTutorialStage(_stage);
        }        

        if (_stage == 4)
        {
            _pauseView.Pause(true);
            _tutorialPanel.SetActive(true);
            for (int i = 0; i < _currentTutorialDialogs.DialogStageFour.Length; i++)
            {
                _isDialogEnd = false;
                _avatar.sprite = _avatars[i];
                //_text.text = _currentTutorialDialogs.DialogStageFour[i];
                StartCoroutine(WriteSentence(_currentTutorialDialogs.DialogStageFour[i]));

                yield return new WaitUntil(() => _isDialogEnd);
            }

            _tutorialWindow.SetActive(false);
            _armorButton.gameObject.SetActive(true);

            yield return new WaitUntil(_armorButton.ButtonIsPressed);

            _armorButton.gameObject.SetActive(false);
            _tutorialWindow.SetActive(true);
            EndDialog();
            _pauseView.Pause(false);

            ChangeArmorSellButton(true);
            yield return new WaitUntil(_armorSellButton.ButtonIsPressed);
            ChangeArmorSellButton(false);
            _stage++;
            _saverData.SaveTutorialStage(_stage);
        }        

        if (_stage == 5)
        {
            _pauseView.Pause(true);
            _tutorialPanel.SetActive(true);
            for (int i = 0; i < _currentTutorialDialogs.DialogStageFive.Length; i++)
            {
                _isDialogEnd = false;
                _avatar.sprite = _avatars[i];
                //_text.text = _currentTutorialDialogs.DialogStageFive[i];
                StartCoroutine(WriteSentence(_currentTutorialDialogs.DialogStageFive[i]));

                yield return new WaitUntil(() => _isDialogEnd);
            }
            EndDialog();
            _pauseView.Pause(false);

            yield return new WaitUntil(_dayChangerView.IsDefenceMode);
            _stage++;
            _saverData.SaveTutorialStage(_stage);
        }        

        if (_stage == 6)
        {
            _pauseView.Pause(true);
            _tutorialPanel.SetActive(true);
            for (int i = 0; i < _currentTutorialDialogs.DialogStageSix.Length; i++)
            {
                _isDialogEnd = false;
                _avatar.sprite = _avatars[i];
                //_text.text = _currentTutorialDialogs.DialogStageSix[i];
                StartCoroutine(WriteSentence(_currentTutorialDialogs.DialogStageSix[i]));

                yield return new WaitUntil(() => _isDialogEnd);
            }
            EndDialog();
            _pauseView.Pause(false);

            yield return new WaitUntil(_dayChangerView.IsClickerMode);
            _stage++;
            _saverData.SaveTutorialStage(_stage);
        }        

        if (_stage == 7)
        {
            _pauseView.Pause(true);
            _tutorialPanel.SetActive(true);
            for (int i = 0; i < _currentTutorialDialogs.DialogStageSeven.Length; i++)
            {
                _isDialogEnd = false;
                _avatar.sprite = _avatars[i];
                //_text.text = _currentTutorialDialogs.DialogStageSeven[i];
                StartCoroutine(WriteSentence(_currentTutorialDialogs.DialogStageSeven[i]));

                yield return new WaitUntil(() => _isDialogEnd);
            }
            EndDialog();
            _pauseView.Pause(false);
            _isTutorialCompleted = true;
            _saverData.SaveTutorialCompletedStatus(_isTutorialCompleted);
        }        
    }

    private void EndDialog()
    {
        _tutorialPanel.SetActive(false);
    }

    public void ContinueDialog()
    {
        _isDialogEnd = true;
    }

    private IEnumerator WriteSentence(string sentence)
    {
        _continueButton.interactable = false;
        _text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            _text.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
        _continueButton.interactable = true;
    }

    public void InitDevelopmentSellButton(DevelopmentSellButton sellButton)
    {
        _developmentSellButton = sellButton;
    }

    public void InitArmorSellButton(ArmorSellButton sellButton)
    {
        _armorSellButton = sellButton;
    }

    public void ChangeDevelopmentSellButton(bool isActive)
    {
        for (int i = 0; i < _developmentShopView.SpawnedItem.Count; i++)
            _developmentShopView.SpawnedItem[i].TutorialSellButton.gameObject.SetActive(isActive);
    }

    public void ChangeArmorSellButton(bool isActive)
    {
        for (int i = 0; i < _armorShopView.SpawnedItem.Count; i++)
            _armorShopView.SpawnedItem[i].TutorialSellButton.gameObject.SetActive(isActive);
    }

    public void ChangeCurrentDialog(string language)
    {
        if (language == "ru")
            _currentTutorialDialogs = RuTutorialDialogs;
        else if (language == "en")
            _currentTutorialDialogs = EnTutorialDialogs;
        else if (language == "tr")
            _currentTutorialDialogs = TrTutorialDialogs;
        else if (language == "uk")
            _currentTutorialDialogs = UkTutorialDialogs;

    }

    public void LoadTutorialData()
    {
        _stage = YandexGame.savesData.SavedTutorialStage;
        _isTutorialCompleted = YandexGame.savesData.IsTutorialCompleted;
    }
}
