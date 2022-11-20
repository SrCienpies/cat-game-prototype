using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public string[] interest;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void PerfectPairing(CatEntitie catA, CatEntitie catB)
    {
        int totalScore = 0;

        for (int i = 0; i < catA.interest.Length; i++)
        {
            int score = 0;

            if (catA.interest[i] == catB.interest[i])
            {
                Debug.Log("Perfect Match");
                score = +2;
                totalScore += score;
                break;
            }

            string code = catA.interest[i] + catB.interest[i];

            switch (code)
            {
                case "A0B0":
                    score = +1;
                    break;
                case "A0C0":
                    score = +1;
                    break;
                case "A0A1":
                    score = -2;
                    break;
                case "A0B1":
                    score = -1;
                    break;
                case "A0C1":
                    score = -1;
                    break;
                case "B0A0":
                    score = +1;
                    break;
                case "B0C0":
                    score = +1;
                    break;
                case "B0A1":
                    score = -1;
                    break;
                case "B0B1":
                    score = -21;
                    break;
                case "B0C1":
                    score = -1;
                    break;
                case "C0A0":
                    score = +1;
                    break;
                case "C0B0":
                    score = +1;
                    break;
                case "C0A1":
                    score = -1;
                    break;
                case "C0B1":
                    score = -1;
                    break;
                case "C0C1":
                    score = -2;
                    break;
                case "A1A0":
                    score = -2;
                    break;
                case "A1B0":
                    score = -1;
                    break;
                case "A1C0":
                    score = -1;
                    break;
                case "A1B1":
                    score = +1;
                    break;
                case "A1C1":
                    score = +1;
                    break;
                case "B1A0":
                    score = -1;
                    break;
                case "B1B0":
                    score = -2;
                    break;
                case "B1C0":
                    score = -1;
                    break;
                case "B1A1":
                    score = +1;
                    break;
                case "B1C1":
                    score = +1;
                    break;
                case "C1A0":
                    score = -1;
                    break;
                case "C1B0":
                    score = -1;
                    break;
                case "C1C0":
                    score = -2;
                    break;
                case "C1A1":
                    score = +1;
                    break;
                case "C1B1":
                    score = +1;
                    break;
                default:
                    break;
            }

            totalScore += score;
        }

        Debug.Log($"TOTAL POINTS: {totalScore}");
    }
}
