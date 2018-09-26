using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RTP : MonoBehaviour {

    AudioSource audioSource;
    GameController gg;

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
    List<Character> BattlersList = new List<Character>();
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

    int totalEnemy = 1;
    bool winConditionPlayed = false;

    // Use this for initialization
    void Start () {

        audioSource = new AudioSource();
        gg = new GameController();

        //IF CHARACTERS = NULL ? RETURN INTRO//
        if (gg.GetAllCharactersList() == null)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            //GET CHARACTER LIST//
            var allCharacter = gg.GetCharactersList();
            var totalCount = 0;
            var count = 0;

            //LOAD CHARACTER SPRITES & STATS//
            foreach (var i in gg.GetCurrentCharactersList())
            {
                count++;
                var o = allCharacter.Find(ch => ch.ID == i);
                o.TempName = "Player" + count;
                o.StatsInGame = o.Stats;
                BattlersList.Add(o);
                GameObject.Find("Player" + count).GetComponent<CharacterBehaviour>().CharacterArrayID = totalCount;
                GameObject.Find("Player" + count).GetComponent<SpriteRenderer>().sprite = o.Sprite;
                GameObject.Find("Player" + count).GetComponent<Animator>().runtimeAnimatorController = o.Animator;
                GameObject.Find("Player" + count).GetComponent<CharacterBehaviour>().characterSounds = o.Sounds;
                totalCount++;
            }
            //CLEAN NOT USED PLAYERS//
            for (var i = count + 1; i <= 3; i++) { GameObject.Find("Player" + i).SetActive(false); }

            var allEnemies = gg.GetEnemiesList();

            count = 0;


            var enemyLevelList = new List<Enemy>();

            enemyLevelList.Add(allEnemies[0]);
            enemyLevelList.Add(allEnemies[0]);

            totalEnemy = enemyLevelList.Count;

            //LOAD ENEMIES SPRITES & STATS//
            foreach (var i in enemyLevelList)
            {
                count++;
                i.TempName = "Enemy" + count;
                i.StatsInGame = i.Stats;
                i.StatsInGame.SPE = i.StatsInGame.SPE * count;
                BattlersList.Add(i);

                Debug.Log(i.TempName + " is " + totalCount);

                GameObject.Find("Enemy" + count).GetComponent<CharacterBehaviour>().CharacterArrayID = totalCount;
                totalCount++;
            }

            //CLEAN NOT USED ENEMIES//
            for (var i = count + 1; i <= 3; i++) { GameObject.Find("Enemy" + i).SetActive(false); }

            //ADD COMMAND BUTTON//
            for (int i = 0; i < battleMenuButton.Count; i++)
            {
                var newi = i + 1;
                UnityAction<int> action = new UnityAction<int>(ButtonDoAction);
                battleMenuButton[i].onClick.AddListener(delegate { action.Invoke(newi); });
            }

            //Debug.Log(BattlersList.Count);

            //ORDER TURNS//
            GenerateOrder();
        }
    }
    
    bool targetReach = false;
    
    Random rnd = new Random();
    bool isDoingAction = true;
    bool isEnemyDoingAction = false;
    int enemyIsAttacking = 0;
	// Update is called once per frame
	void Update () {

        var oo = GetCharacterTurn(characterTurn);

        //Debug.Log(oo.TempName + " EIA: " + enemyIsAttacking + " IDA: " + isEnemyDoingAction);

        //Debug.Log(turn[2].TempName);
        //Debug.Log(turn[3].TempName);

        if (!oo.IsPlayer){
            //DEFAULT ATTACK//
            if (isEnemyDoingAction && enemyIsAttacking == 0)
            {
                //var enemyInstance = GetCharacterTurn(characterTurn);

                Debug.Log(oo.TempName + " : " + oo.Stats.HP);

                if(oo.StatsInGame.HP>0){
                    Debug.Log("ENEMY IS ATTACKING");
                    isEnemyDoingAction = false;
                    targetReach = false;
                    isDoingAction = false;
                    enemyIsAttacking = 1;
                    StartCoroutine(Attack(GameObject.Find(oo.TempName).GetComponent<Transform>(), 
                                          GameObject.Find(string.Concat("Player", "1")).GetComponent<Transform>()));
                    
                } else{
                    ChangeTurnMethod();
                }
                //StartCoroutine(Attack(GameObject.Find(string.Concat("Player", "1")).GetComponent<Transform>(), GameObject.Find(GetCharacterTurn(characterTurn).TempName).GetComponent<Transform>()));
            }
        } 
        else
        {
            var character = GetCharacterTurn(characterTurn);
            var o = GameObject.Find("Enemies");
            if (battleStatus == 1) { //ATTACK!

                //Debug.Log("dd: " + character.TempName);

                ShowSkillAnim(character.TempName, false);
                Debug.Log("ESPERANDO TARGET : TS (" + targetSelected+ ")");
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

            Debug.Log("D2: " + BattlersList.Count);

            for (var i = 0; i < BattlersList.Count; i++){
                Debug.Log("Characters/Player" + (i + 1));
                GameObject.Find("Characters/Player" + (i + 1)).GetComponent<SpriteRenderer>().sprite = BattlersList[i].Sprite;
                GameObject.Find("Characters/Player" + (i + 1)).GetComponent<Animator>().runtimeAnimatorController = BattlersList[i].Animator;
                //GameObject.Find("Characters/Player" + (i + 1)).GetComponent<Transform>().transform.Translate(BattlersList[i].Position);
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

        if (totalEnemy == 0)
        {
            if (!winConditionPlayed)
            {
                winConditionPlayed = true;
                StartCoroutine(WinCondition());
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

    IEnumerator ShowMessage(string message)
    {

        yield return new WaitForSeconds(2);

        yield return null;
    }

    void ReachTarget(Character first, Transform target1, Transform target2){
        if (!first.IsRange)
        {
            float distanceTarget = Vector2.Distance(target1.position, target2.Find("TargetPlace").GetComponent<Transform>().position);
            if (distanceTarget.CompareTo(C_ZERO) != 0 && !targetReach)
            {
                target1.position = Vector2.MoveTowards(target1.position, target2.Find("TargetPlace").GetComponent<Transform>().position, 7.5f * Time.deltaTime);
                target1.GetComponent<Animator>().SetBool("Run", true);
            }
            else
            {
                targetReach = true;
            }
        }
        else
        {
            targetReach = true;
        }

        Debug.Log("OOOO === OOOO ");

    }

    void AttackAnimation(Character first, Transform target1, Transform target2){
        var tar2 = target2.GetComponent<CharacterBehaviour>().CharacterArrayID;
        var second = GetCharacterStats(tar2);

        targetReach = false;

        isDoingAction = true;
        isEnemyDoingAction = true;

        targetSelected = 0;
        battleStatus = 0;
        target1.GetComponent<Animator>().SetTrigger("DealDamage");
        var damageDone = CharacterDamage(first, second, 1);
        target2.Find("Damage").GetComponent<TextMesh>().text = damageDone.ToString();
        target2.Find("Damage").GetComponent<Transform>().gameObject.SetActive(true);

        target2.GetComponent<Animator>().SetTrigger("Hit");

        BattlersList[tar2].StatsInGame.HP -= damageDone;

        if (BattlersList[tar2].StatsInGame.HP < 0)
        {

            Debug.Log(BattlersList[tar2].Name + " takes " + damageDone + " damage");

            if (!second.IsPlayer)
            {
                target2.GetComponent<Animator>().SetTrigger("Death");
                totalEnemy--;
            }
            else
            {
                target2.GetComponent<Animator>().SetTrigger("KO");
            }

        }

    }

    bool isDoingAnimation = false;
    bool isEnemyDoingAnimation = true;
    IEnumerator Attack(Transform target1, Transform target2)
    {

        var first = GetCharacterStats(characterTurn);

        if(first.IsPlayer){
            ShowSkillAnim(first.TempName, false);

            while (isDoingAnimation)
            {
                ReachTarget(first, target1, target2);
                if (targetReach)
                {
                    AttackAnimation(first, target1, target2);
                    isDoingAnimation = false;
                }
                yield return null;
            }
        } else {

            Debug.Log("ENEMY WILL ATACK");

            while (isEnemyDoingAnimation)
            {
                ReachTarget(first, target1, target2);
                if (targetReach)
                {
                    AttackAnimation(first, target1, target2);
                    isEnemyDoingAnimation = false;
                }
                yield return null;
            }
        }
    }

    public void ShowSkillAnim(string name, bool val)
    {
        if(!name.Contains("Enemy"))
        GameObject.Find(name).GetComponent<Transform>().Find("SkillActive").gameObject.SetActive(val);
    }

    public void ShowEnemyTargets(bool val){
        var enemyList = GameObject.Find("Enemies");
        for(var i = 0; i < enemyList.transform.childCount; i++){
            var enemy = enemyList.transform.GetChild(i).GetComponent<Transform>();
            if(enemy.GetComponent<SpriteRenderer>().color.a != 0)
            {
                enemyList.transform.GetChild(i).GetComponent<Transform>().Find("TargetDamage").gameObject.SetActive(val);
            }            
        } 
    }

    public void Run()
    {
        int c = Random.Range(-1, 2);
    }

    #endregion

    #region "CHANGE TURN"

    public void GenerateOrder()
    {
        turn = new List<Character>();
        turn.AddRange(BattlersList);
        turn.Sort((first, second) => second.Stats.SPE.CompareTo(first.Stats.SPE));

        foreach(var o in turn){
            Debug.Log(o.TempName);
        }
    }

    public void ChangeTurnMethod()
    {
        //CHANGE TURN//
        characterTurn++;
        if (characterTurn == turn.Count)
        {
            characterTurn = 0;
            enemyIsAttacking = 0;
            isDoingAction = false;
            isEnemyDoingAnimation = false;
        }
        changeTurn = true;
    }

    IEnumerator WinCondition()
    {
        yield return new WaitForSeconds(3);
        foreach (var i in BattlersList)
        {
            if (i.TempName.Contains("Player") && i.Status != -1)
            {
                GameObject.Find(i.TempName).GetComponent<Animator>().SetBool("Win", true);
            }
        }
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
        PlaySound(1);
        transform.Find("Victory").GetComponent<Transform>().gameObject.SetActive(true);
    }

    #endregion

    #region "GETTER AND SETTERS"

    public void SetTargetSelected(int t)
    {
        Debug.Log("CHOOSE " + t);
        isDoingAction = true;
        targetSelected = t;
    }

    public Character GetCharacterStats(int p)
    {
        return BattlersList[p];
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
                if(damageDone<0) damageDone = 1;
            }
        } else if(damagetType==2){
            if(attack.StatsInGame.MAG>defense.StatsInGame.MDF){
                damageDone = attack.StatsInGame.MAG - defense.StatsInGame.MDF;
                if (damageDone < 0) damageDone = 1;
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
