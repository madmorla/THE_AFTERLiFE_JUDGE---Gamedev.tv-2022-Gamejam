using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "New Soul Data", menuName = "SO/New Soul Data")]
public class SoulDataSO : ScriptableObject
{
	[Header("Info")]
	public Sprite image;
	public string fullname;
	public int diedAtAge;
	[Space]
	[TextArea] public string description;
	[Space]
	public string[] righteousActs;
	[Space]
	public string[] sinfulActs;
	[Space]
	public string[] familyMembers;
}
