using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "New Soul Data", menuName = "SO/New Soul Data")]
public class SoulDataSO : ScriptableObject
{
	[Header("Info")]
	public Image image;
	public string fullname;
	public int age;
	[TextArea] public string description;

	public string[] righteousActs;
	public string[] sinfulActs;

	public string[] familyMembers;
}
