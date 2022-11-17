using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneToStart : MonoBehaviour
{

    public void StartScene()
    {
        SceneManager.LoadScene("START");

        Time.timeScale = 1;
    }
}
