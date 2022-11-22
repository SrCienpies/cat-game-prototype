using System;
using System.Collections;
using UnityEngine;

public class CatEntitie : MonoBehaviour
{
    public SO_Interest[] lstInterests;
    public SpriteRenderer[] sprInterest;

    [Space(10)]
    public bool loop;
    public float speed;
    public float rate;
    public Transform waypoints;

    private Vector3[] waypointPositions;
    private int idxWaypoint = 0;
    private bool isMoving;

    private void Start()
    {
        waypointPositions = new Vector3[waypoints.childCount];

        for (int i = 0; i < waypoints.childCount; i++)
        {
            Transform waypoint = waypoints.GetChild(i);

            waypointPositions[i] = waypoint.position;

            //waypoint.parent = null;
            //waypoint.gameObject.SetActive(false);
        }

        //transform.position = waypointPositions[idxWaypoint];
        isMoving = true;
        StartCoroutine(RoutineMove());

        for (int i = 0; i < lstInterests.Length; i++)
        {
            sprInterest[i].sprite = lstInterests[i].icon;
        }
    }

    private void Update()
    {
        //Move();
    }
    private void Move()
    {
        if (waypoints.childCount == 0) return;

        transform.forward = waypointPositions[idxWaypoint] - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, waypointPositions[idxWaypoint], speed * Time.deltaTime);

        if (transform.position == waypointPositions[idxWaypoint]) idxWaypoint++;

        if(idxWaypoint == waypointPositions.Length)
        {
            idxWaypoint = 0;
        }
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

    private void OnDrawGizmos()
    {
        if (Application.isPlaying) return;

        if (waypoints.childCount > 0 && waypoints != null)
        {
            for (int i = 0; i < waypoints.childCount - 1; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(waypoints.GetChild(i).position, waypoints.GetChild(i + 1).position);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {

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