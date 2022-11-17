using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSkip : MonoBehaviour
{

    public void SkipScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

        Time.timeScale = 1;
    }
}
