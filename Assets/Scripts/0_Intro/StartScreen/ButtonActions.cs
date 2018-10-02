using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        transform.GetComponent<Button>().onClick.AddListener(OnMouseDown);
    }

    public void OnMouseDown()
    {
        StartCoroutine(GoToAnother());
    }

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
