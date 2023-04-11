using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PanelChangerView : MonoBehaviour
{
    [SerializeField] private GameObject _window;
    [SerializeField] private GameObject _shop;
    [SerializeField] private CanvasGroup _panelBackground;
    [SerializeField] private float _basePosition;

    public void OpenPanel()
    {
        if (gameObject.TryGetComponent(out Animator anim))
            gameObject.GetComponent<ButtonAnimation>().StartAnimation();

        if (_shop != null)
            _shop.SetActive(true);

        if (_panelBackground != null)
        {
            _panelBackground.gameObject.SetActive(true);
            DOTween.To(x => _panelBackground.alpha = x, _panelBackground.alpha, 1, 0.5f);
        }

        _window.SetActive(true);
        _window.GetComponent<Animator>().SetTrigger("OpenPanel");        
    }

    public void ClosePanel()
    {
        if (gameObject.TryGetComponent(out Animator anim))
            gameObject.GetComponent<ButtonAnimation>().StartAnimation();

        StartCoroutine(PanelClosing());
    }

    private IEnumerator PanelClosing()
    {
        _window.GetComponent<Animator>().SetTrigger("ClosePanel");

        if (_panelBackground != null)
            DOTween.To(x => _panelBackground.alpha = x, _panelBackground.alpha, 0, 0.5f);

        yield return new WaitUntil(PanelIsHiden);

        _window.SetActive(false);

        if (_shop != null)
            _shop.SetActive(false);

        if (_panelBackground != null)
            _panelBackground.gameObject.SetActive(false);
    }

    private bool PanelIsHiden()
    {
        if (_panelBackground != null)
            return _window.transform.localPosition.y == _basePosition && _panelBackground.alpha == 0;

        return _window.transform.localPosition.y == _basePosition;
    }
}
