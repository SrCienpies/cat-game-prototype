using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform target;

    private void Start()
    {
        transform.parent = null;
    }

    void Update()
    {
        transform.position = target.position;
    }
}
