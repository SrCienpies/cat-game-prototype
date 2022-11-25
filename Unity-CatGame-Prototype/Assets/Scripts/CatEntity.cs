using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatEntity : MonoBehaviour
{
    public List<SO_Interest> lstInterests;
    public SpriteRenderer[] sprInterest;

    [Space(10)]
    public bool loop;
    public float speed;
    public float rate;
    public Transform waypoints;

    private Vector3[] waypointPositions;
    private int idxWaypoint = 0;
    private bool isMoving;

    public bool hasPlayer;
    public int extraScore;
    private void Start()
    {
        Debug.Log(name);
        waypointPositions = new Vector3[waypoints.childCount];

        for (int i = 0; i < waypoints.childCount; i++)
        {
            Transform waypoint = waypoints.GetChild(i);

            waypointPositions[i] = waypoint.position;
        }

        isMoving = true;
        StartCoroutine("RoutineMove");

        for (int i = 0; i < lstInterests.Count; i++)
        {
            sprInterest[i].sprite = lstInterests[i].icon;
        }
    }

    public void MoveToposition(Vector3 toPosition)
    {
        float distance = Vector3.Distance(transform.position, toPosition);

        //if(toPosition - transform.position != Vector3.zero) transform.forward = toPosition - transform.position;

        transform.position = Vector3.MoveTowards(transform.position, toPosition, speed * Time.deltaTime);

        //if(toPosition - transform.position != Vector3.zero)
        //{
        //    if(Vector3.Distance(transform.position, toPosition))
        //}
    }
    private IEnumerator RoutineMove()
    {
        if (waypoints.childCount == 0) yield break;

        while (isMoving)
        {
            transform.forward = waypointPositions[idxWaypoint] - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, waypointPositions[idxWaypoint], speed * Time.deltaTime);

            if (transform.position == waypointPositions[idxWaypoint])
            {
                idxWaypoint ++;
                yield return new WaitForSeconds(rate);
            }

            if (idxWaypoint == waypointPositions.Length)
            {
                if (loop)
                {
                    System.Array.Reverse(waypointPositions);
                    idxWaypoint = 1;
                }
                else
                {
                    idxWaypoint = 0;
                }
            }

            yield return null;
        }
    }

    public void StartMovement()
    {
        if (isMoving) return;

        isMoving = true;
        StartCoroutine("RoutineMove");
    }
    public void StopMovement()
    {
        if (!isMoving) return;

        isMoving = false;
        StopCoroutine("RoutineMove");
    }
    public void TargetPlayer()
    {
        extraScore--;

        SO_Interest[] aux = GameController.Instance.interests;

        aux.Shuffle();

        lstInterests = new List<SO_Interest>();

        lstInterests.Add(aux[0]);
        lstInterests.Add(aux[1]);
        lstInterests.Add(aux[2]);

        for (int i = 0; i < lstInterests.Count; i++)
        {
            sprInterest[i].sprite = lstInterests[i].icon;
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying) return;

        if (waypoints != null)
        {
            for (int i = 0; i < waypoints.childCount - 1; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(waypoints.GetChild(i).position, waypoints.GetChild(i + 1).position);
            }
        }
    }
}

public enum InterestGroup
{
    A = 0,
    B = 1,
    C = 2,
    D = 3,
    E = 4,
    F = 5
}
public enum InterestType
{
    Type_A,
    Type_B
}