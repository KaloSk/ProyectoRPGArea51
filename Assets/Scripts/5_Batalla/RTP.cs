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
    int skillStatus = -1;
    int targetSelected = 0;
    
    float C_ZERO = 0f;

    //public Transform target1;
    //public Transform target2;

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

        Character character = new Character()
        {
            ID = 1,
            IsPlayer = true,
            TempName = "Player1",
            Name = "Cecil",
            Equipment = null,
            Formation = 0,
            Sprite = characterLoadedList[0].GetComponent<Sprite>(),
            Animator = characterLoadedList[0].GetComponent<Animator>().runtimeAnimatorController,
            Position = CharacterFormation(0),
            Type = "T",
            Stats = new Stats() {
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
                    Name = "Golpe con mazo",
                    Damage = 100,
                    Formula = "ENEMY|DAMAGE|100",
                    Level = 1,
                    Icon = skillSpriteList[0],
                    Type = GameConstants.SKILL_TYPE_FOR_ENEMY
                },
				new Skill()
				{
					ID = 2,
					Name = "Golpe con escudo",
					Damage = 100,
					Formula = "ENEMY|DAMAGE|100",
					Level = 1,
					Icon = skillSpriteList[1],
					Type = GameConstants.SKILL_TYPE_FOR_ENEMY
                },
				new Skill()
				{
					ID = 3,
					Name = "Grito de guerra",
					Damage = 100,
					Formula = "ENEMY|DAMAGE|100",
					Level = 1,
					Icon = skillSpriteList[2],
					Type = GameConstants.SKILL_TYPE_FOR_SELF
                },
				new Skill()
				{
					ID = 4,
					Name = "Provocar",
					Damage = 100,
					Formula = "ENEMY|DAMAGE|100",
					Level = 1,
					Icon = skillSpriteList[3],
					Type = GameConstants.SKILL_TYPE_FOR_SELF
                },
				new Skill()
				{
					ID = 5,
					Name = "Endurecer",
					Damage = 100,
					Formula = "ENEMY|DAMAGE|100",
					Level = 1,
					Icon = skillSpriteList[4],
					Type = GameConstants.SKILL_TYPE_FOR_SELF
                }
            }
        };

        character.StatsInGame = character.Stats;

        characterList.Add(character);

        Character character2 = new Character()
        {
            ID = 2,
            IsPlayer = true,
			TempName = "Player2",
            Name = "Kain",
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
            },
            Skills = new List<Skill>()
            {
                new Skill()
                {
                    ID = 1,
                    Name = "Golpe con mazo",
                    Damage = 100,
                    Formula = "ENEMY|DAMAGE|100",
                    Level = 1,
                    Icon = skillSpriteList[0],
                    Type = GameConstants.SKILL_TYPE_FOR_ENEMY
                },
            }
        };

        character2.StatsInGame = character2.Stats;

        characterList.Add(character2);

        Character character3 = new Character()
        {
            ID = 1,
            IsPlayer = true,
			TempName = "Player3",
            Name = "Edge",
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
                SPE = 50,
                LUK = 0
            },
            Skills = new List<Skill>()
            {
                new Skill()
                {
                    ID = 1,
                    Name = "Golpe con mazo",
                    Damage = 100,
                    Formula = "ENEMY|DAMAGE|100",
                    Level = 1,
                    Icon = skillSpriteList[0],
                    Type = GameConstants.SKILL_TYPE_FOR_ENEMY
                },
            }
        };

        character3.StatsInGame = character3.Stats;

        characterList.Add(character3);

        Character enemy1 = new Character()
        {
            ID = 4,
            IsPlayer = false,
			TempName = "Enemy1",
            Name = "Slime",
            Equipment = null,
            Formation = 2,
            Sprite = characterLoadedList[3].GetComponent<Sprite>(),
            Animator = characterLoadedList[3].GetComponent<Animator>().runtimeAnimatorController,
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
                SPE = 60,
                LUK = 0
            }
        };

        enemy1.StatsInGame = enemy1.Stats;

        characterList.Add(enemy1);

        //Debug.Log("D1: " + characterList.Count);
        
        for (int i = 0; i < battleMenuButton.Count; i++) {
            var newi = i+1;
            UnityAction<int> action = new UnityAction<int>(ButtonDoAction);
            battleMenuButton[i].onClick.AddListener(delegate { action.Invoke(newi); });
        }

        if(turn == null) {
            turn = new List<Character>();
            turn.AddRange(characterList);
			turn.Sort((first, second) => second.Stats.SPE.CompareTo(first.Stats.SPE));            
        }

    }
    
    bool targetReach = false;
    

    Random rnd = new Random();
    bool isDoingAction = true;
    int enemyIsAttacking = 0;
	// Update is called once per frame
	void Update () {

        var oo = GetCharacterTurn(characterTurn);

        //Debug.Log(oo.IsPlayer);

        if (!isDoingAction) {
            return;
        }

        if (!oo.IsPlayer){
            //DEFAULT ATTACK//
            if (!isDoingAnimation && enemyIsAttacking == 0)
            {
                Debug.Log("ENMY IS ATTACKING");
                isDoingAnimation = true;
                targetReach = false;
                isDoingAction = false;
                enemyIsAttacking = 1;
                StartCoroutine(Attack(GameObject.Find(GetCharacterTurn(characterTurn).TempName).GetComponent<Transform>(), GameObject.Find(string.Concat("Player", "1")).GetComponent<Transform>()));
                //StartCoroutine(Attack(GameObject.Find(string.Concat("Player", "1")).GetComponent<Transform>(), GameObject.Find(GetCharacterTurn(characterTurn).TempName).GetComponent<Transform>()));
            }
        } 
        else
        {
            var character = GetCharacterTurn(characterTurn);
            var o = GameObject.Find("Enemies");
            if (battleStatus == 1) { //ATTACK!

                Debug.Log("dd: " + character.TempName);

                ShowSkillAnim(character.TempName, false);
                //Debug.Log("ESPERANDO TARGET : TS (" + targetSelected+ ")");
                if (targetSelected !=0)
                {
                    if(isDoingAction)
                    {
                        ShowEnemyTargets(false);
                        isDoingAnimation = true;
                        targetReach = false;
                        isDoingAction = false;
                        StartCoroutine(Attack(GameObject.Find(character.TempName).GetComponent<Transform>(), GameObject.Find("Enemy"+targetSelected).GetComponent<Transform>()));                    
                    }
                }
                else 
                {
                    ShowEnemyTargets(true);
                }
            }
            else if(battleStatus == 2)
            {
                ShowSkillAnim(character.TempName, true);
                if (skillStatus != GameConstants.SKILL_TYPE_NONE)
                {
                    var skillSelected = character.Skills[skillStatus];

                    if (skillSelected.Type.Equals(GameConstants.SKILL_TYPE_FOR_SELF))
                    {

                    }
                    else if (skillSelected.Type.Equals(GameConstants.SKILL_TYPE_FOR_ALLY))
                    {

                    }
                    else if (skillSelected.Type.Equals(GameConstants.SKILL_TYPE_FOR_ENEMY))
                    {
                        if (targetSelected != 0)
                        {
                            ShowEnemyTargets(false);
                            isDoingAnimation = true;
                            targetReach = false;
                            isDoingAction = false;
                            StartCoroutine(Attack(GameObject.Find(character.TempName).GetComponent<Transform>(), GameObject.Find("Enemy" + targetSelected).GetComponent<Transform>()));
                        }
                        else
                        {
                            ShowEnemyTargets(true);
                        }
                    }
                }
                
            }
            else 
            {
                ShowSkillAnim(character.TempName, false);
                ShowEnemyTargets(false);
            }
        }
        

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
       


        /**[DRAW SKILLS]**/
        if (changeTurn)
        {
            var currentSkills = GameObject.Find("ContentSkill");
            for (var i = 0; i < skillContent.transform.childCount; i++)
            {
                Destroy(skillContent.transform.GetChild(i).gameObject);
            }

            changeTurn = false;
            var ch = GetCharacterTurn(characterTurn);
			if (ch.Skills != null) {
				for (var i = 0; i < ch.Skills.Count; i++)
				{   
					GameObject Prefab = Instantiate(prefabPower);
					Prefab.transform.GetComponent<Image>().sprite = ch.Skills[i].Icon;
					Prefab.transform.Find("Panel/Text").GetComponent<Text>().text = ch.Skills[i].Name;

					var newi = i;
                    UnityAction<int> action = new UnityAction<int>(ButtonDoSkill);
                    Prefab.transform.GetComponent<Button>().onClick.AddListener(delegate { action.Invoke(newi); });

					Prefab.transform.SetParent(skillContent.transform);

				}
			}           
        }
	}

    int defaultX = -2;
    int defaultY = 3;

    Vector3 defaultPosition = new Vector3(-2, 3, 0);
    public Vector3 CharacterFormation(int position){
        var pos = new Vector3(defaultX - (position * 2), defaultY - (position * 1.5f), 0);
//        Debug.Log(pos);
        return pos;
    }

    #region "BATTLE MENU"

    void ButtonDoAction(int power)
    {
        battleStatus = power;
        PlaySound(BUTTON_CLICK);
        transform.Find("BattlePanel/SkillPanel").GetComponent<Transform>().gameObject.SetActive(false);
        transform.Find("BattlePanel/ObjectPanel").GetComponent<Transform>().gameObject.SetActive(false);

		if (power.Equals (ACTION_OBJECTS)) {
			transform.Find ("BattlePanel/ObjectPanel").GetComponent<Transform> ().gameObject.SetActive (true);
		} else if (power.Equals (ACTION_SKILLS)) {
			transform.Find ("BattlePanel/SkillPanel").GetComponent<Transform> ().gameObject.SetActive (true);
		} else if (power.Equals (ACTION_RUN)) {
			GameObject.Find("Player1").GetComponent<Animator>().SetBool("Win", true);
			GameObject.Find("Player2").GetComponent<Animator>().SetBool("Win", true);
			GameObject.Find("Player3").GetComponent<Animator>().SetBool("Win", true);
			GameObject.Find ("Main Camera").GetComponent<AudioSource> ().Stop ();
			PlaySound (1);
		}
    }

    void ButtonDoSkill(int power)
    {
        battleStatus = 2;
        skillStatus = power;
        var character = GetCharacterTurn(characterTurn);
        Debug.Log(character.Name + " will use " + character.Skills[power].Name);
    }

    #endregion

    #region "ACTIONS"

    /*public void Attack(Transform target1, Transform target2)
    {
        if (!stopAction) {
            float distanceTarget = Vector2.Distance(target1.position, target2.Find("TargetPlace").GetComponent<Transform>().position);
            if (distanceTarget.CompareTo(C_ZERO) != 0) {                
                target1.position = Vector2.MoveTowards(target1.position, target2.Find("TargetPlace").GetComponent<Transform>().position, 7.5f * Time.deltaTime);
                target1.GetComponent<Animator>().SetBool("Run", true);
                enemyAttacking = false;
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
                enemyAttacking = true;
            }
        }
    }*/


    bool isDoingAnimation = false;
    IEnumerator Attack(Transform target1, Transform target2)
    {
        ShowSkillAnim(GetCharacterStats(characterTurn).TempName, false);

        while (isDoingAnimation) {

//Debug.Log(stopAction);

            float distanceTarget = Vector2.Distance(target1.position, target2.Find("TargetPlace").GetComponent<Transform>().position);
            if (distanceTarget.CompareTo(C_ZERO) != 0 && !targetReach) {                
                target1.position = Vector2.MoveTowards(target1.position, target2.Find("TargetPlace").GetComponent<Transform>().position, 7.5f * Time.deltaTime);
                target1.GetComponent<Animator>().SetBool("Run", true);
                //Debug.Log("ACERCANDONSE");
            }
            else {
                //Debug.Log(target1.name + " llego hacia el " + target2.name);
                targetReach = true;
            }

            if (targetReach) {
                isDoingAnimation = false;
                targetReach = false;                
                isDoingAction = true;
                //Debug.Log("DEAL DAMAGE");
                targetSelected = 0;
                battleStatus = 0;
                target1.GetComponent<Animator>().SetTrigger("DealDamage");
                var dd = CharacterDamage(GetCharacterStats(characterTurn), GetCharacterStats(3),1); 
                target2.Find("Damage").GetComponent<TextMesh>().text = dd.ToString();
                target2.Find("Damage").GetComponent<Transform>().gameObject.SetActive(true);
                
            }
            yield return null;
        }
    }

    public void ShowSkillAnim(string name, bool val)
    {
        GameObject.Find(name).GetComponent<Transform>().Find("SkillActive").gameObject.SetActive(val);
    }

    public void ShowEnemyTargets(bool val){
        var enemyList = GameObject.Find("Enemies");
        for(var i = 0; i < enemyList.transform.childCount; i++){
            enemyList.transform.GetChild(i).GetComponent<Transform>().Find("TargetDamage").gameObject.SetActive(val);
        } 
    }

    #endregion

    #region "CHANGE TURN"

    public void ChangeTurnMethod()
    {
        //CHANGE TURN//
        characterTurn++;
        if (characterTurn == turn.Count)
        {
            characterTurn = 0;
            enemyIsAttacking = 0;
        }
        changeTurn = true;
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

	public Character GetCharacterTurn(int p)
	{
		return turn[p];
	}

	public int GetCharacterTurn()
	{
		return characterTurn;
	}

    public int CharacterDamage(Character attack, Character defense, int damagetType){
        var damageDone = 0; 
        if(damagetType==1){
            if(attack.StatsInGame.ATK>defense.StatsInGame.DEF){
                damageDone = attack.StatsInGame.ATK - defense.StatsInGame.DEF;
            }
        } else if(damagetType==2){
            if(attack.StatsInGame.MAG>defense.StatsInGame.MDF){
                damageDone = attack.StatsInGame.MAG - defense.StatsInGame.MDF;
            }
        }
        return damageDone;
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
