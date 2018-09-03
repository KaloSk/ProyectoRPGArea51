using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RTP : MonoBehaviour {

    private AudioSource audioSource;

    public List<Character> turn;

    bool changeTurn = true;
    int characterTurn = 0;
    int battleStatus = 0;
    int targetSelected = 0;
    
    float C_ZERO = 0f;

    public Transform target1;
    public Transform target2;

    [Header("Characters")]
    public List<GameObject> characterLoadedList;
    List<Character> characterList = new List<Character>();
    int characterLoaded = 0;

    [Header("BattleMenu")]
    public List<Button> battleMenuButton;
    public GameObject skillContent;
    public GameObject prefabPower;

    [Header("SoundsMenu")]
    public List<AudioClip> soundList;

    /********/
    [Header("TempListItem")]
    public List<Sprite> itemSpriteList;
    public List<Sprite> skillSpriteList;
    /*******/

    // Use this for initialization
    void Start () {

        GameController gg = new GameController(skillSpriteList);

        audioSource = new AudioSource();

        target2TP = target2.Find("TargetPlace").GetComponent<Transform>();

        Character character = new Character()
        {
            ID = 1,
            Name = "Cecil",
            Equipment = null,
            Formation = 0,
            Sprite = characterLoadedList[0].GetComponent<Sprite>(),
            Animator = characterLoadedList[0].GetComponent<Animator>().runtimeAnimatorController,
            Position = CharacterFormation(0),
            Type = "T",
            Stats = new Stats(){
                HP = 100,
                MP = 10,
                ATK = 30,
                DEF = 20,
                MAG = 5,
                MDF = 12,
                SPE = 15,
                LUK = 5
            },
            Skills = new List<Skill>()
            {
                new Skill()
                {
                    ID = 1,
                    Name = "Skill 1",
                    Damage = 100,
                    Formula = "ENEMY|DAMAGE|100",
                    Level = 1,
                    Icon = skillSpriteList[0],
                    Type = new SkillType()
                    {
                        ID = 1,
                        Name = "Power"
                    }
                }
            }
        };

        character.StatsInGame = character.Stats;

        characterList.Add(character);

        Character character2 = new Character()
        {
            ID = 2,
            Name = "Prueba 2",
            Equipment = null,
            Formation = 1,
            Sprite = characterLoadedList[1].GetComponent<Sprite>(),
            Animator = characterLoadedList[1].GetComponent<Animator>().runtimeAnimatorController,
            Position = CharacterFormation(1),
            Type = "T",
            Stats = new Stats()
            {
                HP = 100,
                MP = 10,
                ATK = 30,
                DEF = 10,
                MAG = 0,
                MDF = 5,
                SPE = 10,
                LUK = 0
            }
        };

        character2.StatsInGame = character2.Stats;

        characterList.Add(character);

        Character character3 = new Character()
        {
            ID = 1,
            Name = "Prueba 3",
            Equipment = null,
            Formation = 2,
            Sprite = characterLoadedList[2].GetComponent<Sprite>(),
            Animator = characterLoadedList[2].GetComponent<Animator>().runtimeAnimatorController,
            Position = CharacterFormation(2),
            Type = "T",
            Stats = new Stats()
            {
                HP = 100,
                MP = 10,
                ATK = 30,
                DEF = 10,
                MAG = 0,
                MDF = 5,
                SPE = 20,
                LUK = 0
            }
        };

        character3.StatsInGame = character3.Stats;

        characterList.Add(character);

        Debug.Log("D1: " + characterList.Count);
        
        for (int i = 0; i < battleMenuButton.Count; i++) {
            var newi = i+1;
            UnityAction<int> action = new UnityAction<int>(ButtonDoAction);
            battleMenuButton[i].onClick.AddListener(delegate { action.Invoke(newi); });
        }

        if(turn == null) {
            turn = new List<Character>();

            turn.AddRange(characterList);
            
        }

    }
    
    Transform target2TP;
    bool targetReach = false;
    bool stopAction = false;

	// Update is called once per frame
	void Update () {
        /*
        if(characterLoaded==1){

            Debug.Log("D2: " + characterList.Count);

            for (var i = 0; i < characterList.Count; i++){
                Debug.Log("Characters/Player" + (i + 1));
                GameObject.Find("Characters/Player" + (i + 1)).GetComponent<SpriteRenderer>().sprite = characterList[i].Sprite;
                GameObject.Find("Characters/Player" + (i + 1)).GetComponent<Animator>().runtimeAnimatorController = characterList[i].Animator;
                //GameObject.Find("Characters/Player" + (i + 1)).GetComponent<Transform>().transform.Translate(characterList[i].Position);
            }
           
        }
        characterLoaded++;
        */
        var o = GameObject.Find("Enemies");
        if (battleStatus == 1) { //ATTACK!

            Debug.Log("XD" + targetSelected);

            if(targetSelected == 1) {
                stopAction = false;
                targetReach = false;
                Attack();
            }
            else 
            {
                o.transform.GetChild(0).GetComponent<Transform>().Find("TargetDamage").gameObject.SetActive(true);
            }
        }
        else 
        {
            o.transform.GetChild(0).GetComponent<Transform>().Find("TargetDamage").gameObject.SetActive(false);            
        }


        /**[DRAW SKILLS]**/
        if (changeTurn)
        {
            changeTurn = false;
            var ch = GetCharacterStats(0);
            for (var i = 0; i < ch.Skills.Count; i++)
            {   
                GameObject Prefab = Instantiate(prefabPower);
                Prefab.transform.GetComponent<Image>().sprite = ch.Skills[i].Icon;
                Prefab.transform.Find("Panel/Text").GetComponent<Text>().text = ch.Skills[i].Name;
                
                /*var newi = i;
                UnityAction<int> action = new UnityAction<int>(ButtonAddPower);
                Prefab.transform.Find("Button").GetComponent<Button>().onClick.AddListener(delegate { action.Invoke(newi); });*/

                Prefab.transform.SetParent(skillContent.transform);
                
            }
        }
	}

    int defaultX = -2;
    int defaultY = 3;

    Vector3 defaultPosition = new Vector3(-2, 3, 0);
    public Vector3 CharacterFormation(int position){
        var pos = new Vector3(defaultX - (position * 2), defaultY - (position * 1.5f), 0);
        Debug.Log(pos);
        return pos;
    }

    #region "BATTLE MENU"

    void ButtonDoAction(int power)
    {
        Debug.Log(power);
        battleStatus = power;
        PlaySound(BUTTON_CLICK);

        transform.Find("BattlePanel/SkillPanel").GetComponent<Transform>().gameObject.SetActive(false);
        transform.Find("BattlePanel/ObjectPanel").GetComponent<Transform>().gameObject.SetActive(false);

        if (power.Equals(ACTION_OBJECTS))
        {
            transform.Find("BattlePanel/ObjectPanel").GetComponent<Transform>().gameObject.SetActive(true);
        }
        else if (power.Equals(ACTION_SKILLS))
        {
            transform.Find("BattlePanel/SkillPanel").GetComponent<Transform>().gameObject.SetActive(true);
        }
    }

    #endregion


    #region "ACTIONS"

    public void Attack()
    {
        if (!stopAction) {
            float distanceTarget = Vector2.Distance(target1.position, target2TP.position);
            if (distanceTarget.CompareTo(C_ZERO) != 0) {                
                target1.position = Vector2.MoveTowards(target1.position, target2TP.position, 7.5f * Time.deltaTime);
                target1.GetComponent<Animator>().SetBool("Run", true);
            }
            else {
                targetReach = true;
            }

            if (targetReach) {
                targetSelected = 0;
                battleStatus = 0;
                target1.GetComponent<Animator>().SetTrigger("DealDamage");
                target2.Find("Damage").GetComponent<Transform>().gameObject.SetActive(true);                
                stopAction = true;
            }
        }
    }

    #endregion

    #region "GETTER AND SETTERS"

    public void SetTargetSelected(int t)
    {
        targetSelected = t;
    }

    public Character GetCharacterStats(int p)
    {
        return characterList[p];
    }

    #endregion

    #region "SOUND"

    public void PlaySound(int i)
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(soundList[i]);
    }

    #endregion

    #region "CONSTANTS"

    public const int BUTTON_CLICK = 0;

    public const int ACTION_ATTACK = 1;
    public const int ACTION_SKILLS = 2;
    public const int ACTION_OBJECTS = 3;
    public const int ACTION_RUN = 4;

    #endregion

}
