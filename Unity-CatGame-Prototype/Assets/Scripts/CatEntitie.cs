using UnityEngine;

public class CatEntitie : MonoBehaviour
{
    public Interest[] lstInterests;
    public string[] interest;
}

[System.Serializable]
public class Interest
{
    public InterestType interest;
    public bool like;
    public Sprite icon;
}

public enum InterestType
{
    A = 0,
    B = 1,
    C = 2
}