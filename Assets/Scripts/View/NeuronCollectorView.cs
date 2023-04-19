using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
using System;

public class NeuronCollectorView : MonoBehaviour
{
    [SerializeField] private TMP_Text _neuronCount;

    private float _normalScale;

    public TMP_Text NeuronCount => _neuronCount;

    public event Action TryGetNeuronCount;

    private void Start()
    {
        _normalScale = transform.localScale.x;
        TryGetNeuronCount?.Invoke();
    }

    public void ChangeNeuronView(int count)
    {
        _neuronCount.text = $"{FormatNumberExtension.FormatNumber(count)}";
        StartCoroutine(ChangeNeuronTextScale());
    }

    private IEnumerator ChangeNeuronTextScale()
    {
        transform.DOScale(_normalScale + 0.2f, 0.5f);
        yield return new WaitForSeconds(0.1f);
        transform.DOScale(_normalScale, 0.5f);
    }

    public bool NeuronIsCollected(int count)
    {
        return Convert.ToInt32(_neuronCount.text) >= count;
    }
}
