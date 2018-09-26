using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    AudioSource audioSource;

    public static int ultimaEscena;
    public int indice;
    public GameObject fade;
    public AudioClip buttonSound;

    public void Start()
    {
        audioSource = new AudioSource();
    }

    public void OnPlay()
    {
        //ultimaEscena = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(GoToAnother());
    }
    /*
    public void ReturnToLast ()
    {
        
        SceneManager.LoadScene(ultimaEscena);
    }
    */
    IEnumerator GoToAnother()
    {
        PlaySound();
        if (fade != null) fade.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(indice);
    }

    public void PlaySound()
    {
        if (buttonSound != null)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(buttonSound);
        }        
    }

}
