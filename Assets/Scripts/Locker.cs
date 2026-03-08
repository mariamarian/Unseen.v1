using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Locker : MonoBehaviour
{
    public string code = "4287";
    private string currentInput = "";

    public Transform[] digitCubes;  
    public TextMeshPro[] digitTexts;
    public GameObject nextCube;    

    void Start()
    {
        nextCube.SetActive(false); 
    }

    void Update()
    {
        // cifre 0-9
        for (KeyCode k = KeyCode.Alpha0; k <= KeyCode.Alpha9; k++)
        {
            if (Input.GetKeyDown(k))
            {
                if (currentInput.Length < 4)
                {
                    currentInput += (k - KeyCode.Alpha0).ToString();
                    UpdateCubes();
                }
            }
        }

        // backspace
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (currentInput.Length > 0)
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
                UpdateCubes();
            }
        }

        // enter
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CheckCode();
        }

        // click pe cubul Next
        if (nextCube.activeSelf && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == nextCube)
                {
                    SceneManager.LoadScene("NextScene"); 
                }
            }
        }
    }

    void UpdateCubes()
    {
        for (int i = 0; i < digitCubes.Length; i++)
        {
            Renderer rend = digitCubes[i].GetComponent<Renderer>();
            if (i < currentInput.Length)
            {
                rend.material.color = Color.white;
                digitTexts[i].text = currentInput[i].ToString();
            }
            else
            {
                rend.material.color = Color.gray;
                digitTexts[i].text = "";
            }
        }
    }

    void CheckCode()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i >= currentInput.Length) break;
            Renderer rend = digitCubes[i].GetComponent<Renderer>();

            if (currentInput[i] == code[i])
                rend.material.color = Color.green;
            else if (code.Contains(currentInput[i].ToString()))
                rend.material.color = Color.yellow;
            else
                rend.material.color = Color.gray;
        }

        if (currentInput == code)
        {
            Debug.Log("Locker opened!");
            nextCube.SetActive(true); 
        }
        else
        {
            currentInput = "";
            foreach (var t in digitTexts)
                t.text = "";
        }
    }
}