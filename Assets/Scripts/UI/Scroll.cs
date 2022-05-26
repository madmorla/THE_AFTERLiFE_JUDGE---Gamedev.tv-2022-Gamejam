using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    [SerializeField] private SoulDataSO soulData;

    [SerializeField] private GameObject listItemUIPrefab;

    [Header("UI Fields")]
    [SerializeField] private Image portrait;
    [SerializeField] private TextMeshProUGUI fullName;
    [SerializeField] private TextMeshProUGUI diedAtAge;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private RectTransform righteousActsList;
    [SerializeField] private RectTransform sinfulActsList;
    [SerializeField] private RectTransform familyMembersList;

    public void SetSoulData(SoulDataSO soulDataSO)
	{
        this.soulData = soulDataSO;

        fullName.text = soulData.fullname;
        diedAtAge.text = soulData.diedAtAge.ToString();
        description.text = soulData.description;
        SetRighteousActsList(soulData.righteousActs);
        SetSinfulActsList(soulData.sinfulActs);
        SetFamilyMembersList(soulData.familyMembers);
    }

    public void SetRighteousActsList(IEnumerable<string> itemList)
	{
        DeleteItemListUI(righteousActsList);
        SetItemList(itemList, righteousActsList);
    }

    public void SetSinfulActsList(IEnumerable<string> itemList)
    {
        DeleteItemListUI(sinfulActsList);
        SetItemList(itemList, sinfulActsList);
    }

    public void SetFamilyMembersList(IEnumerable<string> itemList)
    {
        DeleteItemListUI(familyMembersList);
        SetItemList(itemList, familyMembersList);
    }

    private void SetItemList(IEnumerable<string> itemList, RectTransform container)
	{
		foreach(string item in itemList)
		{
            GameObject itemObj = Instantiate(listItemUIPrefab, container);
            itemObj.GetComponent<ScrollItemList>().SetItemText(item);
		}
	}

    private void DeleteItemListUI(RectTransform container)
	{
        foreach(Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }
}
