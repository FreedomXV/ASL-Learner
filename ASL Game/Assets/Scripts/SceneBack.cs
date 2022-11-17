using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneBack : MonoBehaviour
{

    public void BackScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        Time.timeScale = 1;
    }
}
