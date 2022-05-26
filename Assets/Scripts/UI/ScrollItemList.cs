using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollItemList : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemField;

    public void SetItemText(string fieldText)
    {
        itemField.text = fieldText;
    }
}
