using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TMP_Text progresText;
    [SerializeField]
    private Toggle toggle;

    private void Start()
    {
        int v = PlayerPrefs.GetInt("Tutorial");
        if(v == 0 && toggle != null)
        {
            toggle.isOn = true;
        }
        else if(v != 0 && toggle != null)
        {
            toggle.isOn = false;
        }
    }

    public void PlayGame(int index)
    {
        StartCoroutine(LoadAsyncronously(index));
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    IEnumerator LoadAsyncronously(int scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        while (!operation.isDone)
        {
            /*float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;
            int pog = Mathf.FloorToInt(progress * 100f);
            progresText.text = pog + "%";
            */
            yield return null;
        }
    }


    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void ChangeTutorial()
    {
        int value = 0;
        if (toggle.isOn == true)
        {
            value = 0;
        }
        else
        {
            value = 1;
        }

        PlayerPrefs.SetInt("Tutorial", value);
    }
}
