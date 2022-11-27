using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform target;

    private void Start()
    {
        transform.parent = null;
        transform.rotation = Quaternion.Euler(45, 45, 0);
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
    }
}
