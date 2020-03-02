using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneFader : MonoBehaviour {

    public Image fadeImage;
    public AnimationCurve fadeCurve;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeToScene (int sceneIndex)
    {
        StartCoroutine(FadeOutScene(sceneIndex));
    }

    public void CrossFadeObjects(GameObject objectToFadeOut, GameObject objectToFadeIn)
    {
        StartCoroutine(CrossFadeGameObjects(objectToFadeOut, objectToFadeIn));
    }

    IEnumerator FadeIn()
    {
        float time = 1f;

        while (time > 0f)
        {
            time -= Time.deltaTime;
            float alpha = fadeCurve.Evaluate(time);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return 0;
        }
    }

    IEnumerator FadeOutScene(int sceneIndex)
    {
        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime;
            float alpha = fadeCurve.Evaluate(time);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return 0;
        }

        SceneManager.LoadScene(sceneIndex);

    }

    IEnumerator CrossFadeGameObjects(GameObject objectToFadeOut, GameObject objectToFadeIn)
    {
        float timeOut = 1f;
        float timeIn = 0f;

        objectToFadeOut.GetComponent<GraphicRaycaster>().enabled = false;
        objectToFadeIn.GetComponent<GraphicRaycaster>().enabled = true;

        while (timeOut > 0f && timeIn < 1f)
        {
            timeOut -= Time.deltaTime;
            timeIn += Time.deltaTime;
            float alphaOut = fadeCurve.Evaluate(timeOut);
            float alphaIn = fadeCurve.Evaluate(timeIn);
            objectToFadeOut.GetComponent<CanvasGroup>().alpha = timeOut;
            objectToFadeIn.GetComponent<CanvasGroup>().alpha = timeIn;
            yield return 0;
        }

    }
}
