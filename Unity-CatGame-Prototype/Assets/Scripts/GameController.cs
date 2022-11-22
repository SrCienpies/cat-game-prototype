using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public Text txtPairScore;
    public Text txtTotalScore;

    [Space(10)]
    public int totalMatches;
    public Image imgGoodBar;
    public Image imgEvilBar;

    private int totalScore;
    private float amountPerMatch;

    [Space(10)]
    [SerializeField] private Sprite bombEmpty;
    [SerializeField] private Sprite bombFilled;
    [SerializeField] private Image[] bombIcon;

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

        imgGoodBar.fillAmount = 0;
        imgEvilBar.fillAmount = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0);
    }

    public void PerfectPairing(CatEntitie catA, CatEntitie catB)
    {
        int totalScore = 0;

        for (int i = 0; i < catA.lstInterests.Length; i++)
        {
            int score = 0;

            SO_Interest intA = catA.lstInterests[i];
            SO_Interest intB = catB.lstInterests[i];

            if (intA.group == intB.group)
            {
                if(intA.type == intB.type)
                {
                    score = +2;
                }
                else
                {
                    score = -2;
                }
            }
            else
            {
                if (intA.type == intB.type)
                {
                    score = +1;
                }
                else
                {
                    score = -1; 
                }
            }

            totalScore += score;
            //if (catA.interest[i] == catB.interest[i])
            //{
            //    Debug.Log("Perfect Match");
            //    score = +2;
            //    totalScore += score;
            //    break;
            //}

            //string code = catA.interest[i] + catB.interest[i];

            //switch (code)
            //{
            //    case "A0B0":
            //        score = +1;
            //        break;
            //    case "A0C0":
            //        score = +1;
            //        break;
            //    case "A0A1":
            //        score = -2;
            //        break;
            //    case "A0B1":
            //        score = -1;
            //        break;
            //    case "A0C1":
            //        score = -1;
            //        break;
            //    case "B0A0":
            //        score = +1;
            //        break;
            //    case "B0C0":
            //        score = +1;
            //        break;
            //    case "B0A1":
            //        score = -1;
            //        break;
            //    case "B0B1":
            //        score = -2;
            //        break;
            //    case "B0C1":
            //        score = -1;
            //        break;
            //    case "C0A0":
            //        score = +1;
            //        break;
            //    case "C0B0":
            //        score = +1;
            //        break;
            //    case "C0A1":
            //        score = -1;
            //        break;
            //    case "C0B1":
            //        score = -1;
            //        break;
            //    case "C0C1":
            //        score = -2;
            //        break;
            //    case "A1A0":
            //        score = -2;
            //        break;
            //    case "A1B0":
            //        score = -1;
            //        break;
            //    case "A1C0":
            //        score = -1;
            //        break;
            //    case "A1B1":
            //        score = +1;
            //        break;
            //    case "A1C1":
            //        score = +1;
            //        break;
            //    case "B1A0":
            //        score = -1;
            //        break;
            //    case "B1B0":
            //        score = -2;
            //        break;
            //    case "B1C0":
            //        score = -1;
            //        break;
            //    case "B1A1":
            //        score = +1;
            //        break;
            //    case "B1C1":
            //        score = +1;
            //        break;
            //    case "C1A0":
            //        score = -1;
            //        break;
            //    case "C1B0":
            //        score = -1;
            //        break;
            //    case "C1C0":
            //        score = -2;
            //        break;
            //    case "C1A1":
            //        score = +1;
            //        break;
            //    case "C1B1":
            //        score = +1;
            //        break;
            //    default:
            //        break;
            //}

            //Debug.Log($"{code}: {score}");

            //totalScore += score;
        }

        if(totalScore > 0)
        {
            imgGoodBar.fillAmount += (1 / (float) totalMatches);
        }
        else if(totalScore < 0)
        {
            imgEvilBar.fillAmount += (1 / (float) totalMatches);
        }

        //===============================================

        this.totalScore += Mathf.Abs(totalScore);

        txtTotalScore.text = $"Total score: {this.totalScore}";
        txtPairScore.text = $"Perfect pair: {totalScore}";
    }
    public void UpdateBombPanel(int index, bool value)
    {
        if (value)
        {
            bombIcon[index].sprite = bombFilled;
        }
        else
        {
            bombIcon[index].sprite = bombEmpty;
        }
    }
}
