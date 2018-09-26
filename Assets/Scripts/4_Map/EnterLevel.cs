using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLevel : MonoBehaviour {

    AudioSource audioSource;

    public int indice;
    public AudioClip Sound;


	// Use this for initialization
	void Start () {
        audioSource = new AudioSource();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseDown(){
        PlaySound();
        SceneManager.LoadScene(indice);
    }
    
    public void PlaySound()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(Sound);
    }
}
