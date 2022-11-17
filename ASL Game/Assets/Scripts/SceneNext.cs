using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneNext : MonoBehaviour
{
    //public AudioSource audioSource;
    //public AudioClip clickSound;

    public void NextScene()
    {
        //audioSource.clip = clickSound;
        //audioSource.Play();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        Time.timeScale = 1;
    }
}
