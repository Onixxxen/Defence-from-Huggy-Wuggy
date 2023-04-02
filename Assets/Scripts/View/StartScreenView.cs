using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartScreenView : MonoBehaviour
{
    [SerializeField] private Canvas _commonCanvas;
    [SerializeField] private CanvasGroup _gameModeCanvas;
    [SerializeField] private ObjectPoolView _objectPool;

    private Camera _camera;
    private float _cameraNormalSize;

    private DayChangerView _dayChangerView;
    private PauseView _pauseView;

    public Camera Camera => _camera;
    public float CameraNormalSize => _cameraNormalSize;

    public void Init(DayChangerView dayChangerView, PauseView pauseView)
    {
        _dayChangerView = dayChangerView;
        _pauseView = pauseView;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverUIObject())
                return;

            CloseStartScreen();
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    public void ActivateStartScreen()
    {
        _pauseView.Pause(true);
        _camera = Camera.main;
        _cameraNormalSize = _camera.orthographicSize;
        DOTween.To(x => _camera.orthographicSize = x, _camera.orthographicSize, 15, 2);
        gameObject.SetActive(true);
        _commonCanvas.gameObject.SetActive(false);
        _gameModeCanvas.gameObject.SetActive(false);
    }

    public void CloseStartScreen()
    {
        DOTween.To(x => _camera.orthographicSize = x, _camera.orthographicSize, _cameraNormalSize, 2);
        _commonCanvas.gameObject.SetActive(true);
        _gameModeCanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
        _pauseView.Pause(false);
    }

    public void ChangeCameraNormalSie(int size)
    {
        _cameraNormalSize = size;
    }
}
