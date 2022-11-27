using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Introduction : MonoBehaviour
{
    public Image fade;

    private void Awake()
    {
        fade.gameObject.SetActive(false);
    }
    public void LoadGameScene()
    {
        fade.gameObject.SetActive(true);
        fade.DOFade(1, 0.5f).OnComplete(() =>
        {
            StartCoroutine("LoadYourAsyncScene");
        });
    }

    public IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Gameplay");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
