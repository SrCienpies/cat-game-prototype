using UnityEngine;

[CreateAssetMenu(fileName ="Interest Data", menuName ="Scriptable Object/Interest Data")]
public class SO_Interest : ScriptableObject
{
    public InterestGroup group;
    public InterestType type;
    public Sprite icon;
}
