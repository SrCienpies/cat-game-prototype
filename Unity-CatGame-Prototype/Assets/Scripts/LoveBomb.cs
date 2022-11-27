using UnityEngine;

public class LoveBomb : MonoBehaviour
{
    public float bombRatio;
    public LayerMask bombLayer;

    private int catAmount;

    private CatEntity catA;
    private CatEntity catB;

    private void Update()
    {
        Collider[] matchs = Physics.OverlapSphere(transform.position, bombRatio / 2, bombLayer);

        if (matchs.Length > 1)
        {
            for (int i = 0; i < matchs.Length; i++)
            {
                CatEntity cat = matchs[i].gameObject.GetComponent<CatEntity>();

                if (cat.matchEnable)
                {
                    catAmount++;

                    if(catAmount == 1)
                    {
                        catA = cat;
                    }
                    if (catAmount == 2)
                    {
                        catB = cat;
                    }

                }

                if (catAmount == 2)
                {
                    GameController.Instance.PerfectPairing(catA, catB);
                    Destroy(gameObject);
                    break;
                }
            }


        }
    }
}
