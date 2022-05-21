using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "New Person", menuName = "SO/New Person")]
public class PersonSO : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] public Image image;
    [SerializeField] public string name;
    [SerializeField] public int age;
    [SerializeField][TextArea] public string description;
    [SerializeField][TextArea] public string howDied;
}
