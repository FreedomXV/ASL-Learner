using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAppear : MonoBehaviour
{
    [SerializeField] GameObject nextPanel;
    [SerializeField] GameObject tick;

    public AudioSource audioSource;
    public AudioClip tickSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelPass()
    {
        StartCoroutine(LevelFinish());
    }

    IEnumerator LevelFinish()
    {
        tick.SetActive(true);
        audioSource.clip = tickSound;
        audioSource.Play();
        yield return new WaitForSeconds(1f);
        nextPanel.SetActive(true);
    }
}
