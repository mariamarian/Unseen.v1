using UnityEngine;
using System.Collections;

public class ClickMessage : MonoBehaviour
{
    public GameObject messageObject;
    public float showTime = 2f;

    void OnMouseDown()
    {
        StartCoroutine(ShowMessage());
    }

    IEnumerator ShowMessage()
    {
        messageObject.SetActive(true);
        yield return new WaitForSeconds(showTime);
        messageObject.SetActive(false);
    }
}