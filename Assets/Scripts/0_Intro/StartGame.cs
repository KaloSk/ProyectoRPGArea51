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

    [Header("Skills")]
    public List<Sprite> SkillSprite;

    [Header("Enemies")]
    public List<GameObject> EnemyAnimator;

    // Use this for initialization
    void Start () {

        var total = 0;

        game = new GameController();
        game.NewGame();

        for(var i = 0; i < game.GetAllCharactersList().Count; i++)
        {
            game.GetAllCharactersList()[i].Face = CharacterFaces[i];
            game.GetAllCharactersList()[i].Full = CharacterBody[i];
            game.GetAllCharactersList()[i].Sprite = CharacterAnimator[i].GetComponent<SpriteRenderer>().sprite;
            game.GetAllCharactersList()[i].Animator = CharacterAnimator[i].GetComponent<Animator>().runtimeAnimatorController;
            game.GetAllCharactersList()[i].Sounds = CharacterAnimator[i].GetComponent<CharacterBehaviour>().characterSounds;

            if (game.GetAllCharactersList()[i].Skills != null)
            {
                for (var x = 0; i < game.GetAllCharactersList()[i].Skills.Count; x++)
                {
                    game.GetAllCharactersList()[i].Skills[x].Icon = SkillSprite[total];
                    total++;
                }
            }
        }
        
        game.GetCharactersList().Add(game.GetAllCharactersList()[2]);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
