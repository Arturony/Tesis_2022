using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wait : MonoBehaviour
{
    public float duration;

    public float waitDuration;

    public RectTransform objectToAnim;

    private void Start()
    {
        FadeIn();
        StartCoroutine(WaitForSegs());
    }

    public void FadeIn()
    {
        LeanTween.alpha(objectToAnim, 0f, duration);
    }

    public void FadeOut()
    {
        LeanTween.alpha(objectToAnim, 1f, duration);
    }

    private IEnumerator WaitForSegs()
    {
        yield return new WaitForSeconds(waitDuration);
        FadeOut();
        yield return new WaitForSeconds(1);

        SceneManager.LoadSceneAsync(1);
    }
}
