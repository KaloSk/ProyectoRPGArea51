using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

    //Control Whole Parameters!!//
    GameController game;

    [Header("Characters")]
    public List<Sprite> CharacterFaces;
    public List<Sprite> CharacterBody;

    // Use this for initialization
    void Start () {

        game = new GameController();
        game.NewGame();

        game.GetCharactersList()[0].Face = CharacterFaces[0];
        game.GetCharactersList()[0].Full = CharacterBody[0];

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
