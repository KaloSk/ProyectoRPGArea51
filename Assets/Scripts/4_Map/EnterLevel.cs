using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterLevel : MonoBehaviour {

    AudioSource audioSource;

    public int Number;
    public bool IsRecuit;
    public bool IsBoss;
    public bool HasStory;
    public string Story;
    public AudioClip Sound;

    GameController gc = new GameController();

	// Use this for initialization
	void Start () {
        
        if (gc.GetAllCharactersList() == null)
        {
            SceneManager.LoadScene(GameConstants.SCENE_INTRO);
        }

        if (gc.GetLastLevel() < Number)
        {
            if (IsBoss) transform.GetComponent<Animator>().runtimeAnimatorController = null;

            transform.GetComponent<CircleCollider2D>().enabled = false;
            transform.GetComponent<SpriteRenderer>().color = Color.black;
        }
        else if(gc.GetLastLevel() == Number)
        {
            transform.GetComponent<CircleCollider2D>().enabled = true;
            transform.Find("Label").GetComponent<Transform>().gameObject.SetActive(true);
        }
        else
        {
            transform.GetComponent<CircleCollider2D>().enabled = true;
        }
        audioSource = new AudioSource();

    }
	
    void OnMouseDown(){
        PlaySound();
        gc.SetOpenLevel(Number);
        SceneManager.LoadScene(GameConstants.SCENE_BATTLE);        
    }
    
    public void PlaySound()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(Sound);
    }
}
