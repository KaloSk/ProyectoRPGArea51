using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class CharacterSelection : MonoBehaviour {

    //TEMPORARY//
    public List<Sprite> CharacterFaces;
    public List<Sprite> CharacterBody;

    GameController game;

    public GameObject ContentCharacterFace;
    public GameObject CharacterFacePrefab;

    public GameObject ContentCharacterFull;
    public GameObject CharacterFullPrefab;

	// Use this for initialization
	void Start () {

        for (var i = 0; i < ContentCharacterFace.transform.childCount; i++){
            Destroy(ContentCharacterFace.transform.GetChild(i));
        }

        game = new GameController();
        game.NewGame();

        //TEMPORAL//
        game.GetCharactersList()[0].Face = CharacterFaces[0];
        game.GetCharactersList()[0].Full = CharacterBody[0];

        game.GetCharactersList()[1].Face = CharacterFaces[1];
        game.GetCharactersList()[1].Full = CharacterBody[1];

        game.GetCharactersList()[2].Face = CharacterFaces[2];
        game.GetCharactersList()[2].Full = CharacterBody[2];


        foreach(var i in game.GetCharactersList()){

            Debug.Log(i.Name);

            GameObject face = Instantiate(CharacterFacePrefab);
            face.transform.Find("Panel/Text").GetComponent<Text>().text = i.Name;
            face.transform.GetComponent<Image>().sprite = i.Face;
            face.transform.parent = ContentCharacterFace.transform;

            var newi = i.ID;
            UnityAction<int> action = new UnityAction<int>(AddCharacter);
            face.GetComponent<Button>().onClick.AddListener(delegate { action.Invoke(newi); });
        }

        foreach(var i in game.GetCurrentCharactersList()){
            AddSprite(i);
        }

	}

    void AddCharacter(int characterID)
    {
        if(!game.GetCurrentCharactersList().Contains(characterID)){
            game.GetCurrentCharactersList().Add(characterID);
            AddSprite(characterID);
            Debug.Log("Character ID " + characterID);
        } else {
            Debug.Log("CANNOT ADD");
        }
    }

    void AddSprite(int characterID){
        var currentCharacter = game.GetCharactersList().Find(character => character.ID == characterID);

        GameObject full = Instantiate(CharacterFullPrefab);
        full.name = "CharacterFull" + currentCharacter.ID;
        full.transform.Find("Name/Text").GetComponent<Text>().text = currentCharacter.Name;
        full.transform.Find("Stats/Number").GetComponent<Text>().text =
            string.Concat(
            currentCharacter.Level.ToString("000"), "\n",
                currentCharacter.Stats.ATK.ToString("000"), "\n",
                currentCharacter.Stats.DEF.ToString("000"), "\n",
                currentCharacter.Stats.MAG.ToString("000"), "\n",
                currentCharacter.Stats.MDF.ToString("000"), "\n",
                currentCharacter.Stats.SPE.ToString("000"), "\n",
                currentCharacter.Stats.LUK.ToString("000")
            );
        full.transform.GetComponent<Image>().sprite = currentCharacter.Full;
        full.transform.parent = ContentCharacterFull.transform;

        var newi = currentCharacter.ID;
        UnityAction<int> action = new UnityAction<int>(DeleteCharacter);
        full.transform.Find("Delete").GetComponent<Button>().onClick.AddListener(delegate { action.Invoke(newi); }); 
    }

    void DeleteCharacter(int characterID){
        game.GetCurrentCharactersList().Remove(game.GetCharactersList().Find(character => character.ID == characterID).ID);
        Destroy(GameObject.Find("CharacterFull"+characterID).gameObject);
    }

}
