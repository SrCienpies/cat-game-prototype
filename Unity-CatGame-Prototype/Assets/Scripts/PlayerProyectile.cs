using UnityEngine;
using DG.Tweening;

public class PlayerProyectile : MonoBehaviour
{
    [SerializeField] private float height;
    [SerializeField] private float duration;
    [SerializeField] private Transform _posOrigin;
    [SerializeField] private Transform _posTarget;

    private Tween twShoot;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Shoot();
    }

    public void Shoot()
    {
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
