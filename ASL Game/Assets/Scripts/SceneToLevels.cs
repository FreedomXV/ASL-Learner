using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneToLevels : MonoBehaviour
{

    public void LevelScene()
    {
        SceneManager.LoadScene("LEVELS");

        Time.timeScale = 1;
    }
}
