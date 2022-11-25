using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[DefaultExecutionOrder(-100)]
public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public SO_Interest[] interests;

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

    public void PerfectPairing(CatEntity catA, CatEntity catB)
    {
        int totalScore = 0;

        for (int i = 0; i < catA.lstInterests.Count; i++)
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

        totalScore += catA.extraScore;
        totalScore += catB.extraScore;

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

public static class Extensions
{
    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];

            int rand = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[rand];

            list[rand] = temp;
        }
    }

    public static void Shuffle<T>(this T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            T temp = array[i];

            int rand = UnityEngine.Random.Range(i, array.Length);
            array[i] = array[rand];

            array[rand] = temp;
        }
    }
}
