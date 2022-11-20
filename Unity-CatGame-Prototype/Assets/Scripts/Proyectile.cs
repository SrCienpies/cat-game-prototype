using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{ 
    [SerializeField] private float _initialVelocity;
    [SerializeField] private float _angle;
    [SerializeField] private float _lifeTime;

    [Space(5)]
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _targetPoint;

    [Space(10)]
    [SerializeField] private LineRenderer _line;
    [SerializeField] private float _step;

    private void Update()
    {
        //DrawPath(_initialVelocity, angle, _step);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(_targetPoint.position - _firePoint.position);

            Vector3 direction = _targetPoint.position - _firePoint.position;
            Vector3 groundDirection = new Vector3(direction.x, 0, direction.z);
            Vector3 targetPos = new Vector3(groundDirection.magnitude, direction.y, 0);
            //Vector3 targetPos = _targetPoint.position;

            float angle = _angle * Mathf.Deg2Rad;
            float v0;
            float time;

            CalculatePath(targetPos, angle,out v0,out time);

            StopAllCoroutines();
            StartCoroutine(RoutineMovement(targetPos.normalized, v0, angle, time));
        }
    }
    private void DrawPath(float v0, float angle, float step)
    {
        _step = Mathf.Max(0.01f, step);
        float totalTime = 10;

        _line.positionCount = (int)(totalTime / step) + 2;

        int count = 0;

        for (int i = 0; i < totalTime; i++)
        {
            float x = v0 * i * Mathf.Cos(angle);
            float y = v0 * i * Mathf.Sin(angle) - 0.5f * -Physics.gravity.y * Mathf.Pow(i, 2);

            _line.SetPosition(count, new Vector3(x, y, 0));
            count++;
        }

        float xFinal = v0 * totalTime * Mathf.Cos(angle);
        float yFinal = v0 * totalTime * Mathf.Sin(angle) - 0.5f * -Physics.gravity.y * Mathf.Pow(totalTime, 2);

        _line.SetPosition(count, new Vector3(xFinal, yFinal, 0));
    }

    private void CalculatePath(Vector3 targetPos, float angle, out float v0, out float time)
    {
        float xt = targetPos.x;
        float yt = targetPos.y;
        float g = -Physics.gravity.y;

        float v1 = Mathf.Pow(xt, 2) * g;
        float v2 = 2 * xt * Mathf.Sin(angle) * Mathf.Cos(angle);
        float v3 = 2 * yt * Mathf.Pow(Mathf.Cos(angle),2);

        v0 = Mathf.Sqrt(v1 / (v2 - v3));
        time = xt / (v0 * Mathf.Cos(angle));
    }

    IEnumerator RoutineMovement(Vector3 direction, float v0, float angle, float time)
    {
        Vector3 posOrigin = _firePoint.position;
        float t = 0;

        while (t < time)
        {
            float x = v0 * t * Mathf.Cos(angle);
            float y = v0 * t * Mathf.Cos(angle) - (1f / 2f) * -Physics.gravity.y * Mathf.Pow(t, 2);

            transform.position = posOrigin + direction * x + Vector3.up*y;

            t += Time.deltaTime;
            yield return null;
        }
    }
}
