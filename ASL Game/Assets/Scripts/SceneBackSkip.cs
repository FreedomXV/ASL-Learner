using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneBackSkip : MonoBehaviour
{

    public void BackSkipScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);

        Time.timeScale = 1;
    }
}
