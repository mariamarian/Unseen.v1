using UnityEngine;

public class OpenFrame : MonoBehaviour
{
    public GameObject gameObject;

    void OnMouseDown()
    {
        gameObject.SetActive(true);
    }
}