using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

    //Control Whole Parameters!!//
    GameController game;

    [Header("Characters")]
    public List<Sprite> CharacterFaces;
    public List<Sprite> CharacterBody;
    public List<GameObject> CharacterAnimator;

    [Header("Enemies")]
    public List<GameObject> EnemyAnimator;

    // Use this for initialization
    void Start () {

        game = new GameController();
        game.NewGame();

        game.GetCharactersList()[0].Face = CharacterFaces[0];
        game.GetCharactersList()[0].Full = CharacterBody[0];
        game.GetCharactersList()[0].Sprite = CharacterAnimator[0].GetComponent<SpriteRenderer>().sprite;
        game.GetCharactersList()[0].Animator = CharacterAnimator[0].GetComponent<Animator>().runtimeAnimatorController;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
