using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LovePuzzle : MonoBehaviour
{
    public Transform obj;
    public SO_Interest interest;
    public GameObject indicator;

    [Space(5)]
    public float timer;
    public float minDistance;
    public float ratioInteractable;
    public float ratioEffect;

    [Space(5)]
    public LayerMask layerInterac;
    public LayerMask layerEffect;

    [Space(5)]
    public SpriteRenderer sprAreaInteract;
    public SpriteRenderer sprAreaEffect;

    private bool onArea;
    private bool active;

    private void Awake()
    {
        GetComponent<SphereCollider>().radius = ratioInteractable / 2;

        sprAreaInteract.transform.localScale = Vector2.one * ratioInteractable;
        sprAreaEffect.transform.localScale = Vector2.one * ratioEffect;

        sprAreaEffect.gameObject.SetActive(false);
        indicator.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (onArea)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (active)
                {
                    active = false;
                    sprAreaEffect.gameObject.SetActive(false);

                    Collider[] cats = Physics.OverlapSphere(transform.position, ratioEffect / 2, layerEffect);

                    for (int i = 0; i < cats.Length; i++)
                    {
                        CatEntity cat = cats[i].GetComponent<CatEntity>();

                        cat.StartMovement();
                    }
                }
                else
                {
                    StartCoroutine("Counter");
                    active = true;
                    sprAreaEffect.gameObject.SetActive(true);
                }
            }
        }

        if (active)
        {
            Collider[] cats = Physics.OverlapSphere(transform.position, ratioEffect / 2, layerEffect);

            if (cats.Length > 0)
            {
                for (int i = 0; i < cats.Length; i++)
                {
                    CatEntity cat = cats[i].GetComponent<CatEntity>();

                    Vector3 direction = transform.position - cat.transform.position;
                    Vector3 nearPos = transform.position - direction.normalized * minDistance;

                    if (cat.lstInterests.Contains(interest))
                    {
                        Debug.DrawLine(transform.position, nearPos, Color.black);

                        cat.StopMovement();
                        cat.MoveToposition(nearPos);
                    }
                }
            }
        }
    }

    private IEnumerator Counter()
    {
        yield return new WaitForSeconds(timer);

        active = false;
        sprAreaEffect.gameObject.SetActive(false);

        Collider[] cats = Physics.OverlapSphere(transform.position, ratioEffect / 2, layerEffect);

        for (int i = 0; i < cats.Length; i++)
        {
            CatEntity cat = cats[i].GetComponent<CatEntity>();

            cat.StartMovement();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            indicator.gameObject.SetActive(true);
            onArea = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            indicator.gameObject.SetActive(false);
            onArea = false;
        }
    }
}
