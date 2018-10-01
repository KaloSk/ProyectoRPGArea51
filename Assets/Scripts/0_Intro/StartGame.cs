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

    [Header("Items")]
    public List<Sprite> ItemSprites;

    // Use this for initialization
    void Start()
    {

        var total = 0;

        game = new GameController();
        game.NewGame();

        for (var i = 0; i < game.GetAllCharactersList().Count; i++)
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

        game.GetCharactersList().Add(game.GetAllCharactersList()[0]);

        for (var i = 0; i < game.GetEnemiesList().Count; i++)
        {
            game.GetEnemiesList()[i].Sprite = EnemyAnimator[i].GetComponent<SpriteRenderer>().sprite;
            game.GetEnemiesList()[i].Animator = EnemyAnimator[i].GetComponent<Animator>().runtimeAnimatorController;
            game.GetEnemiesList()[i].Sounds = EnemyAnimator[i].GetComponent<CharacterBehaviour>().characterSounds;
        }

        for(var i = 0; i  < game.GetItemList().Count; i++)
        {
            game.GetItemList()[i].Icon = ItemSprites[game.GetItemList()[i].Type];
        }
    }
}
