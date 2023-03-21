using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemView : MonoBehaviour
{
    protected int Index;

    [SerializeField] protected Image Icon;
    [SerializeField] protected TMP_Text Name;
    [SerializeField] protected TMP_Text Improvement;
    [SerializeField] protected TMP_Text Price;
    [SerializeField] protected Button SellButton;
    [SerializeField] protected GameObject ClosePanel;
    [SerializeField] protected GameObject LockPanel;

    protected int PriceValue;
    protected int ImprovementValue;
}
