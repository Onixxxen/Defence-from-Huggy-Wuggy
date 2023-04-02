using UnityEngine;
using UnityEngine.EventSystems;

public class CloseStartScreenView : MonoBehaviour
{
    [SerializeField] private StartScreenView _startScreenView;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

             _startScreenView.CloseStartScreen();
        }
    }
}
