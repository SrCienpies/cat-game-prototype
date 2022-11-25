using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewCustom : MonoBehaviour
{
    public float width;
    public float lenght;

    private Vector3[] points;

    public LineRenderer lineModel;
    public LineRenderer[] lines;

    public Transform tfPointsA;
    public Transform tfPointsB;
    public Transform ctrLines;

    private Transform[] pointsA;
    private Transform[] pointsB;

    private CatEntity cat;

    private List<bool> values = new List<bool>();


    private void OnEnable()
    {
        tfPointsB.localPosition = new Vector3(0, tfPointsB.position.y, lenght + 0.5f);
    }
    private void OnValidate()
    {
        tfPointsB.localPosition = new Vector3(0, tfPointsB.position.y, lenght + 0.5f);
        lineModel.SetPosition(1, tfPointsB.localPosition);
    }

    private void Awake()
    {
        pointsA = new Transform[tfPointsA.childCount];
        pointsB = new Transform[tfPointsB.childCount];

        for (int i = 0; i < tfPointsA.childCount; i++)
        {
            pointsA[i] = tfPointsA.GetChild(i);
            pointsB[i] = tfPointsB.GetChild(i);

            pointsA[i].name = $"Position {i}";
            pointsB[i].name = $"Position {i}";
        }

        lines = new LineRenderer[10];

        for (int i = 0; i < 10; i++)
        {
            GameObject objLine = new GameObject();

            objLine.transform.parent = ctrLines;

            lines[i] = objLine.AddComponent<LineRenderer>();
            lines[i].material = lineModel.material;
            lines[i].colorGradient = lineModel.colorGradient;
            lines[i].startWidth = 0.1f;

            values.Add(false);
        }


        lineModel.enabled = false;

        cat = GetComponent<CatEntity>();

        //lineRender.positionCount = 20;
    }

    

    void Update()
    {
        int definition = 10;
        float amtSegment = width / (float)definition;

        points = new Vector3[20];

        for (int i = 0; i < definition; i++)
        {
            values[i] = false;
        }

        for (int i = 0; i < definition; i++)
        {
            Vector3 start = pointsA[i].position;
            Vector3 end = pointsB[i].position;

            RaycastHit hitInfo;

            if (Physics.Linecast(start, end, out hitInfo))
            {
                end = hitInfo.point;

                if (hitInfo.transform.CompareTag("Player"))
                {
                    values[i] = true;
                }
                else
                {
                    values[i] = false;
                }
            }

            int indexA = i+i;
            int indexB = indexA + 1;

            points[indexA] = start; 
            points[indexB] = end; 

            lines[i].SetPosition(0, start);
            lines[i].SetPosition(1, end);

            Debug.DrawLine(start, end, Color.black);
        }

        if (values.Contains(true))
        {
            if (!cat.hasPlayer)
            {
                cat.TargetPlayer();
                cat.hasPlayer = true;
                Debug.Log("Player Targeted, Punishment: " + cat.extraScore);
            }
        }
        else
        {
            cat.hasPlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        //float zValue = (lenght / 2) + 0.5f;

        //Vector3 center = new Vector3(transform.position.x, transform.position.y, zValue + transform.position.z);
        //Vector3 size = new Vector3(width, 1, lenght);

        //Color c = Color.cyan;
        //c.a = 0.2f;
        //Gizmos.color = c;
        //Gizmos.DrawCube(center, size);

        //======================================0
        //Gizmos.color = Color.black;
        //Vector2 dir = transform.position - transform.forward;
        //Gizmos.DrawRay(transform.position, transform.forward * 2);
    }
}
