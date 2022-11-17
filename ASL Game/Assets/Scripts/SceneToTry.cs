using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneToTry : MonoBehaviour
{

    public void TryScene()
    {
        SceneManager.LoadScene("TRY");

        Time.timeScale = 1;
    }
}
