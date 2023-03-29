using TMPro;
using UnityEngine;

public class TranslateView : MonoBehaviour
{
    [Multiline]
    [SerializeField] private string _ruText;
    [Multiline]
    [SerializeField] private string _enText;
    [Multiline]
    [SerializeField] private string _trText;
    [Multiline]
    [SerializeField] private string _ukText;

    public void SetRussianLanguage()
    {
        GetComponent<TMP_Text>().text = _ruText;
    }

    public void SetEnglishLanguage()
    {
        GetComponent<TMP_Text>().text = _enText;
    }

    public void SetTurkeyLanguage()
    {
        GetComponent<TMP_Text>().text = _trText;
    }

    public void SetUkrainianLanguage()
    {
        GetComponent<TMP_Text>().text = _ukText;
    }
}
