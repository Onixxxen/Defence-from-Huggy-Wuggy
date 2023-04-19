using UnityEngine;
using UnityEngine.UI;

public class DevelopmentSellButton : MonoBehaviour
{
    private bool _isPreseed = false;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(PressButton);
    }

    public void PressButton()
    {
        _isPreseed = true;
    }

    public bool ButtonIsPressed()
    {
        return _isPreseed == true;
    }
}
