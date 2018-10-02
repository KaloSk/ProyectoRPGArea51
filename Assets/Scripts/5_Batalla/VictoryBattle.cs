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


        foreach (var ch in gc.GetAllCharactersList())
        {
            ch.Level++;
            ch.Stats.HP += ch.StatsByLevel.HP;
            ch.Stats.MP += ch.StatsByLevel.MP;
            ch.Stats.ATK += ch.StatsByLevel.ATK;
            ch.Stats.DEF += ch.StatsByLevel.DEF;
            ch.Stats.MAG += ch.StatsByLevel.MAG;
            ch.Stats.MDF += ch.StatsByLevel.MDF;
            ch.Stats.SPE += ch.StatsByLevel.SPE;
        }

        if (o == gc.GetLastLevel())
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
