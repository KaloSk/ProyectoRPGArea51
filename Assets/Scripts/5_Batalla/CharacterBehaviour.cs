using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour {


    float C_ZERO = 0f;

    private Vector2 initialPosition;
    private bool isInitialPosition = true;

    public List<AudioClip> characterSounds;
    private AudioSource audioSource;

    public int CharacterArrayID = -1;

	// Use this for initialization
	void Start () {
        initialPosition = transform.position;
        audioSource = new AudioSource();
	}
	
	// Update is called once per frame
	void Update () {
        if(!isInitialPosition){
            transform.GetComponent<Animator>().SetBool("Run", true);
            float distanceTarget = Vector2.Distance(transform.position, initialPosition);
            if (distanceTarget.CompareTo(C_ZERO) != 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, initialPosition, 10 * Time.deltaTime);
            }
            else
            {
                isInitialPosition = true;
                transform.GetComponent<Animator>().SetBool("Run", false);
                GameRol.ChangeTurnMethod();
            }
        }
	}



    public RTP GameRol;

    public void returnOriginalPosition(){
        isInitialPosition = false;
    }

    public void PlaySound(int i)
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(characterSounds[i]);
    }

    public void ChangePosition(int i){

    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
