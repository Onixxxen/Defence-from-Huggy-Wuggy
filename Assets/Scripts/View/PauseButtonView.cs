using UnityEngine;
using UnityEngine.UI;

public class PauseButtonView : MonoBehaviour
{
    private StartScreenView _startScreenView;

    public void Init(StartScreenView startScreenView)
    {
        _startScreenView = startScreenView;
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ActivateStartMenu);
    }

    public void ActivateStartMenu()
    {
        if (_startScreenView.Camera.orthographicSize == _startScreenView.CameraNormalSize)
            _startScreenView.ActivateStartScreen();
    }
}
