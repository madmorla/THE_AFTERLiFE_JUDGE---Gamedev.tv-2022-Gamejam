using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "New Person", menuName = "SO/New Person")]
public class PersonSO : ScriptableObject
{
    [Header("Info")]
    [SerializeField] public Image image;
    [SerializeField] public string fullname;
    [SerializeField] public int age;
    [SerializeField][TextArea] public string description;
    [SerializeField][TextArea] public string howDied;

    [SerializeField] public string[] righteousActs;
    [SerializeField] public string[] sinfulActs;

    [SerializeField] public string[] familyMembers;





}
