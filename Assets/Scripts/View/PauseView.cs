using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseView : MonoBehaviour
{
    private StartScreenView _startScreenView;

    public void Init(StartScreenView startScreenView)
    {
        _startScreenView = startScreenView;
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Pause);
    }

    public void Pause()
    {
        if (_startScreenView.Camera.orthographicSize == _startScreenView.CameraNormalSize)
        {
            _startScreenView.ActivateStartScreen();
        }
    }
}
