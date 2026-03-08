using UnityEngine;
using UnityEngine.SceneManagement;


public class NextScene : MonoBehaviour
{
    public string nextSceneName = "NextScene";
    void OnMouseDown()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}