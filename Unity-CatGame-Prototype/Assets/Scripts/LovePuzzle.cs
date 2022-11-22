using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LovePuzzle : MonoBehaviour
{
    public float bombRatio;
    public LayerMask bombLayer;
    public SpriteRenderer sprArea;

    private void Update()
    {
        Collider[] matchs = Physics.OverlapSphere(transform.position, bombRatio / 2, bombLayer);

        if (matchs.Length > 1)
        {
            CatEntitie catA = matchs[0].gameObject.GetComponent<CatEntitie>();
            CatEntitie catB = matchs[1].gameObject.GetComponent<CatEntitie>();

            GameController.Instance.PerfectPairing(catA, catB);

            Destroy(gameObject);
        }
    }

    public void LoveCall()
    {
        sprArea.gameObject.SetActive(true);
    }
}
