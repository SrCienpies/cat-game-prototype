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

    [Space(5)]
    public FieldOfViewCustom fov;
    public TextMesh txtPunishment;

    [Space(5)]
    public GameObject popInterests;
    public ParticleSystem partLove;
    public ParticleSystem partToxic;

    private IEnumerator _routineMoveTo;
    private bool isMovingTo;

    [HideInInspector] public bool matchEnable;
    private void Start()
    {
        partLove.gameObject.SetActive(false);
        partToxic.gameObject.SetActive(false);

        matchEnable = true;

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

        Vector3 targetPos = new Vector3(toPosition.x, 0, toPosition.z);

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime); 

        if (toPosition - transform.position != Vector3.zero) transform.forward = targetPos - transform.position;
    }
    private IEnumerator RoutineMove()
    {
        if (waypoints.childCount == 0) yield break;
         
        while (isMoving)
        {
            if(waypointPositions[idxWaypoint] - transform.position != Vector3.zero) transform.forward = waypointPositions[idxWaypoint] - transform.position;

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
    public IEnumerator RoutineNoveTo(Vector3 toPosition)
    {
        while(true)
        {
            Vector3 targetPos = new Vector3(toPosition.x, 0, toPosition.z);

            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            if (toPosition - transform.position != Vector3.zero) transform.forward = targetPos - transform.position;

            yield return new WaitForEndOfFrame();
        }


    }
    public void StartMoveTo(Vector3 position)
    {
        if (isMovingTo) return;

        isMovingTo = true;
        _routineMoveTo = RoutineNoveTo(position);
        StartCoroutine(_routineMoveTo);
    }
    public void StopMoveTo()
    {
        isMovingTo = false;
        StopCoroutine(_routineMoveTo);
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

        txtPunishment.transform.parent.gameObject.SetActive(true);
        txtPunishment.text = extraScore.ToString();

        GameController.Instance.ShowWarningPanel();
    }
    public void LookAt(Transform position)
    {
        transform.forward = position.position - transform.position;
    }

    public void Match(bool toxic)
    {
        StopMovement();
        matchEnable = false;
        fov.DisableFieldOfView();
        popInterests.SetActive(false);

        popInterests.gameObject.SetActive(false);

        if (toxic)
        {
            partToxic.gameObject.SetActive(true);
        }
        else
        {
            partLove.gameObject.SetActive(true);
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
    Food = 0,
    Activity = 1,
    Entertainment = 2,
}
public enum InterestType
{
    Type_A,
    Type_B
}