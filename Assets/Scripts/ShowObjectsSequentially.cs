using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeObjectsSequentiallyWithScene : MonoBehaviour
{
    public GameObject[] objectsToShow; // cele 7 obiecte
    public float delay = 5f;           // delay normal între obiecte
    public float fadeDuration = 1f;    // durata fade-ului
    public string nextSceneName = "NextScene"; // numele scenei următoare

    void Start()
    {
        // setează toate obiectele cu alpha 0
        for (int i = 0; i < objectsToShow.Length; i++)
        {
            objectsToShow[i].SetActive(true); // trebuie activ pentru fade
            if (i != 0)
                SetAlpha(objectsToShow[i], 0f); // toate cu alpha 0, exceptând primul
        }

        StartCoroutine(ShowObjectsCoroutine());
    }

    IEnumerator ShowObjectsCoroutine()
    {
        for (int i = 0; i < objectsToShow.Length; i++)
        {
            if (i == 0)
            {
                // primul obiect apare instant
                SetAlpha(objectsToShow[i], 1f);
            }
            else
            {
                // fade in pentru restul
                yield return StartCoroutine(FadeIn(objectsToShow[i]));
            }

            // delay după fade sau afișare
            if (i == 2) // al 3-lea obiect
            {
                yield return new WaitForSeconds(1f); // doar 1 sec
            }
            else
            {
                yield return new WaitForSeconds(delay); // restul 5 sec
            }
        }

        // după ultimul obiect, așteaptă 5 sec și trece la scena următoare
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator FadeIn(GameObject obj)
    {
        float t = 0f;
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

        while (t < fadeDuration)
        {
            float alpha = t / fadeDuration;
            foreach (Renderer rend in renderers)
            {
                Color c = rend.material.color;
                c.a = alpha;
                rend.material.color = c;
            }
            t += Time.deltaTime;
            yield return null;
        }

        foreach (Renderer rend in renderers)
        {
            Color c = rend.material.color;
            c.a = 1f;
            rend.material.color = c;
        }
    }

    void SetAlpha(GameObject obj, float alpha)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in renderers)
        {
            Color c = rend.material.color;
            c.a = alpha;
            rend.material.color = c;
        }
    }
}