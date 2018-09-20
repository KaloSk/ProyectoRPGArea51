using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGameGroup : MonoBehaviour {

    //Control Whole Parameters!!//
    GameController game;
    AudioSource audioSource;

    public InputField FieldName;
    public GameObject WarningImage;

    [Header("SoundsMenu")]
    public List<AudioClip> soundList;

    public void Start()
    {
        audioSource = new AudioSource();
        game = new GameController();
    }

    public void SaveGroupName(){        
        if (string.IsNullOrEmpty(FieldName.text)){
            PlaySound(1);
            WarningImage.SetActive (true);
        }
        else
        {
            PlaySound(0);
            game.SetGroupName(FieldName.text);
            SceneManager.LoadScene(GameConstants.SCENE_LOBBY);
        }
    }

    public void PlaySound(int i)
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(soundList[i]);
    }
}
