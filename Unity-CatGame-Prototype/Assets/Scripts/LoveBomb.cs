using UnityEngine;

public class LoveBomb : MonoBehaviour
{
    public float bombRatio;
    public LayerMask bombLayer;

    private void Update()
    {
        Collider[] matchs = Physics.OverlapSphere(transform.position, bombRatio / 2, bombLayer);

        if (matchs.Length > 1)
        {
            CatEntity catA = matchs[0].gameObject.GetComponent<CatEntity>();
            CatEntity catB = matchs[1].gameObject.GetComponent<CatEntity>();

            GameController.Instance.PerfectPairing(catA, catB);

            Destroy(gameObject);
        }
    }
}
