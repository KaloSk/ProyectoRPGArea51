using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour {

    AudioSource audioSource;

    public int Index;
    public AudioClip Sound;
    public GameObject Fade;

    // Use this for initialization
    void Start()
    {
        audioSource = new AudioSource();
    }

    void OnMouseDown()
    {
        StartCoroutine(DoAction());
    }

    IEnumerator DoAction()
    {
        PlaySound();
        if (Fade != null) Fade.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(Index);
    }

    public void PlaySound()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(Sound);
    }
}
