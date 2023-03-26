using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

public class StartScreenView : MonoBehaviour
{
    [SerializeField] private Canvas _commonCanvas;
    [SerializeField] private CanvasGroup _gameModeCanvas;
    [SerializeField] private ObjectPoolView _objectPool;

    private Camera _camera;
    private float _cameraNormalSize;

    private DayChangerView _dayChangerView;

    public Camera Camera => _camera;
    public float CameraNormalSize => _cameraNormalSize;

    public void Init(DayChangerView dayChangerView)
    {
        _dayChangerView = dayChangerView;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            else
                CloseStartScreen();
        }
    }

    public void ActivateStartScreen()
    {
        _camera = Camera.main;
        _cameraNormalSize = _camera.orthographicSize;
        DOTween.To(x => _camera.orthographicSize = x, _camera.orthographicSize, 15, 2);
        gameObject.SetActive(true);
        _commonCanvas.gameObject.SetActive(false);
        _gameModeCanvas.gameObject.SetActive(false);
        Pause(true);
    }

    public void CloseStartScreen()
    {
        DOTween.To(x => _camera.orthographicSize = x, _camera.orthographicSize, _cameraNormalSize, 2);
        _commonCanvas.gameObject.SetActive(true);
        _gameModeCanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
        Pause(false);
    }

    public void Pause(bool isActive)
    {
        if (isActive)
        {
            _dayChangerView.ChangeDayTimeInSecond(1000000);

            for (int i = 0; i < _objectPool.Pool.Count; i++)
                _objectPool.Pool[i].ChangeSpeed(0);
        }
        else
        {
            _dayChangerView.ChangeDayTimeInSecond(_dayChangerView.NormalDayTimeInSecond);

            for (int i = 0; i < _objectPool.Pool.Count; i++)
                _objectPool.Pool[i].ChangeSpeed(_objectPool.Pool[i].NormalSpeed);
        }
    }
}
