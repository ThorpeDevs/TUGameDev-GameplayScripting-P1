using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelButtonHandler : MonoBehaviour
{
    private Button bt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bt = gameObject.GetComponent<Button>();
        bt.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("clicked");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
