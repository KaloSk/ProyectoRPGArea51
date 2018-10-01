using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryBattle : MonoBehaviour {

    GameController gc = new GameController();

    AudioSource audioSource;
    public AudioClip Sound;

    void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(OnMouseDown);
    }

    void OnMouseDown()
    {
        PlaySound();
        SceneManager.LoadScene(GameConstants.SCENE_MAP);
        var o = gc.GetOpenLevel();

        Debug.Log("CURRENT LEVEL" + o);

        if(o == gc.GetLastLevel())
        {
            if (gc.GetLastLevel().Equals(3)) //PHANTON DAGER//
            {
                gc.GetCharactersList().Add(gc.GetAllCharactersList()[2]);
                gc.GetCurrentCharactersList().Add(3);
            }

            gc.IncreaseLevel();
            Debug.Log("LAST LEVEL" + gc.GetLastLevel());
        }
    }

    public void PlaySound()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(Sound);
    }

}
