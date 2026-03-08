using UnityEngine;
using System.Collections;

public class AutoClose : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Close());
    }

    IEnumerator Close()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}