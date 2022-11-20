using UnityEngine;
using DG.Tweening;

public class PlayerProyectile : MonoBehaviour
{
    [SerializeField] private float height;
    [SerializeField] private float duration;
    [SerializeField] private Transform _posOrigin;
    [SerializeField] private Transform _posTarget;

    [Space(10)]
    [SerializeField] private Transform pointer;
    [SerializeField] private Transform bomb;

    private Tween twShoot;
    private bool isAiming;


    private void Awake()
    {
        pointer.gameObject.SetActive(false);
        bomb.gameObject.SetActive(false);
        isAiming = false;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) Aim();
        if (Input.GetKeyUp(KeyCode.L)) Shoot();
    }
    public void Aim()
    {
        isAiming = true;
    }
    public void Shoot()
    {
        if (!isAiming) return;

        Vector3 posOrigin = _posOrigin.position;
        Vector3 posTarget = _posTarget.position;

        transform.position = posOrigin;

        twShoot.Kill();
        twShoot = transform
            .DOJump(posTarget, height, 1, duration)
            .SetEase(Ease.Linear)
            .OnComplete(OnCompleteShoot);

        void OnCompleteShoot()
        {
            Debug.Log("Evaluate Results");
        }
    }
}
