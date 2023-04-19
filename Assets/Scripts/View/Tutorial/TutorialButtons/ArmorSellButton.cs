using UnityEngine;
using UnityEngine.UI;

public class ArmorSellButton : MonoBehaviour
{
    private bool _isPreseed = false;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(PressButton);
    }

    public void PressButton()
    {
        Debug.Log("Pressed");
        _isPreseed = true;
    }

    public bool ButtonIsPressed()
    {
        return _isPreseed == true;
    }
}
