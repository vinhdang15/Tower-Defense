using UnityEngine;
using TMPro;

public class GoldRefund : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldRefundText;
    public void UpdateGoldRefundText(string gold)
    {
        goldRefundText.text = gold;
    }
}
