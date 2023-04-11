using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    private void Start()
    {
        //Debug.Log(gameObject.GetComponent<Button>());
        gameObject.GetComponent<Button>().onClick.AddListener(StartAnimation);
    }

    public void StartAnimation()
    {
        StartCoroutine(ActiveAnimation());
    }

    public IEnumerator ActiveAnimation()
    {
        Debug.Log("-------------------------");
        Debug.Log(gameObject.GetComponent<Animator>());
        gameObject.GetComponent<Animator>().SetTrigger("Click");
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<Animator>().SetTrigger("EndClick");
    }
}
